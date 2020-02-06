using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.AddinsHost.Business.Driver;
using ULA.Drivers.TcpFamily.TcpModbus;

namespace ULA.Drivers.TcpFamily.Module
{
   /// <summary>
   /// 
   /// </summary>
   public class TcpModbusDriverModule:IModule
    {
        private readonly IUnityContainer _container;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public TcpModbusDriverModule(IUnityContainer container)
        {
            _container = container;
        }



        #region Implementation of IModule
        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            _container.RegisterType<ILogicalDriverFactory,TcpModbusDriverFactory>("TCP_MB");
            _container.RegisterType<TcpModbusDriver>();
        }

        #endregion
    }
}
