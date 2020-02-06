using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ULA.Interceptors.Application
{
    /// <summary>
    ///     Represents default application state manager functionality
    ///     Представляет функционал менеджера состояний приложения
    /// </summary>
    public class DefaultApplicationStateManger : IApplicationStateManager
    {
        #region [Const]

        private const string INTERNAL_APPLICATION_STATE_UUID_CONTEXT = "INternalApplicationStateUUID";
        #endregion

        #region [Private fields]

        private readonly object _syncObject = new object();
        private DefaultApplicationState _currentState;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="DefaultApplicationStateManger" />
        /// </summary>
        public DefaultApplicationStateManger()
        {
            this.UpdateApplicationState(ApplicationState.UNKNOWN);
        }

        #endregion [Ctor's]

        #region [IApplicationStateManager members]

        /// <summary>
        ///     Goes to a new application state
        ///     Метод устанавливающий новое состояние приложения
        /// </summary>
        /// <param name="state">
        ///     An instance of <see cref="ApplicationState" /> that represent new application state enumeration
        /// </param>
        public void GotToNewState(ApplicationState state)
        {
            lock (this._syncObject)
                this.UpdateApplicationState(state);
        }

        /// <summary>
        ///     Gets an instance of <see cref="IApplicationState" /> that represents current state
        ///     Текущее состояние приложения
        /// </summary>
        public IApplicationState CurrentState
        {
            get
            {
                lock (this._syncObject)
                {
                    return this._currentState;
                }
            }
        }

        #endregion [IApplicationStateManager members]

        #region [Help members]

        /// <summary>
        ///     Создает контекст состояния приложения
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        private IDictionary<object, object> CreateApplicationContext(out Guid uid)
        {
            uid = Guid.NewGuid();
            IDictionary<object, object> newContext = new ConcurrentDictionary<object, object>();
            newContext.Add(INTERNAL_APPLICATION_STATE_UUID_CONTEXT, uid.ToString());
            return newContext;
        }

        /// <summary>
        ///     Обновляет состояние приложения
        /// </summary>
        /// <param name="state"></param>
        private void UpdateApplicationState(ApplicationState state)
        {
            if (this._currentState == null)
            {
                this._currentState = new DefaultApplicationState();
            }
            this._currentState.SetState(state);
            Guid uid;
            this._currentState.SetContext(this.CreateApplicationContext(out uid));
            this._currentState.SetUid(uid);
        }

        #endregion [Help members]
    }
}