using Microsoft.Practices.Unity;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.CompositionRoot
{
    /// <summary>
    ///     Represents current application content container
    /// </summary>
    internal class ApplicationContentContainer : Disposable, IApplicationContentContainer
    {
        #region [Private fields]

        private IUnityContainer _container;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="ApplicationContentContainer" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IUnityContainer" /> that represents current application IoC Container
        /// </param>
        public ApplicationContentContainer(IUnityContainer container)
        {
            this._container = container;
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            if (this._container != null)
            {
                this._container.Dispose();
                this._container = null;
            }
            base.OnDisposing();
        }

        #endregion [Override members]
    }
}