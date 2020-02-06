using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DTOs;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DataServices
{
    public class DeviceLogLoadingService : Disposable, IDeviceLogLoadingService
    {
        private readonly Func<ILogInformation> _logInfoCreator;
        private int _currentOffset = 0;
        private ushort? _journalLenght = null;
        private List<byte> _allReadedBytes = new List<byte>();
        private IRuntimeDevice _runtimeDevice;

        public DeviceLogLoadingService(Func<ILogInformation> logInfoCreator)
        {
            _logInfoCreator = logInfoCreator;

        }



        #region Implementation of IDeviceLogLoadingService

        public async Task<List<ILogInformation>> ReadNextLogFromDevice()
        {
            if (!_journalLenght.HasValue)
            {
                try
                {
                    byte[] journalLenghtData = null;
                    journalLenghtData =
                        await _runtimeDevice.TcpDeviceConnection.GetDataByAddressAsync(0x2000, 1,
                            "Get journal lenght data");
                    ushort journalLenght = (ushort) (journalLenghtData[0] * 256 + journalLenghtData[1]);
                    _journalLenght = journalLenght;
                }
                catch (Exception e)
                {
                }
            }


            if (_currentOffset >= _journalLenght) return null;

            var startAddress = 0x2001;
            int maxPackageLenght = 60;
            int lenghtPackage = (_journalLenght.Value - _currentOffset > maxPackageLenght)
                ? maxPackageLenght
                : _journalLenght.Value - _currentOffset;
            try
            {
                var readData =
                (
                    await _runtimeDevice.TcpDeviceConnection.GetDataByAddressAsync(
                        (ushort) (startAddress + _currentOffset),
                        (ushort) lenghtPackage, "Get journal data")) as byte[];
                if (readData == null)
                {
                    readData =
                    (
                        await _runtimeDevice.TcpDeviceConnection.GetDataByAddressAsync(
                            (ushort) (startAddress + _currentOffset),
                            (ushort) lenghtPackage, "Get journal data")) as byte[];
                    if (readData == null) return null;
                }
                _allReadedBytes.AddRange(readData);
            }
            catch (Exception ex)
            {

            }
            _currentOffset += lenghtPackage;
            var parsedJournalData = new byte[_allReadedBytes.Count];
            Array.Copy(_allReadedBytes.ToArray(), parsedJournalData, _allReadedBytes.Count);
            for (int i = 0; i < parsedJournalData.Length; i += 2)
            {
                if (i + 1 >= parsedJournalData.Length)
                {
                    break;
                }
                var temp = parsedJournalData[i];
                parsedJournalData[i] = parsedJournalData[i + 1];
                parsedJournalData[i + 1] = temp;
            }
            return CreateLogListFromBytes(parsedJournalData);

        }


        private List<ILogInformation> CreateLogListFromBytes(byte[] parsedJournalData)
        {
            List<ILogInformation> logData = new List<ILogInformation>();
            var longCurrentMessage = Encoding.Default.GetString(parsedJournalData.ToArray()).ToList();

            for (int i = 0; i < longCurrentMessage.Count() - 46; i += 46)
            {
                var message = new String(longCurrentMessage.GetRange(i, 46).ToArray());

                /*Разбор регулярным выражением*/
                Regex regex = new Regex(@"\<(?<Date>.+)\>\<(?<Time>.+)\>\<(?<Message>.+)\>");
                Match m = regex.Match(message);
                string EventDate = string.Empty;
                string EventTime = string.Empty;
                string EventMessage;
                DateTime JournalDateTime = new DateTime();
                if (m.Success)
                {
                    EventDate = m.Groups["Date"].Value;
                    EventTime = m.Groups["Time"].Value;
                    EventMessage = m.Groups["Message"].Value;

                    try
                    {
                        IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
                        DateTime f1 = DateTime.ParseExact(EventDate, "dd/MM/yyyy", culture);
                        DateTime f2 = DateTime.Parse(EventTime, culture,
                            System.Globalization.DateTimeStyles.AssumeLocal);
                        JournalDateTime = new DateTime(f1.Year, f1.Month, f1.Day, f2.Hour, f2.Minute, f2.Second);
                    }
                    catch (Exception)
                    {
                        EventMessage = "Ошибка преобразования сообщения";
                    }
                }
                else
                {
                    EventMessage = "Ошибка преобразования сообщения";
                }
                ILogInformation logInformation = _logInfoCreator();
                logInformation.ActionDate = JournalDateTime;
                logInformation.ActionDescription = EventMessage;
                logData.Add(logInformation);
            }
            return logData;
        }




        public void Initialize(IRuntimeDevice runtimeDevice)
        {
            _currentOffset = 0;
            _runtimeDevice = runtimeDevice;
            _journalLenght = null;
            _allReadedBytes = new List<byte>();
        }

        #region Overrides of Disposable

        protected override void OnDisposing()
        {
            _currentOffset = 0;
            _runtimeDevice = null;
            _journalLenght = null;
            _allReadedBytes = null;
            base.OnDisposing();
        }

        #endregion

        #endregion
    }
}
