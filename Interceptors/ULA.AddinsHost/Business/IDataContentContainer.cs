using System;

namespace ULA.AddinsHost.Business
{
    /// <summary>
    ///     Exposes device content container functionality to hold device specific data
    ///     Описывает контейнер хранения специфичных для устройства данных
    /// </summary>
    public interface IDataContentContainer
    {
        /// <summary>
        ///     Adds value to a container
        ///     Добавить значение в контэйнер
        /// </summary>
        /// <param name="name">The name of data</param>
        /// <param name="value">The value of data</param>
        void AddValue(string name, object value);


        /// <summary>
        ///     Adds value to a container
        ///     Добавить значения в контэйнер
        /// </summary>
        /// <param name="name">The name of data</param>
        /// <param name="value">The value of data</param>
        void AddValues(string name, object value);

        /// <summary>
        ///     Gets a value from a container
        ///     Вернуть значение из контейнера
        /// </summary>
        /// <typeparam name="T">
        ///     <see cref="Type" /> of value
        /// </typeparam>
        /// <param name="name">The name of data</param>
        /// <returns>Resulted data value</returns>
        T GetValue<T>(string name);

        /// <summary>
        ///     Gets a value from a container
        ///     Вернуть значения из контейнера
        /// </summary>
        /// <typeparam name="T">
        ///     <see cref="Type" /> of value
        /// </typeparam>
        /// <param name="name">The name of data</param>
        /// <returns>Resulted data value</returns>
        T GetValues<T>(string name);
    }
}