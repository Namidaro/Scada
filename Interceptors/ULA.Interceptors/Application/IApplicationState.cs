using System;
using System.Collections.Generic;

namespace ULA.Interceptors.Application
{
    /// <summary>
    ///     Exposes current application state functionality 
    ///     Описывает текущее состояние приложения
    /// </summary>
    public interface IApplicationState
    {
        /// <summary>
        ///     Gets an instance of <see cref="ApplicationState" /> that represents current application state enumeration
        ///     Текущее состояние приложения
        /// </summary>
        ApplicationState CurrentState { get; }

        /// <summary>
        ///     Gets an instance of <see cref="Guid" /> that represents current state unique identifier
        ///     Id состояние
        /// </summary>
        Guid StateId { get; }

        /// <summary>
        ///     Gets an instance of <see cref="IDictionary{TKey,TValue}" /> that represents current application state context
        ///     Описывает контекст состояния приложения
        /// </summary>
        IDictionary<object, object> CurrentContext { get; }
    }
}