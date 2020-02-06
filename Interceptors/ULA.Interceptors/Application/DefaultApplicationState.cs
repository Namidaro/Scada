using System;
using System.Collections.Generic;

namespace ULA.Interceptors.Application
{
    /// <summary>
    ///     Represents default application state functionality
    ///     Представляет базовый функционал состояния приложения
    /// </summary>
    internal class DefaultApplicationState : IApplicationState
    {
        #region [Private fields]

        private readonly object _syncObject = new object();
        private IDictionary<object, object> _context;
        private ApplicationState _state;
        private Guid _uid;

        #endregion [Private fields]

        #region [IApplicationState members]

        /// <summary>
        ///     Gets an instance of <see cref="ApplicationState" /> that represents current application state enumeration
        ///     Текущее состояние
        /// </summary>
        public ApplicationState CurrentState
        {
            get
            {
                lock (this._syncObject)
                {
                    return this._state;
                }
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="Guid" /> that represents current state unique identifier
        ///     Id состояния
        /// </summary>
        public Guid StateId
        {
            get
            {
                lock (this._syncObject)
                {
                    return this._uid;
                }
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IDictionary{TKey,TValue}" /> that represents current application state context
        ///     Представляет контекст текущего состояние
        /// </summary>
        public IDictionary<object, object> CurrentContext
        {
            get
            {
                lock (this._syncObject)
                {
                    return this._context;
                }
            }
        }

        #endregion [IApplicationState members]

        #region [Public members]

        /// <summary>
        ///     Sets an instance of <see cref="ApplicationState" /> that represents the state enumeration
        ///     Метод установки состояния
        /// </summary>
        /// <param name="state">
        ///     An instance of <see cref="ApplicationState" /> that represents the state enumeration
        /// </param>
        internal void SetState(ApplicationState state)
        {
            lock (this._syncObject)
            {
                this._state = state;
            }
        }

        /// <summary>
        ///     Sets an instance of <see cref="IDictionary{TKey,TValue}" /> that represents the state context
        ///     Устанавливает контекст текущего состояния приложения
        /// </summary>
        /// <param name="context">
        ///     An instance of <see cref="IDictionary{TKey,TValue}" /> that represents the state context
        /// </param>
        internal void SetContext(IDictionary<object, object> context)
        {
            lock (this._syncObject)
            {
                this._context = context;
            }
        }

        /// <summary>
        ///     Sets an instance of <see cref="Guid" /> that represents current state unique identifier
        ///     Устанавливает Id-к текущего состояния
        /// </summary>
        /// <param name="uid"></param>
        internal void SetUid(Guid uid)
        {
            lock (this._syncObject)
            {
                this._uid = uid;
            }
        }

        #endregion [Public members]
    }
}