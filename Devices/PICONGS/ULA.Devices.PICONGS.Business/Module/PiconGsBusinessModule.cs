using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.Devices.PICONGS.Business.Factories;

namespace ULA.Devices.PICONGS.Business.Module
{
  public  class PiconGsBusinessModule:IModule
    {
        private readonly IUnityContainer _container;

        public PiconGsBusinessModule(IUnityContainer container)
        {
            _container = container;
        }

        #region Implementation of IModule

        public void Initialize()
        {
            _container.RegisterType<PiconGsStarterFactory>();
            _container.RegisterType<PiconGsRuntimeDevice>();
            _container.RegisterType<PiconGsDeviceFactory>();
            _container.RegisterType<PiconGsConfigLogicalDevice>();
        }

        #endregion
    }
}
