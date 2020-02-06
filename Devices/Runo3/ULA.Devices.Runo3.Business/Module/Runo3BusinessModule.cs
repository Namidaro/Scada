using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.AddinsHost.Business.Device;
using ULA.Devices.Runo3.Business.Factories;
using ULA.Devices.Runo3.Business.Interfaces;

namespace ULA.Devices.Runo3.Business.Module
{
   public class Runo3BusinessModule:IModule
    {
        private readonly IUnityContainer _container;

        public Runo3BusinessModule(IUnityContainer container)
        {
            _container = container;
        }


        #region Implementation of IModule

        public void Initialize()
        {
            _container.RegisterType<IRuno3StarterFactory, Runo3StarterFactory>();
            _container.RegisterType<IRuno3Device, Runo3RuntimeDevice>();
            _container.RegisterType<IRuno3DeviceFactory, Runo3DeviceFactory>();
            _container.RegisterType<Runo3ConfigLogicalDevice>();

        }

        #endregion
    }
}
