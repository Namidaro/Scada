using System;

namespace ULA.Interceptors.IoC
{
    /// <summary>
    ///     Exposes IoC container root functionality
    ///     Описывает корневой функционал IoC контейнера
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ioc")]
    public interface IIocContainerRoot
    {
        /// <summary>
        ///     Registers an instance mapping in an IoC Container
        /// </summary>
        /// <typeparam name="TMapTo">
        ///     <see cref="Type" /> to map tp
        /// </typeparam>
        /// <typeparam name="TMapFrom">
        ///     <see cref="Type" /> to map from
        /// </typeparam>
        /// <param name="name">The name of the mapping</param>
        /// <param name="isSingleton">A value that indicates whether creating an instance of TMapFrom should be occured only once</param>
        void RegisterType<TMapTo, TMapFrom>(string name, bool isSingleton = true) where TMapFrom : TMapTo;

        /// <summary>
        ///     Registers an instance mapping in an IoC Container
        /// </summary>
        /// <typeparam name="TMapTo">
        ///     <see cref="Type" /> to map tp
        /// </typeparam>
        /// <typeparam name="TMapFrom">
        ///     <see cref="Type" /> to map from
        /// </typeparam>
        /// <param name="isSingleton">A value that indicates whether creating an instance of TMapFrom should be occured only once</param>
        void RegisterType<TMapTo, TMapFrom>(bool isSingleton = true) where TMapFrom : TMapTo;
    }
}