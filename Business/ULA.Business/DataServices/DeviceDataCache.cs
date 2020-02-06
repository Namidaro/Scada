using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.Metadata;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DataServices
{
    public class DeviceDataCache : Disposable, IDeviceDataCache
    {
        private ConcurrentDictionary<EntityMetadata, byte[]> _cacheBytes;
        public DeviceDataCache()
        {
            _cacheBytes = new ConcurrentDictionary<EntityMetadata, byte[]>();
        }


        #region Implementation of IDeviceDataCache
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityMetadata"></param>
        /// <returns></returns>
        public byte[] GetBytesFromMetadata(EntityMetadata entityMetadata)
        {
            return _cacheBytes[entityMetadata];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityMetadata"></param>
        /// <param name="bytes"></param>
        public void SaveBytesByMetadata(EntityMetadata entityMetadata, byte[] bytes)
        {
            if (!_cacheBytes.ContainsKey(entityMetadata))
            {
                _cacheBytes.TryAdd(entityMetadata, bytes);
            }
            else
            {
                _cacheBytes[entityMetadata] = bytes;
            }
        }

        #endregion
    }
}
