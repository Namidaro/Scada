using System.Collections.Generic;

namespace ULA.Business.Infrastructure.DeviceDomain
{

    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceCustomItems
    {
        bool IsSignalsEnabled { get; set; }
        bool IsIndicatorsEnabled { get; set; }
        bool IsCascadeEnabled { get; set; }

        List<ICascade> Cascades { get; set; }
        List<ISignal> Signals { get; set; }
        List<IIndicator> Indicators { get; set; }

    }
}