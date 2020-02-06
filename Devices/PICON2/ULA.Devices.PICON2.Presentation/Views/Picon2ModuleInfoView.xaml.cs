using System.Windows;
using ULA.Devices.PICON2.Presentation.Interfaces;

namespace ULA.Devices.PICON2.Presentation.Views
{
    /// <summary>
    /// Interaction logic for Picon2ModuleInfoView.xaml
    /// </summary>
    public partial class Picon2ModuleInfoView : Window
    {
       

        public Picon2ModuleInfoView(IPicon2ModuleInfoViewModel picon2ModuleInfoViewModel)
        {
            DataContext = picon2ModuleInfoViewModel;
            InitializeComponent();
        }
    }
}
