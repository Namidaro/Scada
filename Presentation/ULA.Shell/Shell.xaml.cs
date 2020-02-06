using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using FirstFloor.ModernUI;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.Utility;
using NLog;
using NLog.Targets;
using ULA.Interceptors.Application;
using ULA.Log;
using ULA.Log.LoggerService;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Services;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.SharedResourses.Controls;
using MessageBoxResult = ULA.Presentation.Infrastructure.ViewModels.MessageBoxResult;

namespace ULA.Shell
{
    /// <summary>
    ///     Interaction logic for Shell.xaml
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
    public partial class Shell
    {
        #region [PrivateField]
        
        private InteractionService _interactionService;
        private IApplicationSettingsService _settings;
        private bool _isClose;
        private Mutex _mutex;

        #endregion [PrivateField]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="Shell" />
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Нельзя"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "запустить"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "больше"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "приложения"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "экземпляров"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "трех")]
        public Shell(InteractionService service, IApplicationSettingsService settings)
        {
            InitializeComponent();
            Guard.ArgumentNotNull(service, "Interaction Service");
            Guard.ArgumentNotNull(settings, "settings");
            this._interactionService = service;
            this._settings = settings;
            this._isClose = false;

            var countApplication = 0;
            var isInitialize = false;
            while (countApplication<3)
            {
                var mutexName = String.Format(CultureInfo.CurrentCulture,
                "Ula_app{0}", countApplication);
                _mutex = new Mutex(false, mutexName);
                try
                {
                    if (_mutex.WaitOne(0, false))
                    {
                        // Ok number application
                        isInitialize = true;
                        break;
                    }
                }
                finally
                {
                    if (!isInitialize)
                    {
                        countApplication++;
                        if (_mutex != null)
                        {
                            _mutex.Close();
                            _mutex = null;
                        }
                    }
                }
            }

            if (!isInitialize)
            {
                throw new Exception("Нельзя запустить больше трех экземпляров приложения");
            }

            //var countApplication = System.Diagnostics.Process.GetProcessesByName(
            //        System.IO.Path.GetFileNameWithoutExtension(
            //            System.Reflection.Assembly.GetEntryAssembly().Location)).Count();
            this._settings.ApplicationNumber = countApplication;
            
            FileTarget target = (FileTarget)LogManager.Configuration.FindTargetByName("file");
            target.FileName = "${basedir}/nlog_${date:format=MM}_${date:format=yyyy}_app" + countApplication + ".txt";
            LogManager.ReconfigExistingLoggers();
            (new LoggerServiceBase()).LogMessage("Включение приложения", LogManager.GetLogger(LogMessageTypes.COMMEN_MESSAGE), LogMessageTypes.COMMEN_MESSAGE);
        }

        #endregion [Ctor's]

        #region [EventHandlers]

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Вы")]
        private void Shell_OnClosing(object sender, CancelEventArgs e)
        {
            if (!_isClose)
            {
                e.Cancel = true;
            }
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Закрыть СУНО БЭМН?";
                    viewModel.Message = "Вы уверены?";
                },
                viewModel =>
                {
                    if (viewModel.Result == MessageBoxResult.YES)
                    {
                        (new LoggerServiceBase()).LogMessage("Закрытие приложения", LogManager.GetLogger(LogMessageTypes.COMMEN_MESSAGE), LogMessageTypes.COMMEN_MESSAGE);
                        this._isClose = true;

                        try
                        {
                            (ServiceLocator.Current.GetInstance(typeof(ISoundNotificationsService)) as
                                ISoundNotificationsService).ReleaseAllAndStopSound();
                        }
                        catch
                        {
                        }


                        (sender as MainViewBase).Close();

                        if (_mutex != null)
                        {
                            _mutex.Close();
                            _mutex = null;
                        }
                    }
                    else
                    {
                        this._isClose = false;
                    }
                });
        }

        #endregion [EventHandlers]

        private void Shell_OnActivated(object sender, EventArgs e)
        {
            //int height = 0, width=0;
            //foreach (System.Windows.Forms.Screen screen in System.Windows.Forms.Screen.AllScreens)
            //{
            //    // height += screen.WorkingArea.Height; <-- Changed this line  
            //    // If you want to make sure your largest screen height is used  
            //    // put some logic in here to check the value of height against  
            //    // screen.WorkingArea.Height in the foreach loop.   
            //    // otherwise the value of height will be the rightmost screen  
            //    // in your system setup.  
            //    height = screen.WorkingArea.Height;
            //    width += screen.WorkingArea.Width;
            //}

            //this.WindowState = WindowState.Normal;
            //Top = 0;
            //Left = 0;
            ////this.Height = height;
            ////this.Width = 1000;


            ////this.WindowStyle = WindowStyle.None;
            ////this.ResizeMode = ResizeMode.CanMinimize;  
        }
    }
}