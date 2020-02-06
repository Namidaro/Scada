using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using ULA.CompositionRoot;
using ULA.Interceptors.Application;
using ULA.Shell.Bootstrapping;

namespace ULA.Shell
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region [Private fields]

        private IApplicationContentContainer _applicationContent;

        #endregion [Private fields]

        #region [Override members]

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">
        ///     A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.
        /// </param>
        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "exception")]
        protected override void OnStartup(StartupEventArgs e)
        {
            var stateManager = new DefaultApplicationStateManger();
            stateManager.GotToNewState(ApplicationState.BOOTSTRAPPING);
            try
            {
                base.OnStartup(e);
                ApplicationBootstrapper bootstrapper = new ApplicationBootstrapper(stateManager);
                bootstrapper.Run();
                this._applicationContent = bootstrapper.ContentContainer;
                stateManager.GotToNewState(ApplicationState.RUNTIME);
                Current.MainWindow = (Window)bootstrapper.CurrentShell;
                Current.MainWindow.Show();
            }
#pragma warning disable 168
            catch (Exception exception)
#pragma warning restore 168
            {
                stateManager.GotToNewState(ApplicationState.FATAL_ERROR);
                /*TODO: do some work here on notifying a user about the failure*/
                throw;
            }
        }


        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Application.Exit" /> event.
        /// </summary>
        /// <param name="e">
        ///     An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.
        /// </param>
        protected override void OnExit(ExitEventArgs e)
        {
            this._applicationContent.Dispose();
            this._applicationContent = null;
            base.OnExit(e);
        }



        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            string message = $"Возникла ошибка : {e.Exception.Message}\n\n\n Рекомендуется перезапустить приложение." +
                             $"\n Ошибка сохранена в лог-файл ErrorLog.txt в папке с приложением. " +
                             $"\n Пожалуйста, свяжитесь с разработчиком и вышлите этот файл.";
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //Current.Shutdown();
            try
            {

                using (StreamWriter writer =
                    new StreamWriter(new FileStream("ErrorLog.txt", FileMode.Append | FileMode.OpenOrCreate)))
                {
                    writer.WriteLine();
                    writer.WriteLine();
                    writer.WriteLine($"[{DateTime.Now.ToString()}] Exception");
                    writer.WriteLine(e.Exception.Message);
                    writer.WriteLine(e.Exception.StackTrace);
                }
            }
            catch
            {

            }



        }

        #endregion [Override members]
    }
}