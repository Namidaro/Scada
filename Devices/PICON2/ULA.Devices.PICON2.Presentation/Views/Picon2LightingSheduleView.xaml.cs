using Microsoft.Practices.Unity;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.ViewModels;
using YP.Toolkit.System.Validation;

namespace ULA.Devices.PICON2.Presentation.Views
{
    /// <summary>
    /// Interaction logic for LightingSheduleView.xaml
    /// </summary>
    public partial class Picon2LightingSheduleView
    {
        /// <summary>
        ///     Представляет вьюшку графика освещения
        /// </summary>
        public Picon2LightingSheduleView(IUnityContainer container)
        {
            var viewModel = container.Resolve(typeof(ILightingSheduleViewModel),
                ApplicationGlobalNames.PICON2LIGHTING_SHEDULER_VIEWMODEL_NAME);
            InitializeComponent();
            Guard.AgainstNullReference(viewModel, "viewModel");
            this.DataContext = viewModel;
        }
    }
}
