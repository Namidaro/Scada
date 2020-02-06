using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ULA.Interceptors.Application;
using ULA.Localization;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.SharedResourses.Common;

namespace ULA.Presentation.ViewModels
{
    /// <summary>
    ///     Represents the root view model
    ///     Представляет корневую вью-модель
    /// </summary>
    public class RootViewModel : ViewModelBase, IRootViewModel
    {
        #region [Fields]

        private readonly IInteractionService _interactionService;
        private readonly IApplicationModeViewModelFactory _modeFactory;
        private readonly IApplicationStateManager _stateManager;
        private readonly ISoundNotificationsService _soundNotificationsService;
        private ICommand _changeModeToConfigurationCommand;
        private ICommand _changeModeToRuntimeCommand;
        private IApplicationModeViewModel _currentApplicationModeViewModel;
        private ICommand _initializeCommand;
        private bool _isInitializingMode;
        private int _imageZIndex;
        #endregion [Fields]

        #region [Properties]

        /// <summary>
        ///     Gets an instance of <see cref="IApplicationModeViewModel" /> that represents current mode view model
        ///     Вью-модель текущего режима приложения(конфиг/реального времени/ошмбка)
        /// </summary>
        public IApplicationModeViewModel ApplicationMode
        {
            get { return this._currentApplicationModeViewModel; }
        }
        /// <summary>
        /// положение картинки поверх или под остальным содержимым
        /// </summary>
        public int ImageZIndex
        {
            get { return _imageZIndex; }
            set
            {
                _imageZIndex = value;
                OnPropertyChanged(nameof(ImageZIndex));
            }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="RootViewModel" />
        /// </summary>
        /// <param name="modeFactory">
        ///     An instance of <see cref="IApplicationModeViewModelFactory" /> to use
        /// </param>
        /// <param name="interactionService">
        ///     An instance of <see cref="IInteractionService" /> to use
        /// </param>
        /// <param name="stateManager">
        ///     An instance of <see cref="IApplicationStateManager" /> to use
        /// </param>
        /// <param name="soundNotificationsService"></param>
        public RootViewModel(IApplicationModeViewModelFactory modeFactory,
                             IInteractionService interactionService,
                             IApplicationStateManager stateManager,ISoundNotificationsService soundNotificationsService)
        {
            this._modeFactory = modeFactory;
            this._interactionService = interactionService;
            this._stateManager = stateManager;
            _soundNotificationsService = soundNotificationsService;
        }

        #endregion [Ctor's]

        #region [IRootViewModel members]

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents an initialization action
        ///     Команда инициализации
        /// </summary>
        public ICommand InitializeCommand
        {
            get { return this._initializeCommand ?? (this._initializeCommand = new AsyncCommand(OnInitialize)); }
        }

        /// <summary>
        ///     Gets an istance of <see cref="ICommand" /> that represents an action of changing current application mode to
        ///     configuration mode
        ///     Команда изменения режима на режим конфигурации
        /// </summary>
        public ICommand ChangeModeToConfigurationCommand
        {
            get
            {
                return this._changeModeToConfigurationCommand ??
                       (this._changeModeToConfigurationCommand = new AsyncCommand(OnChangeModeToConfiguration));
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represent an action of changing current application mode top
        ///     runtime mode
        ///     Команда изменения режима на режим реального времени
        /// </summary>
        public ICommand ChangeModeToRuntimeCommand
        {
            get
            {
                return this._changeModeToRuntimeCommand ??
                       (this._changeModeToRuntimeCommand = new AsyncCommand(OnChangeModeToRuntime));
            }
        }

        #endregion [IRootViewModel members]

        #region [Help members]

        private async Task OnInitialize()
        {
            ImageZIndex = 6;
            var currentState = this._stateManager.CurrentState;
            await ChangeStateTo(currentState.CurrentState);
        }

        private IApplicationModeViewModel CreateModeFromApplicationState(ApplicationState state)
        {
            Func<IApplicationModeViewModel> factory;
            switch (state)
            {
                case ApplicationState.RUNTIME:
                    factory = this._modeFactory.CreateRuntimeModeViewModel;
                    break;
                case ApplicationState.CONFIGURATION:
                    factory = this._modeFactory.CreateConfigurationModeViewModel;
                    break;
                default:
                    throw new NotSupportedException();
            }
            return factory();
        }

        private async Task ChangeStateTo(ApplicationState state)
        {
            this._stateManager.GotToNewState(state);
            var mode = CreateModeFromApplicationState(state);
            this._isInitializingMode = true;
            var interactionSessionToken = CreateBusyInteractionSession();

            Exception modeChangingException = null;
            var isNewModeAlreadyInitialized = false;
            try
            {
                if (this._currentApplicationModeViewModel != null)
                    await this._currentApplicationModeViewModel.UninitializeAsync();
                var currentMode = mode;
                await currentMode.InitializeAsync();
                this._currentApplicationModeViewModel = currentMode;
                isNewModeAlreadyInitialized = true;
                OnModeChanged();
                this._isInitializingMode = false;
                interactionSessionToken.Dispose();
                currentMode.OnInitialized();
            }
            catch (Exception error)
            {
                modeChangingException = error;
                interactionSessionToken.Dispose();
            }
            if (modeChangingException == null) return;
            this._stateManager.GotToNewState(ApplicationState.FATAL_ERROR);
            var failureMode = this._modeFactory.CreateFailureModeViewModel(modeChangingException);
            if (isNewModeAlreadyInitialized)
                await this._currentApplicationModeViewModel.UninitializeAsync();
            await failureMode.InitializeAsync();
            this._currentApplicationModeViewModel = failureMode;
            this._isInitializingMode = false;
        }

        private async Task OnChangeModeToRuntime()
        {
          
            if (!CanChangeMode()) return;
            if (this._currentApplicationModeViewModel is IApplicationRuntimeModeViewModel) return;
  ImageZIndex = 4;
            await ChangeStateTo(ApplicationState.RUNTIME);
        }

        private IInteractionToken CreateBusyInteractionSession()
        {
            return this._interactionService.Interact(ApplicationInteractionProviders.BusyInteraction,
                                                     this.InitializeBusyDialog);
        }

        private void InitializeBusyDialog(IBusyInteractionViewModel dialog)
        {
            dialog.Title = LocalizationResources.Instance.BusyDialogTitle;
            dialog.Message = LocalizationResources.Instance.WaitingBusyDialogMessage;
        }

        private async Task OnChangeModeToConfiguration()
        {
            if (!CanChangeMode()) return;
            if (this._currentApplicationModeViewModel is IApplicationConfigurationModeViewModel) return;
            _soundNotificationsService.ReleaseAllAndStopSound();
            await ChangeStateTo(ApplicationState.CONFIGURATION);
        }

        private bool CanChangeMode()
        {
            if (this._isInitializingMode) return false;
            return !(this._currentApplicationModeViewModel is IApplicationFailureModeViewModel);
        }

        private void OnModeChanged()
        {
            OnPropertyChanged("ApplicationMode");

        }

        #endregion [Help members]
    }
}