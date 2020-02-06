using System;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using ULA.Presentation.Infrastructure;

namespace ULA.Presentation.Views
{
    /// <summary>
    ///     Interaction logic for RuntimeMode.xaml
    /// </summary>
    public partial class RuntimeModeView
    {
        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="RuntimeModeView" />
        /// </summary>
        public RuntimeModeView()
        {
            InitializeComponent();
            RegionManager.SetRegionManager(this, ServiceLocator.Current.GetInstance<IRegionManager>());

            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.LIST_WIDGET_RUNTIME_VIEW_NAME, UriKind.Relative);
            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });
        }

        #endregion [Ctor's]
    }
}