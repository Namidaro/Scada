using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using ULA.AddinsHost.Business.Device.Enums;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.Commands;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Interceptors.Application;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DataServices
{
    public class DeviceCommandService : Disposable, IDeviceCommandService
    {
        private readonly ICommandSendingService _commandSendingService;
        private readonly IDeviceCommandStateViewModelFactory _deviceCommandStateViewModelFactory;
        private readonly IApplicationSettingsService _settingsService;
        private Dictionary<IRuntimeDevice, Queue<Func<IDeviceCommand>>> _queueCommandDictionary;
        private ConcurrentDictionary<IRuntimeDeviceViewModel, IDeviceCommand> _presiousDeviceCommands;

        public DeviceCommandService(ICommandSendingService commandSendingService, IDeviceCommandStateViewModelFactory deviceCommandStateViewModelFactory, IApplicationSettingsService settingsService)
        {
            _commandSendingService = commandSendingService;
            _deviceCommandStateViewModelFactory = deviceCommandStateViewModelFactory;
            _settingsService = settingsService;
            _queueCommandDictionary = new Dictionary<IRuntimeDevice, Queue<Func<IDeviceCommand>>>();
            _presiousDeviceCommands = new ConcurrentDictionary<IRuntimeDeviceViewModel, IDeviceCommand>();
        }


        #region Implementation of IDeviceCommandService

        public async Task AddDeviceCommandCreator(Func<IDeviceCommand> deviceCommandCreator,IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {
            if (!_queueCommandDictionary.ContainsKey(runtimeDeviceViewModel.Model as IRuntimeDevice))
            {
                _queueCommandDictionary.Add(runtimeDeviceViewModel.Model as IRuntimeDevice, new Queue<Func<IDeviceCommand>>());
            }

            _queueCommandDictionary[runtimeDeviceViewModel.Model as IRuntimeDevice].Enqueue(deviceCommandCreator);
           await  TryExecutenextCommandAsync(runtimeDeviceViewModel);
        }

        public async Task TryExecuteCommandOnDevice(IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {
            if (_presiousDeviceCommands.ContainsKey(runtimeDeviceViewModel))
            {
                _presiousDeviceCommands[runtimeDeviceViewModel].CheckIfCommandSucceed(runtimeDeviceViewModel);
                if (_presiousDeviceCommands[runtimeDeviceViewModel].IsCommandSucceed.HasValue)
                {
                    IDeviceCommand command;
                    _presiousDeviceCommands.TryRemove(runtimeDeviceViewModel, out command);
                }
            }

            if (_queueCommandDictionary.ContainsKey(runtimeDeviceViewModel.Model as IRuntimeDevice))
            {
                if (_queueCommandDictionary[runtimeDeviceViewModel.Model as IRuntimeDevice].Count > 0)
                {
                    await TryExecutenextCommandAsync(runtimeDeviceViewModel);
                }
            }
        }


        private async Task TryExecutenextCommandAsync(IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {
            if (_presiousDeviceCommands.ContainsKey(runtimeDeviceViewModel))
            {
                return;
            }
            if (_queueCommandDictionary[runtimeDeviceViewModel.Model as IRuntimeDevice].Count > 0)
            {
                Func<IDeviceCommand> commandCreator = _queueCommandDictionary[runtimeDeviceViewModel.Model as IRuntimeDevice].Dequeue();
               
                if (runtimeDeviceViewModel.StateWidget == WidgetState.NoConnection)
                {
                    await DelayExecution(commandCreator, runtimeDeviceViewModel);
                    return;
                }
                var command = commandCreator.Invoke();
                foreach (var starter in command.Starters)
                {
                    var starterVM = runtimeDeviceViewModel.StarterViewModels.FirstOrDefault((starterViewModel =>

                          starterViewModel?.Model == starter));
                    if (starterVM != null)
                    {
                        starterVM.DeviceCommandStateViewModel =
                            _deviceCommandStateViewModelFactory.CreateDeviceCommandStateViewModel(command);
                    }
                }
                _presiousDeviceCommands.TryAdd(runtimeDeviceViewModel, command);
                await _commandSendingService.TryExecuteCommand(command,runtimeDeviceViewModel.Model as IRuntimeDevice);
            }
        }


        private async Task DelayExecution(Func<IDeviceCommand> deviceCommandCreator,IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {
            for (int i = 0; i < _settingsService.NumberOfLightingCommandRepeat; i++)
            {
                await Task.Delay(_settingsService.MillisecondRepeatInterval);
                if (runtimeDeviceViewModel.StateWidget == WidgetState.NoConnection)
                {
                    continue;
                }
                _queueCommandDictionary[runtimeDeviceViewModel.Model as IRuntimeDevice].Enqueue(deviceCommandCreator);
                await TryExecutenextCommandAsync(runtimeDeviceViewModel);
            }
        }
        


        #endregion
    }
}
