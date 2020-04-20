using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DTOs;
using ULA.Presentation.DTOs;
using ULA.Presentation.Infrastructure.DTOs;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.Views.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a log interaction view model
    ///     Представляет вью-модель журнала системы
    /// </summary>
    public class LogInteractionViewModel : InteractionDialogViewModel, ILogInteractionViewModel
    {
        private readonly IDeviceLogLoadingService _deviceLogLoadingService;
        private readonly IInteractionService _interactionService;
        private bool _isCancelRequested=false;
        #region [Fields]

        private string _title = string.Empty;
        private string _descriptionFilter = string.Empty;
        private DateTime _dateFromFilter;
        private DateTime _dateToFilter;
        private IRuntimeDeviceViewModel _runtimeDeviceViewModel;
        private ICollectionView _logCollection;
        private bool _isLoadingInProcess;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Create a instance of <see cref="LogInteractionViewModel" />
        /// </summary>
        public LogInteractionViewModel(IDeviceLogLoadingService deviceLogLoadingService, IInteractionService interactionService)
        {
            _deviceLogLoadingService = deviceLogLoadingService;
            _interactionService = interactionService;
            this.LogCollection = CollectionViewSource.GetDefaultView(new ObservableCollection<ILogInformation>
            {
              //  new LogInformation("Start", new DateTime(2014, 11, 12))
            });
            this.DateFromFilter = DateTime.Today.AddDays(-14);
            this.DateToFilter = DateTime.Today;
            RefreshCommand = new DelegateCommand(OnRefreshExecute);
            SaveJournalToFileCommand = new DelegateCommand(OnSaveJournalToFileExecute);
        }

        private void OnSaveJournalToFileExecute()
        {
            using (System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog())
            {
                sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.RestoreDirectory = false;
                sfd.FileName = DateTime.Now.ToShortDateString() + " " + this._runtimeDeviceViewModel.Model.DeviceDescription;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(new System.IO.FileStream(sfd.FileName, System.IO.FileMode.OpenOrCreate)))
                        {

                            foreach (var item in this.LogCollection.SourceCollection)
                            {
                                writer.Write((item as ILogInformation).ActionDate.ToString() + " ");
                                writer.WriteLine((item as ILogInformation).ActionDescription);
                            }
                            writer.Close();
                        }
                        this._interactionService
                        .Interact(ApplicationInteractionProviders.WarningMessageBoxInteractionProvider,
                        viewModel =>
                        {
                            viewModel.ButtonType = Infrastructure.ViewModels.MessageBoxButtonType.OK;
                            viewModel.Title = "Информация";
                            viewModel.Message = "Журнал сохранен";
                        },
                        viewModel => { });
                    }
                    catch (Exception)
                    {
                        this._interactionService
                        .Interact(ApplicationInteractionProviders.WarningMessageBoxInteractionProvider,
                        viewModel =>
                        {
                            viewModel.ButtonType = Infrastructure.ViewModels.MessageBoxButtonType.OK;
                            viewModel.Title = "Информация";
                            viewModel.Message = "При сохранении журнала возникла ошибка";
                        },
                        viewModel => { });
                    }
                }
            }
        }

        private async void OnRefreshExecute()
        {
            _deviceLogLoadingService.Initialize(_runtimeDeviceViewModel.Model as IRuntimeDevice);
            var logList = new List<ILogInformation>();
            _isCancelRequested = false;
            LogCollection = CollectionViewSource.GetDefaultView(new ObservableCollection<ILogInformation>());
            IsLoadingInProcess = true;
            do
            {
                if (_isCancelRequested)
                {
                    IsLoadingInProcess = false;

                    return;
                }
                try
                {
                    logList = await _deviceLogLoadingService.ReadNextLogFromDevice();
                }
                catch (Exception e)
                {

                }
                if (_isCancelRequested)
                {
                    IsLoadingInProcess = false;

                    return;
                }
                if (logList != null)
                {
                    LogCollection = CollectionViewSource.GetDefaultView(
                        new ObservableCollection<ILogInformation>(logList));
                }



            } while (logList != null);
            IsLoadingInProcess = false;
        }

        #endregion [Ctor's]

        #region [Help members]

        /// <summary>
        ///     Метод изменения данных при фильтровании
        /// </summary>
        /// <param name="descriptionFilter">фильтр описания</param>
        /// <param name="dateFromFilter">дата с</param>
        /// <param name="dateToFilter">дата по</param>
        private void FilterChanged(string descriptionFilter, DateTime dateFromFilter, DateTime dateToFilter)
        {
            LogCollection.Filter = logArgument =>
            {
                var logInformation = logArgument as LogInformation;
                if (logInformation == null)
                    return false;

                return ((descriptionFilter == "" ||
                         logInformation.ActionDescription.ToUpper().Contains(descriptionFilter.ToUpper())) &&
                        logInformation.ActionDate > dateFromFilter && logInformation.ActionDate < (dateToFilter + new TimeSpan(1, 0, 0, 0))); // чтобы захватить весь этот день
            };

        }

        #endregion [Help members]

        #region [ILogInteractionViewModel mebers]

        /// <summary>
        ///     Gets or sets the title of current interaction request
        ///     Заголовок
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set
            {
                if (this._title.Equals(value))
                    OnPropertyChanging("Title");
                this._title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="ObservableCollection{T}" />, where T is <see cref="ILogInformation" />
        ///     Коллекция логов(журнал)
        /// </summary>
        public ICollectionView LogCollection
        {
            get { return _logCollection; }
            set
            {
                _logCollection = value;
                OnPropertyChanged(nameof(LogCollection));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICommand RefreshCommand { get; } 
        /// <summary>
        /// 
        /// </summary>
        public ICommand SaveJournalToFileCommand { get; }

        /// <summary>
        ///     Gets an instance of <see cref="string" /> that represents description filter
        ///     Строка - фильтр описания действия
        /// </summary>
        public string DescriptionFilter
        {
            get { return this._descriptionFilter; }
            set
            {
                if (this._descriptionFilter.Equals(value))
                    OnPropertyChanging("DescriptionFilter");
                this._descriptionFilter = value;
                OnPropertyChanged("DescriptionFilter");
                FilterChanged(value, this._dateFromFilter, this._dateToFilter);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="DateTime" /> that represents from date and time filter
        ///     Фильтр - "Дата с"
        /// </summary>
        public DateTime DateFromFilter
        {
            get { return this._dateFromFilter; }
            set
            {
                if (this._dateFromFilter.Equals(value))
                    OnPropertyChanging("DateFromFilter");
                this._dateFromFilter = value;
                var a = new DatePicker();
                OnPropertyChanged("DateFromFilter");
                FilterChanged(this._descriptionFilter, value, this._dateToFilter);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="DateTime" /> that represents to from date and time filter
        ///     Фильтр - "Дата по"
        /// </summary>
        public DateTime DateToFilter
        {
            get { return this._dateToFilter; }
            set
            {
                if (this._dateToFilter.Equals(value))
                    OnPropertyChanging("DateToFilter");
                this._dateToFilter = value;
                OnPropertyChanged("DateToFilter");
                FilterChanged(this._descriptionFilter, this._dateFromFilter, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsLoadingInProcess
        {
            get { return _isLoadingInProcess; }
            set
            {
                _isLoadingInProcess = value;
                OnPropertyChanged(nameof(IsLoadingInProcess));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDeviceViewModel"></param>
        public  void OpenDeviceLog(IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {

            _runtimeDeviceViewModel = runtimeDeviceViewModel;


            var logWnd = new Window();
            logWnd.Owner = Application.Current.MainWindow;
            var logView = new LogInteractionView();
            var vm = this;
            logView.DataContext = vm;
            logView.Margin = new Thickness(5, 5, 5, 5);
            logWnd.Content = logView;
            logWnd.Title = "Журнал устройства";
            logWnd.ResizeMode = ResizeMode.CanMinimize;
            logWnd.Height = 625;
            logWnd.Width = 670;
            logWnd.Closing += (o,e) =>
            {
                //this.IsLoadingInProcess = false;
                OnCanceling();
            };
            logWnd.Show();


            if (LogCollection.IsEmpty)OnRefreshExecute();
            //if (logList.Count == 0)
            //{
            //    logWnd.Close();
            //    this._interactionService.Interact(
            //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
            //        viewModel =>
            //        {
            //            viewModel.Title = "Журнал устройства";
            //            viewModel.Message = "Журнал пуст";
            //        });
            //}

        }

        #endregion [ILogInteractionViewModel mebers]

        #region [Override members]

        /// <summary>
        ///     Canceling current interaction dialog
        ///     Обработчик отмены
        /// </summary>
        protected override void OnCanceling()
        {
            if (System.Windows.Application.Current.MainWindow.OwnedWindows.Count != 0)
            {
                foreach (Window ownedWindow in System.Windows.Application.Current.MainWindow.OwnedWindows)
                {
                    try
                    {
                        ownedWindow.Close();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
                IsLoadingInProcess = false;
                _isCancelRequested = true;
            }
            base.OnCanceling();
        }


        #region Overrides of Disposable
        /// <summary>
        /// 
        /// </summary>
        protected override void OnDisposing()
        {
            base.OnDisposing();
        }

        #endregion

        #endregion [Override members]
    }
}