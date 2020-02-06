using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;

namespace ULA.AddinsHost.Module
{
    /// <summary>
    /// 
    /// </summary>
   public class AddinsModule:IModule
    {
        private readonly IUnityContainer _container;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public AddinsModule(IUnityContainer container)
        {
            _container = container;
        }


        #region Implementation of IModule
        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            _container.RegisterType<ICustomFider,CustomFider>();
            _container.RegisterType<ICustomSignal, CustomSignal>();
            _container.RegisterType<ICustomIndicator, CustomIndicator>();
            _container.RegisterType<ICascadeIndicator, CascadeIndicator>();



        }

        #endregion
    }
}
