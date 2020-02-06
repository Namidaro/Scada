using System;
using System.Reflection;

namespace YP.Toolkit.System.Exceptions
{
    /// <summary>
    /// Represnets base application functionality of an exception
    /// </summary>
    public abstract class ExceptionBase : ApplicationException
    {
        #region [Private fields]
        private string _message = string.Empty;
        private static readonly Lazy<FieldInfo> _innerExceptionFieldInfo =
            new Lazy<FieldInfo>(
                () => typeof (Exception).GetField("_innerException", BindingFlags.NonPublic | BindingFlags.Instance));
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of <see cref="ExceptionBase"/>
        /// </summary>
        protected ExceptionBase()
        { }
        /// <summary>
        /// Initializes an instance of <see cref="ExceptionBase"/>
        /// </summary>
        /// <param name="message">The message of the exception</param>
        protected ExceptionBase(string message)
        {
            this._message = message;
        } 
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        /// <returns>
        /// The error message that explains the reason for the exception, or an empty string("").
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public sealed override string Message
        {
            get
            {
                return this._message;
            }
        }
        #endregion [Override members]


        #region [Internal members]
        /// <summary>
        /// Sets exception message
        /// </summary>
        /// <param name="message">The message to set</param>
        internal void SetMessage(string message)
        {
            this._message = message;
        }
        /// <summary>
        /// Sets an inner exception
        /// </summary>
        /// <param name="innerException">The inner exception to set</param>
        internal void SetInnerException(Exception innerException)
        {
            // ReSharper disable PossibleNullReferenceException
            ExceptionBase._innerExceptionFieldInfo.Value.SetValue(this, innerException);
            // ReSharper restore PossibleNullReferenceException
        }
        #endregion [Internal members]
    }
}