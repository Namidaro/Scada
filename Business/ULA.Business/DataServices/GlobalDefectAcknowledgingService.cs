using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Interceptors.IoC;

namespace ULA.Business.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public class GlobalDefectAcknowledgingService : IGlobalDefectAcknowledgingService
    {
        private readonly IIoCContainer _container;
        private ConcurrentDictionary<IRuntimeDevice, IDefectAcknowledgingService> _acknowledgingServices;

        public GlobalDefectAcknowledgingService(IIoCContainer container)
        {

            _container = container;
            _acknowledgingServices=new ConcurrentDictionary<IRuntimeDevice, IDefectAcknowledgingService>();
        }

        #region Implementation of IGlobalDefectAcknowledgingService

        public IDefectAcknowledgingService GetDefectAcknowledgingService(IRuntimeDevice runtimeDevice)
        {
            if (_acknowledgingServices.ContainsKey(runtimeDevice))
            {
                return _acknowledgingServices[runtimeDevice];
            }
            else
            {
                IDefectAcknowledgingService defectAcknowledgingService =
                    _container.Resolve<IDefectAcknowledgingService>();
                defectAcknowledgingService.Initialize(runtimeDevice);
                _acknowledgingServices.TryAdd(runtimeDevice, defectAcknowledgingService);
                return defectAcknowledgingService;
            }
        }

        #endregion
    }
}
