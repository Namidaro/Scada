using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ULA.AddinsHost.ViewModel.Device.CustomItems;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Devices.Presentation.Runtime.CustomItems
{
  public  class CustomItemsViewModel:BindableBase, ICustomItemsViewModel
    {
        private bool _isSignalsEnabled;
        private bool _isIndicatorsEnabled;
        private bool _isCascadeEnabled;
        private List<ICascadeViewModel> _cascadeViewModels;
        private List<ISignalViewModel> _signalViewModels;
        private List<IIndicatorViewModel> _indicatorViewModels;
        private IDeviceCustomItems _model;


        public CustomItemsViewModel()
        {
            
        }



        #region Implementation of ICustomItemsViewModel

        public bool IsSignalsEnabled
        {
            get { return _isSignalsEnabled; }
            set
            {
                _isSignalsEnabled = value; 
                RaisePropertyChanged();
            }
        }

        public bool IsIndicatorsEnabled
        {
            get { return _isIndicatorsEnabled; }
            set
            {
                _isIndicatorsEnabled = value; 
                RaisePropertyChanged();
            }
        }

        public bool IsCascadeEnabled
        {
            get { return _isCascadeEnabled; }
            set
            {
                _isCascadeEnabled = value; 
                RaisePropertyChanged();
            }
        }

        public List<ICascadeViewModel> CascadeViewModels
        {
            get { return _cascadeViewModels; }
            set
            {
                _cascadeViewModels = value;
                RaisePropertyChanged();
            }
        }

        public List<ISignalViewModel> SignalViewModels
        {
            get { return _signalViewModels; }
            set
            {
                _signalViewModels = value;
                RaisePropertyChanged();
            }
        }

        public List<IIndicatorViewModel> IndicatorViewModels
        {
            get { return _indicatorViewModels; }
            set
            {
                _indicatorViewModels = value; 
                RaisePropertyChanged();
            }
        }

        public void Update()
        {
            CascadeViewModels?.ForEach((model => model.Update()));
            SignalViewModels?.ForEach((model => model.Update()));
            IndicatorViewModels?.ForEach((model => model.Update()));

        }

        public void SetLostConnection()
        {
            CascadeViewModels?.ForEach((model => model.SetLostConnaction()));
            SignalViewModels?.ForEach((model => model.SetLostConnaction()));
            IndicatorViewModels?.ForEach((model => model.SetLostConnaction()));
        }

        #endregion
    }
}
