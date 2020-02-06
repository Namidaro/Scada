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
    /// <summary>
    /// 
    /// </summary>
  public  class FiderDefinitionsViewModel:BindableBase, IFiderDefinitionsViewModel
    {
        private bool _isEnabledResistor1;
        private bool _isEnabledResistor2;
        private bool _isEnabledResistor3;
        private int _moduleResistor1;
        private int _moduleResistor2;
        private int _moduleResistor3;
        private int _numberResistor1;
        private int _numberResistor2;
        private int _numberResistor3;
        private ICustomFider _fiderDefinitionModel;
        private string _tagName;

        #region Implementation of IFiderDefinitionsViewModel
        /// <summary>
        /// 
        /// </summary>
        public string TagName
        {
            get { return _tagName; }
            set
            {
                _tagName = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabledResistor1
        {
            get { return _isEnabledResistor1; }
            set
            {
                _isEnabledResistor1 = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabledResistor2
        {
            get { return _isEnabledResistor2; }
            set
            {
                _isEnabledResistor2 = value;
                RaisePropertyChanged();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabledResistor3
        {
            get { return _isEnabledResistor3; }
            set
            {
                _isEnabledResistor3 = value;
                RaisePropertyChanged();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModuleResistor1
        {
            get { return _moduleResistor1; }
            set
            {
                _moduleResistor1 = value;
                RaisePropertyChanged();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModuleResistor2
        {
            get { return _moduleResistor2; }
            set
            {
                _moduleResistor2 = value;
                RaisePropertyChanged();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModuleResistor3
        {
            get { return _moduleResistor3; }
            set
            {
                _moduleResistor3 = value;
                RaisePropertyChanged();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NumberResistor1
        {
            get { return _numberResistor1; }
            set
            {
                _numberResistor1 = value;
                RaisePropertyChanged();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NumberResistor2
        {
            get { return _numberResistor2; }
            set
            {
                _numberResistor2 = value;
                RaisePropertyChanged();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NumberResistor3
        {
            get { return _numberResistor3; }
            set
            {
                _numberResistor3 = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICustomFider FiderDefinitionModel
        {
            get
            {
                _fiderDefinitionModel.IsEnabledResistor1 = IsEnabledResistor1;
                _fiderDefinitionModel.IsEnabledResistor2 = IsEnabledResistor2;
                _fiderDefinitionModel.IsEnabledResistor3 = IsEnabledResistor3;
                _fiderDefinitionModel.ModuleResistor1 = ModuleResistor1;
                _fiderDefinitionModel.ModuleResistor2 = ModuleResistor2;
                _fiderDefinitionModel.ModuleResistor3 = ModuleResistor3;
                _fiderDefinitionModel.NumberResistor1 = NumberResistor1;
                _fiderDefinitionModel.NumberResistor2 = NumberResistor2;
                _fiderDefinitionModel.NumberResistor3 = NumberResistor3;
                _fiderDefinitionModel.Tag = TagName;
                return _fiderDefinitionModel;
            }
            set
            {
                _fiderDefinitionModel = value;
                IsEnabledResistor1 =_fiderDefinitionModel.IsEnabledResistor1;
                IsEnabledResistor2 = _fiderDefinitionModel.IsEnabledResistor2;
                IsEnabledResistor3 = _fiderDefinitionModel.IsEnabledResistor3;
                ModuleResistor1 = _fiderDefinitionModel.ModuleResistor1;
                ModuleResistor2 = _fiderDefinitionModel.ModuleResistor2;
                ModuleResistor3 = _fiderDefinitionModel.ModuleResistor3;
                NumberResistor1 = _fiderDefinitionModel.NumberResistor1;
                NumberResistor2 = _fiderDefinitionModel.NumberResistor2;
                NumberResistor3 = _fiderDefinitionModel.NumberResistor3;
                TagName = _fiderDefinitionModel.Tag;
            }
        }

        #endregion
    }
}
