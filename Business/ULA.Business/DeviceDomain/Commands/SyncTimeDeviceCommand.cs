using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Metadata;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DeviceDomain.Commands
{
    public class SyncTimeDeviceCommand : DeviceCommandBase
    {
        private object[] _commandValues;
        private  bool? _isCommandSucceed;

        public SyncTimeDeviceCommand()
        {
            CommandTags = new string[]
            {
                DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_YEAR,
                DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_MONTH,
                DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_DAY,
                DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_DAYINWEEK,
                DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_HOUR,
                DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_MINUTE,
                DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_SECOND,
            };

        
        }


        #region Implementation of IDeviceCommand



        #endregion

        #region Implementation of IDeviceCommand

        public override string[] CommandTags { get; }

        public override object[] CommandValues
        {
            get { return _commandValues; }
        }

        public override bool? IsCommandSucceed
        {
            get { return _isCommandSucceed; }
        }

        #region Overrides of DeviceCommandBase

        public override void CheckIfCommandSucceed(IRuntimeDeviceViewModel runtimeDevice)
        {
            _isCommandSucceed = IsCommandSent;
            CurrectCommandStateChanged?.Invoke();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        public void SetValue(DateTime newDate)
        {
            _commandValues = new object[]
            {
                (short) newDate.Year, (byte) newDate.Month, (byte) newDate.Day, (byte) newDate.DayOfWeek,
                (byte) newDate.Hour, (byte) newDate.Minute, (byte) newDate.Second
            };
        }

        #endregion
    }
}
