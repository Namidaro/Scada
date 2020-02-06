using Microsoft.Practices.ServiceLocation;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.Services.Interactions
{
    /// <summary>
    ///     Represents default application based interaction providers registry
    ///      ласс предоставл€ет статические методы, которые возвращают провайдеры определенных интерактиных диалогов
    /// </summary>
    public class ApplicationInteractionProviders
    {
        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current system busy interaction
        ///     request
        ///     ¬озвращает провайдер диалога ожидани€
        /// </summary>
        public static IInteractionProvider<IBusyInteractionViewModel> BusyInteraction
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current.GetInstance<IInteractionProvider<IBusyInteractionViewModel>>(
                    ApplicationGlobalNames.BUSY_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current system create new
        ///     deviceViewModel request
        ///     ¬озвращает провайдер диалога создани€ нового устройства
        /// </summary>
        public static IInteractionProvider<IModifyDeviceViewModel> CreateNewDeviceProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current.GetInstance<IInteractionProvider<IModifyDeviceViewModel>>(
                    ApplicationGlobalNames.CREATE_NEW_DEVICE_PROVIDER_NAME);
            }
        }
        

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current system error messagebox
        ///     interaction request
        ///     ¬озвращает провайдер диалога ошибки
        /// </summary>
        public static IInteractionProvider<IErrorMessageBoxInteractionViewModel> ErrorMessageBoxInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current.GetInstance<IInteractionProvider<IErrorMessageBoxInteractionViewModel>>(
                    ApplicationGlobalNames.ERROR_MESSAGEBOX_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current system information
        ///     messagebox interaction request
        ///     ¬озвращает провайдер диалога информации
        /// </summary>
        public static IInteractionProvider<IInformationMessageBoxInteractionViewModel>
            InformationMessageBoxInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current
                    .GetInstance<IInteractionProvider<IInformationMessageBoxInteractionViewModel>>(
                        ApplicationGlobalNames.INFORMATION_MESSAGEBOX_INTERACTION_PROVIDER_NAME);
            }
        }




        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current system information
        ///     messagebox interaction request
        ///     ¬озвращает провайдер диалога информации
        /// </summary>
        public static IInteractionProvider<IExtraSettingsViewModel>
            ExtraSettingsInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current
                    .GetInstance<IInteractionProvider<IExtraSettingsViewModel>>(
                        ApplicationGlobalNames.EXTRASETTINGS_INTERACTION_PROVIDER_NAME);
            }
        }




        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current system question
        ///     messagebox interaction request
        ///     ¬озвращает провайдер диалога вопроса
        /// </summary>
        public static IInteractionProvider<IQuestionMessageBoxInteractionViewModel>
            QuestionMessageBoxInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current.GetInstance<IInteractionProvider<IQuestionMessageBoxInteractionViewModel>>
                    (
                        ApplicationGlobalNames.QUESTION_MESSAGEBOX_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current system warning
        ///     messagebox interaction request
        ///     ¬озвращает провайдер диалога предупреждени€
        /// </summary>
        public static IInteractionProvider<IWarningMessageBoxInteractionViewModel> WarningMessageBoxInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current.GetInstance<IInteractionProvider<IWarningMessageBoxInteractionViewModel>>(
                    ApplicationGlobalNames.WARNING_MESSAGEBOX_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current citywide
        ///     commands interaction request
        ///     ¬озвращает провайдер диалога общегородских команд
        /// </summary>
        public static IInteractionProvider<ICitywideCommandsInteractionViewModel> CitywideCommandsInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current.GetInstance<IInteractionProvider<ICitywideCommandsInteractionViewModel>>(
                    ApplicationGlobalNames.CITYWIDE_COMMANDS_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///  Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current command on templates interaction request
        ///     ¬озвращает провайдер диалога команд по шаблонам
        /// </summary>
        public static IInteractionProvider<ICommandOnTemplateInteractionViewModel> CommandOnTemplatesInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current.GetInstance<IInteractionProvider<ICommandOnTemplateInteractionViewModel>>(
                    ApplicationGlobalNames.COMMAND_ON_TEMPLATES_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current about
        ///     program commands interaction request
        ///     ¬озвращает провайдер диалога "ќ программе"
        /// </summary>
        public static IInteractionProvider<IAboutInteractionViewModel> AboutInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return
                    ServiceLocator.Current.GetInstance<IInteractionProvider<IAboutInteractionViewModel>>(
                        ApplicationGlobalNames.ABOUT_INTERACTION_PROVIDER_NAME);
            }
        }







        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current log
        ///     interaction request
        ///     ¬озвращает провайдер диалога "∆урнал системы"
        /// </summary>
        public static IInteractionProvider<ILogInteractionViewModel> LogInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return
                    ServiceLocator.Current.GetInstance<IInteractionProvider<ILogInteractionViewModel>>(
                        ApplicationGlobalNames.LOG_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current log
        ///     synchronization time interaction request
        ///     ¬озвращает провайдер диалога синхронизации времени
        /// </summary>
        public static IInteractionProvider<ISynchronizationTimeInteractionViewModel>
            SynchronizationTimeInteractionProvider
        {
            get
            {
                if(ServiceLocator.Current == null) return null;
                return ServiceLocator.Current
                    .GetInstance<IInteractionProvider<ISynchronizationTimeInteractionViewModel>>(
                        ApplicationGlobalNames.SYNCHRONIZATION_TIME_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current 
        ///     update timeout interaction request
        ///     ¬озвращает провайдер диалога редактировани€ периода обновлений
        /// </summary>
        public static IInteractionProvider<IUpdateTimeoutInteractionViewModel> UpdateTimeoutInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current.GetInstance<IInteractionProvider<IUpdateTimeoutInteractionViewModel>>(
                    ApplicationGlobalNames.UPDATE_TIMEOUT_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current 
        ///     passworg validation interaction request
        ///     ¬озвращает провайдер диалога парол€ обновлений
        /// </summary>
        public static IInteractionProvider<IPasswordInteractionViewModel> PasswordInteractionProvider
        {
            get
            {
                if (ServiceLocator.Current == null) return null;
                return ServiceLocator.Current.GetInstance<IInteractionProvider<IPasswordInteractionViewModel>>(
                    ApplicationGlobalNames.PASSWORD_INTERACTION_PROVIDER_NAME);
            }
        }




    }
}