using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NLog;
using Prism.Commands;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.Commands;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Factories;
using ULA.Devices.Presentation.Logger;
using ULA.Interceptors;
using ULA.Localization;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Services.Interactions;

namespace ULA.Devices.Presentation.Runtime
{
    public class DeviceCommandQueueViewModel : IDeviceCommandQueueViewModel
    {
        private readonly IRuntimeDevice _currentDeviceViewModel;
        private readonly IInteractionService _interactionService;
        private readonly IDeviceCommandFactory _deviceCommandFactory;
        private readonly IDeviceCommandService _deviceCommandService;
        private readonly DispatcherCommandsLogger _dispatcherCommandsLogger;
        private IRuntimeDeviceViewModel _runtimeDeviceViewModel;
        private NLog.Logger _logger;

        public DeviceCommandQueueViewModel(IInteractionService interactionService, IDeviceCommandFactory deviceCommandFactory,IDeviceCommandService deviceCommandService,DispatcherCommandsLogger dispatcherCommandsLogger)
        {
            _interactionService = interactionService;
            _deviceCommandFactory = deviceCommandFactory;
            _deviceCommandService = deviceCommandService;
            _dispatcherCommandsLogger = dispatcherCommandsLogger;
            SetDateTimeCommand = new DelegateCommand(OnSetDateTimeCommand);
            AutoModeAllStartersCommand = new DelegateCommand(OnRunAutoModeAllStarters);
            ManualModeAllStartersCommand = new DelegateCommand(OnRunManualModeAllStarters);
            StartRepairAllStarter = new DelegateCommand(OnStartRepairAllStarters);
            StopRepairAllStarter = new DelegateCommand(OnStopRepairAllStarter);
            RunNightlightCommand=new DelegateCommand(OnRunNightlightCommand);
            StopNightlightCommand=new DelegateCommand(OnStopNightlightCommand);
            RunEveningCommand=new DelegateCommand(OnRunEveningCommand);
            StopEveningCommand=new DelegateCommand(OnStopEveningCommand);
            RunFullCommand=new DelegateCommand(OnRunFullCommand);
            StopFullCommand=new DelegateCommand(OnStopFullCommand);
            RunBacklightCommand=new DelegateCommand(OnRunBacklightCommand);
            StopBacklightCommand=new DelegateCommand(OnStopBacklightCommand);
            RunIlluminationCommand=new DelegateCommand(OnRunIlluminationCommand);
            StopIlluminationCommand=new DelegateCommand(OnStopIlluminationCommand);
            RunEnergySavingCommand=new DelegateCommand(OnRunEnergySaving);
            StopEnergySavingCommand = new DelegateCommand(OnStopEnergySaving);



        }

        private void OnStopEnergySaving()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }


            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Отключить энергосбережение на всех пускателях?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                         _dispatcherCommandsLogger.EnergySavingOff(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStartEnergySavingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }

