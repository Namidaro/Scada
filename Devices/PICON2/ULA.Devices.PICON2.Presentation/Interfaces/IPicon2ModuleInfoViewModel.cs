using System.Windows.Input;

namespace ULA.Devices.PICON2.Presentation.Interfaces
{
    public interface IPicon2ModuleInfoViewModel
    {
        bool IsNotCommandExecuting { get; set; }
        string ModuleFirmwareVersion { get; set; }
        string ModemVersion { get; set; }
        string ModemFirmwareVersion { get; set; }
        string ModemIMEI { get; set; }
        ICommand ReadModuleInformationCommand { get; }


    }
}
