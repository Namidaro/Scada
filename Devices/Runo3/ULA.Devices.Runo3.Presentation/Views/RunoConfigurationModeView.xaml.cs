using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ULA.Devices.Runo3.Presentation.ViewModels;
using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.ViewModels;
using YP.Toolkit.System.Validation;

namespace ULA.Devices.Runo3.Presentation.Views
{
    /// <summary>
    /// Interaction logic for RunoConfigurationModeView.xaml
    /// </summary>
    public partial class RunoConfigurationModeView
    {
        /// <summary>
        ///     Конструктор вьюшеи конфигурации руно
        /// </summary>
        /// <param name="container">контейнер для поиска вью-модели</param>
        public RunoConfigurationModeView(IIoCContainer container)
        {
            InitializeComponent();
            IConfigurationModeViewModel viewModel =
                container.Resolve<IConfigurationModeViewModel>(ApplicationGlobalNames.RUNO_CONFIGURATION_VIEWMODEL_NAME);
            Guard.AgainstNullReference(viewModel, "ViewModel");
            this.DataContext = viewModel;
        }

        private void TimeToStartTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox)
            {
                if (((sender as TextBox).Text + e.Text).Length == 0)
                {
                    (sender as TextBox).Text = "0";
                }
                e.Handled = IsCorrectTimeToStart((sender as TextBox).Text+e.Text);
            }
            else
            {
                e.Handled = true;
            }
        }

        private bool IsCorrectTimeToStart(string text)
        {
            bool result = true;
            int intResult;
            if (Int32.TryParse(text, out intResult))
            {
                if (intResult >= 0 && intResult <= 65535)
                {
                    result = false;
                }
            }
            return result;
        }

        private void TimeToStartTextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                if ((sender as TextBox).Text.Length == 0)
                {
                    (sender as TextBox).Text = "0";
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void FaultMask_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void _cbManagemnt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.DataContext as RunoConfigurationModeViewModel;
            if (!vm.IsInitializeNow)
            {
                for (int i = 0; i < vm.ManagementKuSelected.Count; i++)
                {
                    if (vm.TempManagmentCollection[i] != vm.ManagementKuSelected[i])
                    {
                        vm.ManagementKuSelected[i] = vm.TempManagmentCollection[i];
                        return;
                    }
                }
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.DataContext as RunoConfigurationModeViewModel;
            if (!vm.IsInitializeNow)
            {
                for (int i = 0; i < vm.OutputsKuSelected.Count; i++)
                {
                    if (vm.TempOutputKuSelected[i] != vm.OutputsKuSelected[i])
                    {
                        vm.OutputsKuSelected[i] = vm.TempOutputKuSelected[i];
                        return;
                    }
                }
            }
        }
    }
}