        private void OnRunEnergySaving()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }


            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Включить энергосбережение на всех пускателях?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                         _dispatcherCommandsLogger.EnergySavingOn(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStartEnergySavingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }


        public void SetContext(IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {
            _runtimeDeviceViewModel = runtimeDeviceViewModel;
            _logger = LogManager.GetLogger(_runtimeDeviceViewModel.DeviceName);
        }

        /// <summary>
        ///     Represent action which set date and time in deviceViewModel
        ///     Команада синхронизации времени на устройстве
        /// </summary>
        public ICommand SetDateTimeCommand { get; }
        /// <summary>
        ///     Перевод в авторежим всех пускателей
        /// </summary>
        public ICommand AutoModeAllStartersCommand { get; }
        /// <summary>
        /// Перевод в ручной режим все пускатели
        /// </summary>
        public ICommand ManualModeAllStartersCommand { get; }

        /// <summary>
        ///     Включение режима ремонта на всех пускателей
        /// </summary>
        public ICommand StartRepairAllStarter { get; }

        public ICommand StopRepairAllStarter { get; }
        /// <summary>
        ///     Включить ночное
        ///  </summary>
        public ICommand RunNightlightCommand { get; }
        /// <summary>
        ///     Отключить ночное
        /// </summary>
        public ICommand StopNightlightCommand { get; }
        /// <summary>
        ///     Включить вечернее
        /// </summary>
        public ICommand RunEveningCommand { get; }
        /// <summary>
        ///   Отключить вечернее
        /// </summary>
        public ICommand StopEveningCommand { get; }
        /// <summary>
        ///     Включить полное
        /// </summary>
        public ICommand RunFullCommand { get; }
        /// <summary>
        ///     Отключить полное
        /// </summary>
        public ICommand StopFullCommand { get; }


        /// <summary>
        ///     Включить подсветку
        /// </summary>
        public ICommand RunBacklightCommand { get; }
        /// <summary>
        ///     Отключить подсветку
        /// </summary>
        public ICommand StopBacklightCommand { get; }

        /// <summary>
        ///     Включить иллюминацию
        /// </summary>
        public ICommand RunIlluminationCommand { get; }

        /// <summary>
        ///     Отключить иллюминацию
        /// </summary>
        public ICommand StopIlluminationCommand { get; }

        public ICommand RunEnergySavingCommand { get; }
        public ICommand StopEnergySavingCommand { get; }


        private void OnSetDateTimeCommand()
        {
            //{
            //    this._interactionService.Interact(
            //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
            //        viewModel =>
            //        {
            //            viewModel.Title = LocalizationResources.Instance.RepairDefandTitle;
            //            viewModel.Message = Localization.LocalizationResources.Instance.RepairDefand;
            //        });
            //    return;
            //}
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }


            this._interactionService.Interact(ApplicationInteractionProviders.SynchronizationTimeInteractionProvider,
                viewModel =>
                {
                },
                viewModel =>
                {
                    if (viewModel.Result == MessageBoxResult.OK)
                    {
                        _deviceCommandService.AddDeviceCommandCreator((() =>
                            _deviceCommandFactory.CreateSyncTimeCommand(viewModel.DateTime,
                                (_runtimeDeviceViewModel.Model as IRuntimeDevice))), _runtimeDeviceViewModel);

                        _dispatcherCommandsLogger.SyncTime(_logger);
                    }
                    else if (viewModel.Result == MessageBoxResult.CANCEL)
                    {
                        return;
                    }
                    else
                    {
                        throw new ArgumentException("SynchronizationTimeInteraction result is incorrect");
                    }
                });
        }






        private void OnRunAutoModeAllStarters()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }
            
            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Включить авто режим на всех пускателях?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                       _dispatcherCommandsLogger.AutoModeAllStarters(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateRunAutoModeCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }




        /// <summary>
        /// Перевод пускателей в ручной режим  1-го устройства
        /// </summary>
        private void OnRunManualModeAllStarters()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }
            
            var result = LightingCommandResult.UNDEFINED;
                this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                    viewModel =>
                    {
                        viewModel.Title = "Вы уверены?";
                        viewModel.Message = "Включить ручной режим на всех пускателях?";
                    },
                    viewModel =>
                    {
                        if (viewModel.Result == MessageBoxResult.YES)
                        {
                            _dispatcherCommandsLogger.ManualModeAllStarters(_logger);
                            _deviceCommandService.AddDeviceCommandCreator(
                                () => _deviceCommandFactory.CreateStopAutoModeCommand(
                                    _runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                            if (result.Equals(LightingCommandResult.NO_EXIST))
                            {
                                this._interactionService.Interact(
                                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                    viewModelEx =>
                                    {
                                        viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                        viewModelEx.Message =
                                            Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                    });
                                return;
                            }
                        }
                    });
            

        }

        //if (CheckRepair())
            //{
            //    var result = LightingCommandResult.UNDEFINED;
            //    this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
            //        viewModel =>
            //        {
            //            viewModel.Title = "Вы уверены?";
            //            viewModel.Message = "Включить ручной режим на всех пускателях?";
            //        },
            //        viewModel =>
            //        {
            //            if (viewModel.Result == MessageBoxResult.YES)
            //            {
            //                _dispatcherCommandsLogger.ManualModeAllStarters(_logger);
            //                _deviceCommandService.AddDeviceCommandCreator(
            //                    () => _deviceCommandFactory.CreateStopAutoModeCommand(
            //                        _runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

            //                if (result.Equals(LightingCommandResult.NO_EXIST))
            //                {
            //                    this._interactionService.Interact(
            //                        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
            //                        viewModelEx =>
            //                        {
            //                            viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
            //                            viewModelEx.Message =
            //                                Localization.LocalizationResources.Instance.StarterNoExistMessage;
            //                        });
            //                    return;
            //                }
            //                if (result.Equals(LightingCommandResult.REPAIR))
            //                {
            //                    this._interactionService.Interact(
            //                        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
            //                        viewModelRep =>
            //                        {
            //                            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
            //                            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
            //                        });
            //                    return;
            //                }
            //            }
            //        });
            //}
        





        private void OnStartRepairAllStarters()
        {

            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Включить режим ремонта на всех пускателях?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                         _dispatcherCommandsLogger.StopAllStarters(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStopAllStartersCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);



                         _dispatcherCommandsLogger.RepairModeAllStarters(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStartRepairCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         if (result.Equals(LightingCommandResult.REPAIR))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelRep =>
                                 {
                                     viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                                 });
                             return;
                         }
                     }
                 });
        }


        private void OnStopRepairAllStarter()
        {
            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Отключить режим ремонта на всех пускателях?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                      _dispatcherCommandsLogger.OffRepairModeAllStarters(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStopRepairCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         if (result.Equals(LightingCommandResult.REPAIR))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelRep =>
                                 {
                                     viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                                 });
                             return;
                         }
                     }
                 });
        }


        private void OnRunNightlightCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }


            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить включение ночного освещения?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                      _dispatcherCommandsLogger.NightLightingOn(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateRunNightlightingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);
                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }



        private void OnStopNightlightCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }


            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить отключение ночного освещения?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                       _dispatcherCommandsLogger.NightLightingOff(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStopNightlightingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }

        private void OnRunEveningCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }

            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить включение вечернего освещения?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                        _dispatcherCommandsLogger.EveninglightingLightingOn(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateRunEveninglightingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }




        private void OnStopEveningCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }

            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить отключение вечернего освещения?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                         _dispatcherCommandsLogger.EveninglightingLightingOff(_logger);

                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStopEveninglightingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }



        private void OnRunFullCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }

            var resultFull = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить включение полного освещения?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                        _dispatcherCommandsLogger.FulllightingLightingOn(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateRunFullLightingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (resultFull.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }

                         //if (resultFull.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }




        private void OnStopFullCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }

            var resultFull = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить отключение полного освещения?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                         _dispatcherCommandsLogger.FulllightingLightingOff(_logger);

                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStopFullLightingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);


                         if (resultFull.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (resultFull.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }



        private void OnRunBacklightCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }

            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить включение подсветки?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                        _dispatcherCommandsLogger.BackLightOn(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateRunBackLightLightingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }






        private void OnStopBacklightCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }

            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить отключение подсветки?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                         _dispatcherCommandsLogger.BackLightOff(_logger);

                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStopBackLightLightingCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }


        private void OnRunIlluminationCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }

            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить включение иллюминации?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                       _dispatcherCommandsLogger.IlluminationOn(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateRunIlluminationCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }


        
        private void OnStopIlluminationCommand()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModelRep =>
                    {
                        viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }

            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить отключение иллюминации?";
                },
                 viewModel =>
                 {
                     if (viewModel.Result == MessageBoxResult.YES)
                     {
                         _dispatcherCommandsLogger.IlluminationOff(_logger);
                         _deviceCommandService.AddDeviceCommandCreator(() => _deviceCommandFactory.CreateStopIlluminationCommand(_runtimeDeviceViewModel.Model as IRuntimeDevice), _runtimeDeviceViewModel);

                         if (result.Equals(LightingCommandResult.NO_EXIST))
                         {
                             this._interactionService.Interact(
                                 ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                 viewModelEx =>
                                 {
                                     viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                     viewModelEx.Message =
                                         Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                 });
                             return;
                         }
                         //if (result.Equals(LightingCommandResult.REPAIR))
                         //{
                         //    this._interactionService.Interact(
                         //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                         //        viewModelRep =>
                         //        {
                         //            viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                         //            viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                         //        });
                         //    return;
                         //}
                     }
                 });
        }

        private bool CheckRepair()
        {
            var runtimeDevice = _runtimeDeviceViewModel.Model as IRuntimeDevice;
            return runtimeDevice != null && runtimeDevice.StartersOnDevice.Any((starter =>
            {
                return starter.IsInRepairState != null && starter.IsInRepairState.Value;
            }));
        }

    }
}
