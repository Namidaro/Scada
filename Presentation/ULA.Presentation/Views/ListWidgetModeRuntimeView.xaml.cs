using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Prism.Regions;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business;
using ULA.Business.Infrastructure;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.ViewModels;

namespace ULA.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ListWidgetView.xaml
    /// </summary>
    public partial class ListWidgetModeRuntimeView 
    {
        private readonly IGlobalDefectAcknowledgingService _globalDefectAcknowledgingService;

        /// <summary>
        ///     Constructor of instance <see cref="ListWidgetModeRuntimeView"/>
        /// </summary>
        public ListWidgetModeRuntimeView(IGlobalDefectAcknowledgingService globalDefectAcknowledgingService)
        {
            _globalDefectAcknowledgingService = globalDefectAcknowledgingService;
            InitializeComponent();
            ListWidgetRuntimeListBox.MouseRightButtonUp += ListWidgetRuntimeListBox_MouseRightButtonUp;
            ListWidgetRuntimeListBox.SelectionChanged += ListWidgetRuntimeListBox_SelectionChanged;
        }

        private void ListWidgetRuntimeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
            {
                (DataContext as IApplicationRuntimeModeViewModel).SelectedDeviceViewModels =
                    ListWidgetRuntimeListBox.SelectedItems.Cast<IRuntimeDeviceViewModel>().ToList();
            }
        }

        private void ListWidgetRuntimeListBox_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is ListBox)) return;
            object o= (sender as ListBox).SelectedItem;
            if(o==null)return;
            if (o is IRuntimeDeviceViewModel)
            {

             IDefectAcknowledgingService defectAcknowledgingService=   _globalDefectAcknowledgingService.GetDefectAcknowledgingService(
                    (o as IRuntimeDeviceViewModel).Model as IRuntimeDevice);
                if (!defectAcknowledgingService.IsFailNeedsAcknowledge())
                {
                    if (((ListBox) sender).ContextMenu != null)
                        ((ListBox) sender).ContextMenu = null;
                    return;
                }
                ((ListBox) sender).ContextMenu=new ContextMenu();
                MenuItem ackMenuItem = new MenuItem {Header = "Квитировать неисправность"};
                ackMenuItem.Click += (se,ea) =>
                {
                    defectAcknowledgingService.AcknowledgeFail();
                };
                ((ListBox) sender).ContextMenu.Items.Add(ackMenuItem);
            }
        }
       
    }
}
