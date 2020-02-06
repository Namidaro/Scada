using ULA.Business.ConfigLogicalDevice;
using ULA.Business.Infrastructure.PersistanceServices;

namespace ULA.Devices.PICONGS.Business
{
    public class PiconGsConfigLogicalDevice : ConfigLogicalDevice
    {
        public PiconGsConfigLogicalDevice(IPersistanceService persistanceService) : base(persistanceService)
        {
        }


        #region Overrides of ConfigLogicalDevice

   
        #endregion
    }
}
