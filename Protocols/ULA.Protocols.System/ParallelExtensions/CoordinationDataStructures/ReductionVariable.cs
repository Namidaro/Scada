using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures
{
    /// <summary>
    /// Provides a reduction variable for aggregating data across multiple threads involved in a computation.
    /// </summary>
    /// <typeparam name="T">Specifies the type of the data being aggregated.</typeparam>
    [DebuggerDisplay("Count={_values.Count}")]
    [DebuggerTypeProxy(typeof(ReductionVariableDebugView<>))]
    public sealed class ReductionVariable<T>
    {
        #region [Private fields]
        /// <summary>
        /// The factory used to initialize a value on a thread.
        /// </summary>
        private readonly Func<T> _seedFactory;
        /// <summary>
        /// Thread-local storage for each thread's value.
        /// </summary>
        private readonly ThreadLocal<StrongBox<T>> _threadLocal;
        /// <summary>
        /// The list of all thread-local values for later enumeration.
        /// </summary>
        private readonly ConcurrentQueue<StrongBox<T>> _values = new ConcurrentQueue<StrongBox<T>>();
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes the instances.
        /// </summary>
        public ReductionVariable()
        {
            this._threadLocal = new ThreadLocal<StrongBox<T>>(CreateValue);
        }
        /// <summary>
        /// Initializes the instances.
        /// </summary>
        /// <param name="seedFactory">
        /// The function invoked to provide the initial value for a thread.  
        /// If null, the default value of T will be used as the seed.
        /// </param>
        public ReductionVariable(Func<T> seedFactory)
            : this()
        {
            this._seedFactory = seedFactory;
        } 
        #endregion [Ctor's]


        #region [Properties]
        /// <summary>
        /// Gets or sets the value for the current thread.
        /// </summary>
        public T Value
        {
            get { return this._threadLocal.Value.Value; }
            set { this._threadLocal.Value.Value = value; }
        }
        /// <summary>
        /// Gets the values for all of the threads that have used this instance.
        /// </summary>
        public IEnumerable<T> Values
        {
            get
            {
                return this._values.Select(s => s.Value);
            }
        } 
        #endregion [Properties]


        #region [Public members]
        /// <summary>
        /// Applies an accumulator function over the values in this variable.
        /// </summary>
        /// <param name="function">An accumulator function to be invoked on each value.</param>
        /// <returns>The accumulated value.</returns>
        public T Reduce(Func<T, T, T> function)
        {
            return this.Values.Aggregate(function);
        }
        /// <summary>
        /// Applies an accumulator function over the values in this variable.
        /// The specified seed is used as the initial accumulator value.
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="function">An accumulator function to be invoked on each value.</param>
        /// <returns>The accumulated value.</returns>
        public TAccumulate Reduce<TAccumulate>(TAccumulate seed, Func<TAccumulate, T, TAccumulate> function)
        {
            return this.Values.Aggregate(seed, function);
        } 
        #endregion [Public members]


        #region [Help members]
        /// <summary>
        /// Creates a value for the current thread and stores it in the central list of values.
        /// </summary>
        /// <returns>The boxed value.</returns>
        private StrongBox<T> CreateValue()
        {
            var s = new StrongBox<T>(this._seedFactory != null ? this._seedFactory() : default(T));
            this._values.Enqueue(s);
            return s;
        } 
        #endregion [Help members]
    }

    /// <summary>
    /// Debug view for the reductino variable
    /// </summary>
    /// <typeparam name="T">Specifies the type of the data being aggregated.</typeparam>
    internal sealed class ReductionVariableDebugView<T>
    {
        #region [Private fields]
        private readonly ReductionVariable<T> _variable; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ReductionVariableDebugView{T}"/>
        /// </summary>
        /// <param name="variable">An instance of reduction variable to perform debug view for</param>
        public ReductionVariableDebugView(ReductionVariable<T> variable)
        {
            this._variable = variable;
        } 
        #endregion [Ctor's]


        #region [Properties]
        /// <summary>
        /// Gets a collection of values
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Values
        {
            get
            {
                return this._variable.Values.ToArray();
            }
        } 
        #endregion [Properties]
    }
}