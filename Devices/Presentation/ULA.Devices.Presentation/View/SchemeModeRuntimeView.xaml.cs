using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Devices.Presentation.View
{
    /// <summary>
    /// Interaction logic for SchemeModeView.xaml
    /// </summary>
    public partial class SchemeModeRuntimeView
    {
        /// <summary>
        ///     Constructor of instance <see cref="SchemeModeRuntimeView"/>
        /// </summary>
        public SchemeModeRuntimeView(ISchemeModeRuntimeViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
