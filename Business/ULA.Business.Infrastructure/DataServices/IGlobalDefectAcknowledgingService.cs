using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGlobalDefectAcknowledgingService
    {/// <summary>
    /// 
    /// </summary>
    /// <param name="runtimeDevice"></param>
    /// <returns></returns>
        IDefectAcknowledgingService GetDefectAcknowledgingService(IRuntimeDevice runtimeDevice);
    }
}