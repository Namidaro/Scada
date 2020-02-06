using System;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Subscription token returned from <see cref="EventBase"/> on subscribe
    /// </summary>
    public class SubscriptionToken : Disposable, IEquatable<SubscriptionToken>
    {
        #region [Private fields]
        private readonly Guid _token;
        private Action<SubscriptionToken> _unsubscribeAction;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes a new instance of <see cref="SubscriptionToken"/>.
        /// </summary>
        public SubscriptionToken(Action<SubscriptionToken> unsubscribeAction)
        {
            this._unsubscribeAction = unsubscribeAction;
            this._token = Guid.NewGuid();
        }
        #endregion [Ctor's]


        #region [IEquatable<SubscriptionToken> members]
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the current object is equal to the <paramref name="other"/> parameter;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(SubscriptionToken other)
        {
            this.ThrowIfDisposed();
            return other != null && object.Equals(this._token, other._token);
        } 
        #endregion [IEquatable<SubscriptionToken> members]


        #region [Override members]
        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            this.ThrowIfDisposed();
            return object.ReferenceEquals(this, obj) || this.Equals(obj as SubscriptionToken);
        }
        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            this.ThrowIfDisposed();
            return this._token.GetHashCode();
        }
        /// <summary>
        /// Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            if (this._unsubscribeAction != null)
            {
                this._unsubscribeAction(this);
                this._unsubscribeAction = null;
            }
            base.OnDisposing();
        } 
        #endregion [Override members]
    }
}