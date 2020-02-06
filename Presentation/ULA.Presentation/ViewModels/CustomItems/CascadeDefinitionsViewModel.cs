using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;
using ULA.Presentation.Infrastructure.ViewModels.CustomItems;

namespace ULA.Presentation.ViewModels.CustomItems
{
   public class CascadeDefinitionsViewModel : BindableBase, ICascadeDefinitionsViewModel
    {
        private string _description;
        private int _resistorModule;
        private int _resistorNumber;
        private ICascadeIndicator _model;

        #region Implementation of ISignalDefinitionsViewModel

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        public int ResistorModule
        {
            get { return _resistorModule; }
            set
            {
                _resistorModule = value;
                RaisePropertyChanged();
            }
        }

        public int ResistorNumber
        {
            get { return _resistorNumber; }
            set
            {
                _resistorNumber = value;
                RaisePropertyChanged();
            }
        }

        public ICascadeIndicator Model
        {
            get
            {
                _model.ResistorModule = ResistorModule;
                _model.ResistorNumber = ResistorNumber;
                _model.Tag = Description;
                return _model;

            }
            set
            {
                _model = value;
                ResistorNumber = _model.ResistorNumber;
                ResistorModule = _model.ResistorModule;
                Description = _model.Tag;
            }
        }

        #endregion
    }
}
