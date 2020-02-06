using System;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Presentation.Infrastructure.Factories
{
    public interface ILogMessageViewModelFactory
    {
        ILogMessageViewModel CreateLogMessageViewModel(string logType,string message,string owner,DateTime dateTime);
    }
}