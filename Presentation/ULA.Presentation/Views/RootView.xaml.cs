using System.Windows.Input;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Presentation.Views
{
    /// <summary>
    ///     Interaction logic for RootView.xaml
    /// </summary>
    public partial class RootView
    {
        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="RootView" />
        /// </summary>
        /// <param name="viewModel">An instance of <see cref="IRootViewModel" /> to use</param>
        public RootView(IRootViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        #endregion [Ctor's]
        
    }
}