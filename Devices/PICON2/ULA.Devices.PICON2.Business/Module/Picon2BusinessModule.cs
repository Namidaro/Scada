using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.Devices.PICON2.Business.Factories;

namespace ULA.Devices.PICON2.Business.Module
{
   public class Picon2BusinessModule:IModule
    {
        private readonly IUnityContainer _container;

        public Picon2BusinessModule(IUnityContainer container)
        {
            _container = container;
        }


        #region Implementation of IModule

        public void Initialize()
        {
            _container.RegisterType<Picon2StarterFactory>();
            _container.RegisterType<Picon2RuntimeDevice>();
            _container.RegisterType<Picon2DeviceFactory>();
            _container.RegisterType<Picon2ConfigLogicalDevice>();
        }

        #endregion
    }
}
