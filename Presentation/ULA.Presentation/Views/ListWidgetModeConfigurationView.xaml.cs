using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ULA.AddinsHost.Business.Device;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Markups;
using ULA.Presentation.ViewModels;

namespace ULA.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ListWidgetModeConfigurationView.xaml
    /// </summary>
    public partial class ListWidgetModeConfigurationView
    {
        private Grid _listBoxGrid;
        private IConfigDeviceViewModel _curDragDropItem;

        /// <summary>
        ///     Create a instance of <see cref="ListWidgetModeConfigurationView"/>
        /// </summary>
        public ListWidgetModeConfigurationView()
        {
            InitializeComponent();
        }

        private void MyGrid_OnLoaded(object sender, RoutedEventArgs e)
        {
            this._listBoxGrid = sender as Grid;
        }

        private void UIElement_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            var hit = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (hit == null)
            {
                _curDragDropItem = null;
                return;
            }

            var grid = this._listBoxGrid; //.VisualTree.FirstChild;
            if (grid == null)
            {
                _curDragDropItem = null;
                return;
            }

            var gridPosition = grid.GetColumnRow(e.GetPosition(grid));
            if (_curDragDropItem != null && this.DataContext is ApplicationConfigurationModeViewModel)
            {
                (this.DataContext as ApplicationConfigurationModeViewModel).ChangeDevicePositionNumber(_curDragDropItem,
                    (long)(gridPosition.Y*ApplicationGlobalNames.WidgetListColumnCount + gridPosition.X));
            }
            _curDragDropItem = null;
            //MessageBox.Show(string.Format("Grid location Row: {1} Column: {0}", gridPosition.X, gridPosition.Y));
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var send = (sender as ListBoxItem);
            if(send == null) return;
            _curDragDropItem = send.DataContext as IConfigDeviceViewModel;
            base.OnMouseDown(e);
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var send = (sender as ListBoxItem);
            if (send == null) return;
            IConfigDeviceViewModel editingLogicalDevice = send.DataContext as IConfigDeviceViewModel;
   if (editingLogicalDevice != null && this.DataContext is ApplicationConfigurationModeViewModel)
            {
                (this.DataContext as ApplicationConfigurationModeViewModel).EditCurrentDeviceCommand?.Execute(editingLogicalDevice);
            }
        }
    }
}
