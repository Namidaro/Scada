using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Devices.PICON2.Business;
using ULA.Devices.PICON2.Presentation.Interfaces;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Devices.PICON2.Presentation.ViewModels
{
    public class Picon2ModuleInfoViewModel : BindableBase, IPicon2ModuleInfoViewModel
    {
        private readonly Picon2RuntimeDevice _runtimeDevice;
        private string _moduleFirmwareVersion;
        private string _modemVersion;
        private string _modemFirmwareVersion;
        private string _modemImei;
        private bool _isNotCommandExecuting;

        public Picon2ModuleInfoViewModel(ISchemeModeRuntimeViewModel schemeModeRuntimeViewModel)
        {
            _runtimeDevice = schemeModeRuntimeViewModel.CurrentDeviceViewModel.Model as Picon2RuntimeDevice;
            UpdateData();
            ReadModuleInformationCommand = new DelegateCommand(OnReadExecute);
            IsNotCommandExecuting = true;
        }

        private void UpdateData()
        {
            if (_runtimeDevice.Picon2ModuleInfo.ModuleFirmwareVersion != null)
            {
                ModuleFirmwareVersion = _runtimeDevice.Picon2ModuleInfo.ModuleFirmwareVersion != "" 
                    ? _runtimeDevice.Picon2ModuleInfo.ModuleFirmwareVersion 
                    : "Неизвестно";
            }
            if (_runtimeDevice.Picon2ModuleInfo.ModemVersion != null)
            {
                ModemVersion = _runtimeDevice.Picon2ModuleInfo.ModemVersion != ""
                    ? _runtimeDevice.Picon2ModuleInfo.ModemVersion
                    : "Неизвестно";
            }
            if (_runtimeDevice.Picon2ModuleInfo.ModemFirmwareVersion != null)
            {
                ModemFirmwareVersion = _runtimeDevice.Picon2ModuleInfo.ModemFirmwareVersion != ""
                    ? _runtimeDevice.Picon2ModuleInfo.ModemFirmwareVersion
                    : "Неизвестно";
            }
            if (_runtimeDevice.Picon2ModuleInfo.ModemIMEI != null)
            {
                ModemIMEI = _runtimeDevice.Picon2ModuleInfo.ModemIMEI !=""
                    ? _runtimeDevice.Picon2ModuleInfo.ModemIMEI
                    : "Неизвестно";
            }
        }

        private async void OnReadExecute()
        {

            if (!_runtimeDevice.TcpDeviceConnection.LastTransactionSucceed)
            {
                MessageBox.Show("Устройство не на связи");
                return;
            }

            try
            {
                if (_runtimeDevice.ConnectionModuleId != 0)
                {
                    IsNotCommandExecuting = false;
                    var data = await _runtimeDevice.TcpDeviceConnection.ExecuteFunction12Async(
                        (byte)_runtimeDevice.ConnectionModuleId, "GetModuleFirmwareVersion", 0xF0);
                    if (data != null)
                    {
                        string ModuleFirmwareVersion = Encoding.UTF8.GetString(data);
                        _runtimeDevice.Picon2ModuleInfo.ModuleFirmwareVersion = ModuleFirmwareVersion;
                    }
                    data = await _runtimeDevice.TcpDeviceConnection.ExecuteFunction12Async(
                        (byte)_runtimeDevice.ConnectionModuleId, "GetModemVersion", 0xF1);
                    if (data != null)
                    {
                        string ModemVersion = Encoding.UTF8.GetString(data);
                        _runtimeDevice.Picon2ModuleInfo.ModemVersion = ModemVersion;
                    }

                    data = await _runtimeDevice.TcpDeviceConnection.ExecuteFunction12Async(
                        (byte)_runtimeDevice.ConnectionModuleId, "GetModemFirmwareVersion", 0xF2);
                    if (data != null)
                    {

                        string ModemFirmwareVersion = Encoding.UTF8.GetString(data);
                        _runtimeDevice.Picon2ModuleInfo.ModemFirmwareVersion = ModemFirmwareVersion;
                    }
                    data = await _runtimeDevice.TcpDeviceConnection.ExecuteFunction12Async(
                        (byte)_runtimeDevice.ConnectionModuleId, "GetModemIMEI", 0xF3);
                    if (data != null)
                    {
                        string ModemIMEI = Encoding.UTF8.GetString(data);
                        _runtimeDevice.Picon2ModuleInfo.ModemIMEI = ModemIMEI;
                    }
                    UpdateData();
                    IsNotCommandExecuting = true;
                }
                else
                {
                    MessageBox.Show("Не удалось прочитать номер модуля");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения информации по модулю связи");
            }
        }

        public bool IsNotCommandExecuting
        {
            get { return _isNotCommandExecuting; }
            set
            {
                _isNotCommandExecuting = value;
                RaisePropertyChanged();
            }
        }

        public string ModuleFirmwareVersion
        {
            get { return _moduleFirmwareVersion; }
            set
            {
                _moduleFirmwareVersion = value;
                RaisePropertyChanged();
            }
        }

        public string ModemVersion
        {
            get { return _modemVersion; }
            set
            {
                _modemVersion = value;
                RaisePropertyChanged();
            }
        }

        public string ModemFirmwareVersion
        {
            get { return _modemFirmwareVersion; }
            set
            {
                _modemFirmwareVersion = value;
                RaisePropertyChanged();
            }
        }

        public string ModemIMEI
        {
            get { return _modemImei; }
            set
            {
                _modemImei = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ReadModuleInformationCommand { get; }
    }
}
