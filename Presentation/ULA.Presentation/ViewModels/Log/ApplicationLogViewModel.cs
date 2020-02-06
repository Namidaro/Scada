using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Mvvm;
using ULA.Business.Infrastructure.DTOs;
using ULA.Presentation.DTOs;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Helpers;
using ULA.Presentation.Infrastructure.ViewModels.Log;
using ULA.Presentation.Views.Log;

namespace ULA.Presentation.ViewModels.Log
{
  public  class ApplicationLogViewModel:BindableBase, IApplicationLogViewModel
    {
        private readonly ILogLoadingHelper _logLoadingHelper;
        private ICollectionView _logMessageViewModels;
        private string _descriptionFilter;
        private DateTime _dateToFilter;
        private DateTime _dateFromFilter;
        private bool _isLoadingInProcess;
        private ObservableCollection<string> _availableOwners;
        private string _selectedOwner;
        private ObservableCollection<string> _availableMessageTypes;
        private string _selectedMessageType;

        private const string ALL = "Все";
        


        public ApplicationLogViewModel(ILogLoadingHelper logLoadingHelper)
        {
            _availableOwners=new ObservableCollection<string>();
            _logLoadingHelper = logLoadingHelper;
            _descriptionFilter=String.Empty;
            _dateToFilter=DateTime.Today;
            _dateFromFilter = DateTime.Today.AddDays(-14);
            _selectedOwner = ALL;
            _availableOwners.Add(ALL);
            _availableMessageTypes=new ObservableCollection<string>();
            _selectedMessageType = ALL;
        }


        #region Implementation of IApplicationLogViewModel

        public ICollectionView LogMessageViewModels
        {
            get { return _logMessageViewModels; }
        }

        public string DescriptionFilter
        {
            get { return _descriptionFilter; }
            set
            {
                _descriptionFilter = value;
                RaisePropertyChanged();
                FilterChanged();

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
               
                this._dateFromFilter = value;
               RaisePropertyChanged();
                FilterChanged();
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
            this._dateToFilter = value;
           RaisePropertyChanged();
                FilterChanged();
            }
        }




        /// <summary>
        ///     Метод изменения данных при фильтровании
        /// </summary>
        /// <param name="descriptionFilter">фильтр описания</param>
        /// <param name="dateFromFilter">дата с</param>
        /// <param name="dateToFilter">дата по</param>
        private void FilterChanged()
        {
            LogMessageViewModels.Filter = logArgument =>
            {
                var logInformation = logArgument as ILogMessageViewModel;
                if (logInformation == null)
                    return false;

                if (_selectedOwner != null)
                {
                    if (!_selectedOwner.Equals(ALL))
                    {
                        if (!logInformation.Owner.Equals(_selectedOwner)) return false;
                    }
                }


                if (_selectedMessageType != null)
                {
                    if (!_selectedMessageType.Equals(ALL))
                    {
                        if (!logInformation.LogMessageType.Equals(_selectedMessageType)) return false;
                    }
                }

                return ((_descriptionFilter == "" ||
                         logInformation.Message.ToUpper().Contains(_descriptionFilter.ToUpper())) &&
                        logInformation.MessageDateTime > _dateFromFilter && logInformation.MessageDateTime < _dateToFilter + new TimeSpan(1, 0, 0, 0)); // чтобы захватить весь этот день
            };

        }



        public async  void ShowLog()
        {
         
            ApplicationLogView logView=new ApplicationLogView();
            logView.DataContext = this;
            logView.Show();

            IsLoadingInProcess = true;
            try
            {
                await LoadMessages();

            }
            catch (Exception e)
            {
            }
            finally
            {
                IsLoadingInProcess = false;
                RaisePropertyChanged(nameof(LogMessageViewModels));
                RefreshAvailableItems();
            }
           
        }

        private void RefreshAvailableItems()
        {
            AvailableOwners.Clear();
            AvailableOwners.Add(ALL);
            AvailableOwners.AddRange(
                (_logMessageViewModels.SourceCollection as IEnumerable<ILogMessageViewModel>)
                .Select((model => model.Owner)).Distinct());
            SelectedOwner = ALL;

            AvailableMessageTypes.Clear();
            AvailableMessageTypes.Add(ALL);
            AvailableMessageTypes.AddRange(
                (_logMessageViewModels.SourceCollection as IEnumerable<ILogMessageViewModel>)
                .Select((model => model.LogMessageType)).Distinct());
            SelectedMessageType = ALL;


        }

        public bool IsLoadingInProcess
        {
            get { return _isLoadingInProcess; }
            set
            {
                _isLoadingInProcess = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> AvailableOwners
        {
            get { return _availableOwners; }
            set
            {
                _availableOwners = value;
                RaisePropertyChanged();
            }
        }

        public string SelectedOwner
        {
            get { return _selectedOwner; }
            set
            {
                _selectedOwner = value;
                RaisePropertyChanged();
                FilterChanged();
            }
        }

        public ObservableCollection<string> AvailableMessageTypes
        {
            get { return _availableMessageTypes; }
            set
            {
                _availableMessageTypes = value;
                RaisePropertyChanged();
            }
        }

        public string SelectedMessageType
        {
            get { return _selectedMessageType; }
            set
            {
                _selectedMessageType = value; 
                RaisePropertyChanged();
                FilterChanged();
            }
        }

        #endregion


        private async Task LoadMessages()
        {
            var mesages = await _logLoadingHelper.LoadLogMessageViewModels();
          mesages=  mesages.OrderBy(c => c.MessageDateTime.Date)
                .ThenBy(c => c.MessageDateTime.TimeOfDay);
            _logMessageViewModels = CollectionViewSource.GetDefaultView(mesages);
         
        }
    }
}
