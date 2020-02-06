using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Metadata;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DataServices
{
    public class DataWritingService : Disposable, IDataWritingService
    {
        private IRuntimeDevice _runtimeDevice;

        public DataWritingService()
        {

        }



        #region Implementation of IDataWritingService
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentBytes"></param>
        /// <param name="entityMetadata"></param>
        /// <param name="tags"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public async Task<bool> WriteValues(EntityMetadata entityMetadata, string[] tags, object[] values)
        {

            byte[] currentBytes = _runtimeDevice.DeviceDataCache.GetBytesFromMetadata(entityMetadata);
            byte[] newBytes= currentBytes;
            for (int i = 0; i < tags.Length; i++)
            {
                newBytes= (byte[])_runtimeDevice.DeviceMomento.State.DataTable[tags[i]].Formatter.FormatBack(values[i], newBytes);
            }
            if (newBytes != null)
            {
                return await _runtimeDevice.TcpDeviceConnection.PostDataByAddressAsync(entityMetadata.StartAddress,
                    newBytes, "writing");
            }
            return false;
        }

        public void SetDevice(IRuntimeDevice runtimeDevice)
        {
            _runtimeDevice = runtimeDevice;
        }

        #endregion
    }
}
