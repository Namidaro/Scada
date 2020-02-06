using ULA.AddinsHost.Business.Device.Context;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.Factories
{
    public interface ICustomItemsFactory
    {
        IDeviceCustomItems CreateDeviceCustomItems(IDeviceContext deviceContext);
    }
}