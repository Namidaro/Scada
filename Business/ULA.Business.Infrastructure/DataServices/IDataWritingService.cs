using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Metadata;

namespace ULA.Business.Infrastructure.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataWritingService
    {
        /// <summary>
        /// записать значения по тэгам
        /// </summary>
        /// <param name="entityMetadata"></param>
        /// <param name="tags"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        Task<bool> WriteValues(EntityMetadata entityMetadata,string[] tags, object[] values);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        void SetDevice(IRuntimeDevice runtimeDevice);
    }
}
