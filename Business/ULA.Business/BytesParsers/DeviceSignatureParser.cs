using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.BytesParsers;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;

namespace ULA.Business.BytesParsers
{
   public class DeviceSignatureParser:IRawBytesToDeviceStateParser
    {
        #region Implementation of IRawBytesToDeviceStateParser

        public void ParseRawBytesToDeviceState(byte[] rawBytes, IRuntimeDevice runtimeDevice)
        {
            var signature = runtimeDevice.DeviceMomento.State
                .DataTable[DeviceStringKeys.DeviceTableTagKeys.DEVICE_SIGNATURE].Formatter.Format(rawBytes).ToString();

            var version = rawBytes.Skip(16).Take(4).ToArray();
            string versionStr = version[2] + "." + version[3] + "." + version[0] + "." +
                                version[1];
            try
            {
                runtimeDevice.DeviceSignature = signature.Remove(8) + " " + versionStr;

            }
            catch (Exception e)
            {
                if (signature.Contains("RUNO"))
                {
                    runtimeDevice.DeviceSignature = "RUNO" + " " + versionStr;
                }
                else
                {
                    runtimeDevice.DeviceSignature = versionStr;
                }
            }
            var chars = runtimeDevice.DeviceSignature.ToCharArray();
            foreach (var charSign in chars)
            {
                if (charSign == 192||(!char.IsLetterOrDigit(charSign)&&!char.IsWhiteSpace(charSign) && !char.IsPunctuation(charSign)&& !Regex.IsMatch(charSign.ToString(), "[a-zA-Z]")))
                {
                    runtimeDevice.DeviceSignature= runtimeDevice.DeviceSignature.Replace(charSign, ' ');
                }
            }
           
            runtimeDevice.AnalogData.AnalogDataUpdated?.Invoke();
        }

        #endregion
    }
}
