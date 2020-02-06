using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using ULA.Interceptors.Application;

namespace ULA.CompositionRoot.IoC
{
    /// <summary>
    ///     Represents application state lifetime manager
    /// </summary>
    public class ApplicationStateLifetimeManager : LifetimeManager, IRequiresRecovery
    {
        #region [Private fields]

        private readonly IApplicationState _applicationState;
        private readonly object _syncObject = new object();
        private Tuple<Guid, object> _currentObject;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="ApplicationStateLifetimeManager" />
        /// </summary>
        /// <param name="applicationState">
        ///     An instance of <see cref="IApplicationState" /> to use
        /// </param>
        public ApplicationStateLifetimeManager(IApplicationState applicationState)
        {
            this._applicationState = applicationState;
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>
        ///     the object desired, or null if no such object is currently stored.
        /// </returns>
        public override object GetValue()
        {
            lock (this._syncObject)
            {
                return this.SynchronizedGetValue();
            }
        }

        /// <summary>
        ///     Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            lock (this._syncObject)
            {
                this.SynchronizedSetValue(newValue);
            }
        }

        /// <summary>
        ///     Remove the given object from backing store.
        /// </summary>
        public override void RemoveValue()
        {
            if (this._currentObject != null)
                this.DisposeCurrentObject();
        }

        #endregion [Override members]

        #region [IRequiresRecovery members]

        /// <summary>
        ///     A method that does whatever is needed to clean up
        ///     as part of cleaning up after an exception.
        /// </summary>
        /// <remarks>
        ///     Don't do anything that could throw in this method,
        ///     it will cause later recover operations to get skipped
        ///     and play real havoc with the stack trace.
        /// </remarks>
        public void Recover()
        {
            /*None*/
        }

        #endregion [IRequiresRecovery members]

        #region [Help members]

        private void DisposeCurrentObject()
        {
            var disposable = this._currentObject.Item2 as IDisposable;
            if (disposable != null)
                disposable.Dispose();
            this._currentObject = null;
        }

        private void SynchronizedSetValue(object newValue)
        {
            if (this._currentObject != null)
                this.DisposeCurrentObject();
            this._currentObject = new Tuple<Guid, object>(this._applicationState.StateId, newValue);
        }

        private object SynchronizedGetValue()
        {
            if (this._currentObject == null)
                return null;
            if (this._currentObject.Item2 == null)
                return null;
            if (!this._currentObject.Item1.Equals(this._applicationState.StateId))
            {
                this.DisposeCurrentObject();
                return null;
            }
            return this._currentObject.Item2;
        }

        #endregion [Help members]
    }
}