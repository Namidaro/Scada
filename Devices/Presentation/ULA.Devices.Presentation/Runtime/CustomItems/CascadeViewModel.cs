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
   public class CascadeViewModel:BindableBase,ICascadeViewModel
    {
        private string _description;
        private bool? _state;
        private ICascade _model;

        #region Implementation of ICascadeViewModel

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        public bool? State
        {
            get { return _state; }
            set
            {
                if((_state!=null)&&(_state==value))return;
                _state = value;
                RaisePropertyChanged();
            }
        }

        public object Model
        {
            get { return _model; }
            set
            {
                _model = value as ICascade;
                Description = _model.SignalDescription;
            }
        }

        public void Update()
        {
            State = _model.SignalState;
        }

        public void SetLostConnaction()
        {
            State = null;
        }

        #endregion
    }
}
