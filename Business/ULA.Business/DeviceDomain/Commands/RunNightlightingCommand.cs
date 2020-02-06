using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.Commands;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Metadata;
using ULA.Interceptors;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DeviceDomain.Commands
{
    public class StartersCommand : DeviceCommandBase
    {
        private object[] _value;
        private string[] _commandTags;

        public StartersCommand()
        {

        }


        #region Implementation of IDeviceCommand

        public override string[] CommandTags
        {
            get { return _commandTags; }
        }

        public override object[] CommandValues => _value;

     

        internal void SetValue(bool[] val)
        {
            _value = val.Select((b => (object) b)).ToArray();
        }

        #endregion

        internal void SetTag(string[] s)
        {
            _commandTags = s;
        }


     
    }
}