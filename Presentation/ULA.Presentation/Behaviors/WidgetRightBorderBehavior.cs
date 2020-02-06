using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Practices.ServiceLocation;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Device.Enums;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business;
using ULA.Business.Infrastructure;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Common;
using ULA.Interceptors.Application;
using ULA.Presentation.Infrastructure.Services;

namespace ULA.Presentation.Behaviors
{
    /// <summary>
    /// behavior для правой панели, сделано для анимирования квитирования неисправностей
    /// </summary>
    public class WidgetRightBorderBehavior : Behavior<Border>
    {
        private ISoundNotificationsService _soundNotificationsService;
        private IGlobalDefectAcknowledgingService _globalDefectAcknowledgingService;
        private IDefectAcknowledgingService _defectAcknowledgingService;
        /// <summary>
        /// 
        /// </summary>
        public WidgetRightBorderBehavior()
        {
            _soundNotificationsService = ServiceLocator.Current.GetInstance(typeof(ISoundNotificationsService)) as ISoundNotificationsService;
            _globalDefectAcknowledgingService = ServiceLocator.Current.GetInstance(typeof(IGlobalDefectAcknowledgingService)) as IGlobalDefectAcknowledgingService;
        }


        private Border _assotiatedBorder;
        private IRuntimeDeviceViewModel _deviceViewModel;
        private ColorAnimation _borderColorAnimation;
        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        ///     Вызывается после того как поведение привяжется к AssociatedObject
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            _deviceViewModel = AssociatedObject.DataContext as IRuntimeDeviceViewModel;
            _defectAcknowledgingService =
                _globalDefectAcknowledgingService.GetDefectAcknowledgingService(
                    _deviceViewModel.Model as IRuntimeDevice);
            _assotiatedBorder = AssociatedObject;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;

            if (_deviceViewModel != null)
            {
                (_deviceViewModel.Model as IRuntimeDevice).DeviceValuesUpdated += async () =>
                {
                    await Task.Run((() =>
                    {
                        CheckStateWidget();
                    }));
                };
                (_deviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection.ConnectionLostAction += async () =>
                {
                    await Task.Run((() =>
                    {
                        CheckStateWidget();
                    }));
                };


                _defectAcknowledgingService.AcknowledgeValueChanged += CheckStateWidget;
                CheckAckFail();
                CheckStateWidget();
            }
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_deviceViewModel != null)
            {
                _defectAcknowledgingService.AcknowledgeValueChanged = null;
                (_deviceViewModel.Model as IRuntimeDevice).DeviceValuesUpdated -= CheckStateWidget;

            }
        }

        private void CheckStateWidget()
        {
            if (_deviceViewModel == null) return;
            CheckAckFail();
            if (!_defectAcknowledgingService.IsFailNeedsAcknowledge())
            {
                DispatchService.Invoke((() =>
                {
                    switch (_deviceViewModel.StateWidget)
                    {
                        case WidgetState.Norm:
                            _assotiatedBorder.Visibility = Visibility.Collapsed;
                            break;
                        case WidgetState.NoConnection:
                            _assotiatedBorder.Visibility = Visibility.Visible;
                            _assotiatedBorder.Background = Brushes.Silver;
                            break;
                        case WidgetState.Crash:
                            _assotiatedBorder.Visibility = Visibility.Visible;
                            _assotiatedBorder.Background = Brushes.Red;
                            break;
                        case WidgetState.Repair:
                            _assotiatedBorder.Visibility = Visibility.Visible;
                            _assotiatedBorder.Background = Brushes.Orange;
                            break;
                    }
                }));
            }
            else
            {
                if (_deviceViewModel.StateWidget == WidgetState.NoConnection)
                {
                    DispatchService.Invoke((() =>
                    {

                        _assotiatedBorder.Visibility = Visibility.Visible;
                        _assotiatedBorder.Background = Brushes.Silver;

                    }));
                }
            }
        }


        private void CheckAckFail()
        {
            if (_deviceViewModel == null) return;
            if (_deviceViewModel.StateWidget == WidgetState.Repair)
            {
                DispatchService.Invoke((() =>
                {
                    _assotiatedBorder.Visibility = Visibility.Visible;
                    _assotiatedBorder.Background = Brushes.Orange;
                    return;
                }));
            }

            if (_defectAcknowledgingService.IsFailNeedsAcknowledge())
            {
                if (_borderColorAnimation == null)
                {
                    DispatchService.Invoke((() =>
                    {
                        _assotiatedBorder.Visibility = Visibility.Visible;
                        if (_borderColorAnimation == null)
                        {

                            _borderColorAnimation = new ColorAnimation();
                            _borderColorAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 500));
                            _borderColorAnimation.To = Colors.Silver;
                            _borderColorAnimation.From = Colors.Red;
                            _borderColorAnimation.AutoReverse = true;
                            _borderColorAnimation.RepeatBehavior = RepeatBehavior.Forever;
                            _assotiatedBorder.Background = new SolidColorBrush();
                            _assotiatedBorder.Background.BeginAnimation(SolidColorBrush.ColorProperty,
                                _borderColorAnimation);
                            _soundNotificationsService.SetNotificationSource(_deviceViewModel);
                        }
                        return;
                    }));
                }
            }
            else
            {
                DispatchService.Invoke((() =>
                {

                    _borderColorAnimation = null;
                    _soundNotificationsService.ReleaseNotificationSource(_deviceViewModel);
                }));
            }

        }



        /// <summary>
        /// метод срабатывает после отсоединения поведения
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (_deviceViewModel != null)
            {
                try
                {
                    _defectAcknowledgingService.AcknowledgeValueChanged = null;
                    (_deviceViewModel.Model as IRuntimeDevice).DeviceValuesUpdated -= CheckStateWidget;

                }
                catch
                {
                }
            }
        }
    }
}
