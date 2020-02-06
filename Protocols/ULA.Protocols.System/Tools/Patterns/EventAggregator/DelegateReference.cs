using System;
using System.Reflection;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Represents a reference to a <see cref="T:System.Delegate"/> that may contain a
    /// <see cref="T:System.WeakReference"/> to the target. This class is used
    /// internally by the Composite Application Library.
    /// </summary>
    public class DelegateReference : IDelegateReference
    {
        #region [Private fields]
        private readonly Delegate _delegate;
        private readonly WeakReference _weakReference;
        private readonly MethodInfo _method;
        private readonly Type _delegateType;
        #endregion [Private fields]


        #region [IDelegateReference members]
        /// <summary>
        /// Gets the <see cref="T:System.Delegate"/> (the target) referenced by the current
        /// <see cref="DelegateReference"/> object.
        /// </summary>
        /// <value>
        /// <see langword="null"/> if the object referenced by the current
        /// <see cref="DelegateReference"/> object has been garbage collected; otherwise, 
        /// a reference to the <see cref="T:System.Delegate"/> referenced by the current 
        /// <see cref="DelegateReference"/> object.
        /// </value>
        public Delegate Target
        {
            get
            {
                return this._delegate ?? this.TryGetDelegate();
            }
        }
        #endregion [IDelegateReference members]


        #region [Ctor's]
        /// <summary>
        /// Initializes a new instance of <see cref="DelegateReference"/>.
        /// </summary>
        /// <param name="delegate">The original <see cref="T:System.Delegate"/> to create a reference for.</param>
        /// <param name="keepReferenceAlive">If <see langword="false"/> the class will create a weak reference
        /// to the delegate, allowing it to be garbage collected. Otherwise it will keep a strong 
        /// reference to the target.</param>
        /// <exception cref="T:System.ArgumentNullException">If the passed 
        /// <paramref name="delegate"/> is not assignable to <see cref="T:System.Delegate"/>.
        /// </exception>
        public DelegateReference(Delegate @delegate, bool keepReferenceAlive)
        {
            Guard.AgainstNullReference(@delegate, "delegate");
            if (keepReferenceAlive) this._delegate = @delegate;
            else
            {
                this._weakReference = new WeakReference(@delegate.Target);
                this._method = @delegate.Method;
                this._delegateType = @delegate.GetType();
            }
        }
        #endregion [Ctor's]


        #region [Help members]
        private Delegate TryGetDelegate()
        {
            if (this._method.IsStatic)
                return Delegate.CreateDelegate(this._delegateType, null, this._method);
            var target = this._weakReference.Target;
            return target != null ? Delegate.CreateDelegate(this._delegateType, target, this._method) : null;
        }
        #endregion [Help members]
    }
}