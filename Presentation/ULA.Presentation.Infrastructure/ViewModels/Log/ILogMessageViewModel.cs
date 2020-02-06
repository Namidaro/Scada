using System;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    public interface ILogMessageViewModel
    {
        string LogMessageType { get; set; }
        DateTime MessageDateTime { get; set; }
        string Message { get; set; }
        string Owner { get; set; }
    }
}