using Microsoft.Practices.Unity;
using System;
using ULA.Interceptors.IoC;

namespace ULA.CompositionRoot.IoC
{
    /// <summary>
    ///     Represents a unity container wrapper
    /// </summary>
    public class UnityIoCRootWrapper : IIocContainerRoot
    {
        #region [Private fields]

        private readonly IUnityContainer _container;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="UnityIoCRootWrapper" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IUnityContainer" /> to use
        /// </param>
        public UnityIoCRootWrapper(IUnityContainer container)
        {
            this._container = container;
        }

        #endregion [Ctor's]

        #region [IIocContainerRoot members]

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
        public void RegisterType<TMapTo, TMapFrom>(string name, bool isSingleton = true) where TMapFrom : TMapTo
        {
            this._container.RegisterType<TMapTo, TMapFrom>(name, isSingleton
                                                                     ? (LifetimeManager)
                                                                       new ContainerControlledLifetimeManager()
                                                                     : (LifetimeManager) new TransientLifetimeManager());
        }

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
        public void RegisterType<TMapTo, TMapFrom>(bool isSingleton = true) where TMapFrom : TMapTo
        {
            this._container.RegisterType<TMapTo, TMapFrom>(isSingleton
                                                               ? (LifetimeManager)
                                                                 new ContainerControlledLifetimeManager()
                                                               : (LifetimeManager) new TransientLifetimeManager());
        }

        #endregion [IIocContainerRoot members]
    }
}