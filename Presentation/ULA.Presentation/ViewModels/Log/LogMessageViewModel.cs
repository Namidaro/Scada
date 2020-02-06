using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Presentation.ViewModels.Log
{
   public class LogMessageViewModel:BindableBase,ILogMessageViewModel
    {
        private string _logMessageType;
        private DateTime _messageDateTime;
        private string _message;
        private string _owner;

        #region Implementation of ILogMessageViewModel

        public string LogMessageType
        {
            get { return _logMessageType; }
            set { _logMessageType = value; }
        }

        public DateTime MessageDateTime
        {
            get { return _messageDateTime; }
            set { _messageDateTime = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        #endregion
    }
}
