using Microsoft.Practices.Unity;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.ViewModels;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.Views
{
    /// <summary>
    /// Interaction logic for LightingSheduleView.xaml
    /// </summary>
    public partial class LightingSheduleView
    {
        /// <summary>
        ///     Представляет вьюшку графика освещения
        /// </summary>
        public LightingSheduleView(IUnityContainer container)
        {
            var viewModel = container.Resolve(typeof(ILightingSheduleViewModel),
                ApplicationGlobalNames.LIGHTING_SHEDULER_VIEWMODEL_NAME);
            InitializeComponent();
            Guard.AgainstNullReference(viewModel, "viewModel");
            this.DataContext = viewModel;
        }
    }
}
