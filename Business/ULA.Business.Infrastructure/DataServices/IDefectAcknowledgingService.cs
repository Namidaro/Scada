using System;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDefectAcknowledgingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        void Initialize(IRuntimeDevice runtimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool IsFailNeedsAcknowledge();
        /// <summary>
        /// 
        /// </summary>
        void AcknowledgeFail();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Action AcknowledgeValueChanged { get; set; }
    }
}