using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a CommandOnTemplates interaction view model
    ///     Представляет вью-модель окна "Управление по шаблонам"
    /// </summary>
    public class CommandOnTemplateInteractionViewModel : InteractionDialogViewModel,
        ICommandOnTemplateInteractionViewModel
    {
        #region [Fields]

        private ICommand _editTemplateCommand;
        private ICommand _saveTemplateCommand;
        private ICommand _saveAsTemplateCommand;
        private ICommand _deleteTemplateCommand;
        private ICommand _executeTemplateCommand;
        private ICommand _exitWindowCommand;

        private bool IsNightLightToBeEnabled;
        private bool IsEveningLightToBeEnabled;
        private bool IsFullLightToBeEnabled;
        private bool IsSublightToBeEnabled;
        private bool IsIlluminationToBeEnabled;
        private bool IsEnergySafeToBeEnabled;
        private bool IsAutoModeToBeEnabled;
        private bool IsManualModeToBeEnabled;

        private List<string> TemplatesList;
        private string TemplateListSelectedItem;
        private string _commandOnTemplateTitle;

        #endregion



        #region [Members]
        public string Title
        {
            get { return this._commandOnTemplateTitle; }
            set
            {
                if (this._commandOnTemplateTitle.Equals(value))
                    OnPropertyChanging("Title");
                this._commandOnTemplateTitle = value;
                OnPropertyChanged("Title");
            }
        }

        public ICommand EditTemplateCommand
        {
            get
            {
                return this._editTemplateCommand ??
                       (this._editTemplateCommand = new DelegateCommand(OnEditTemplateCommand));
            }
        }

        private void OnEditTemplateCommand()
        {
            throw new NotImplementedException();
        }

        public ICommand SaveTemplateCommand
        {
            get
            {
                return this._saveTemplateCommand ??
                       (this._saveTemplateCommand = new DelegateCommand(OnSaveTemplateCommand));
            }
        }

        private void OnSaveTemplateCommand()
        {
            throw new NotImplementedException();
        }

        public ICommand SaveAsTemplateCommand
        {
            get
            {
                return this._saveAsTemplateCommand ??
                       (this._saveAsTemplateCommand = new DelegateCommand(OnSaveAsTemplateCommand));
            }
        }

        private void OnSaveAsTemplateCommand()
        {
            throw new NotImplementedException();
        }

        public ICommand DeleteTemplateCommand
        {
            get
            {
                return this._deleteTemplateCommand ??
                       (this._deleteTemplateCommand = new DelegateCommand(OnDeleteTemplateCommand));
            }
        }

        private void OnDeleteTemplateCommand()
        {
            throw new NotImplementedException();
        }

        public ICommand ExecuteTemplateCommand
        {
            get
            {
                return this._executeTemplateCommand ??
                       (this._executeTemplateCommand = new DelegateCommand(OnExecuteTemplateCommand));
            }
        }

        private void OnExecuteTemplateCommand()
        {
            throw new NotImplementedException();
        }


        public ICommand ExitWindowCommand
        {
            get
            {
                return this._exitWindowCommand ??
                       (this._exitWindowCommand = new DelegateCommand(OnExitWindowCommand));
            }
        }

        private void OnExitWindowCommand()
        {
           this.OnCanceling();
            this.IsCanceled = true;
            this.OnOnInteractionComplete();
        }
  
        #endregion

    }
}
