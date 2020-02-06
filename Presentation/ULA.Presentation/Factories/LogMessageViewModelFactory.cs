using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure.Factories;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Presentation.Factories
{
  public  class LogMessageViewModelFactory: ILogMessageViewModelFactory
    {
        private readonly IIoCContainer _container;

        public LogMessageViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }


        #region Implementation of ILogMessageViewModelFactory

        public ILogMessageViewModel CreateLogMessageViewModel(string logType, string message, string owner, DateTime dateTime)
        {
            ILogMessageViewModel logMessageViewModel = _container.Resolve<ILogMessageViewModel>();
            logMessageViewModel.LogMessageType = logType;
            logMessageViewModel.Message = message;
            logMessageViewModel.Owner = owner;
            logMessageViewModel.MessageDateTime = dateTime;
            return logMessageViewModel;
        }

        #endregion
    }
}
