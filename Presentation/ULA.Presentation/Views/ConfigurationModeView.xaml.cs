using System;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.ViewModels;

namespace ULA.Presentation.Views
{
    /// <summary>
    ///     Interaction logic for ConfigurationMode.xaml
    /// </summary>
    public partial class ConfigurationModeView
    {
        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="ConfigurationModeView" />
        /// </summary>
        public ConfigurationModeView()
        {
            InitializeComponent();

            RegionManager.SetRegionManager(this, ServiceLocator.Current.GetInstance<IRegionManager>());

            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.CONFIGURATION_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.LIST_WIDGET_CONFIGURATION_VIEW_NAME, UriKind.Relative);
            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });
        }

        #endregion [Ctor's]


        private void DesignerItem_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DesignerItem.ContextMenu = new ContextMenu();
            MenuItem delmenuItem = new MenuItem {Header = "Удалить"};
            delmenuItem.Click += (ob, se) =>
            {
                (DataContext as ApplicationConfigurationModeViewModel).DeleteImageCommand.Execute(null);
            };
            DesignerItem.ContextMenu.Items.Add(delmenuItem);

        }
        
    }
}