using System.Collections.Generic;
using ULA.AddinsHost.Business.Device.Context;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.Factories
{
    /// <summary>
    /// фабрика для создания пускателей
    /// </summary>
    public interface IStarterFactory
    {
        /// <summary>
        /// метод создания пускателей
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="configDataBytes"></param>
        /// <returns></returns>
        List<IStarter> CreateStarters(IDeviceContext deviceContext, byte[] configDataBytes);
    }
}