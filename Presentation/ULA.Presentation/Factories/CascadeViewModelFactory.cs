using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;
using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure.Factories;
using ULA.Presentation.Infrastructure.ViewModels.CustomItems;

namespace ULA.Presentation.Factories
{
   public class CascadeViewModelFactory: ICascadeViewModelFactory
    {

        private readonly IIoCContainer _container;

        /// <summary>
        /// 
        /// </summary>
        public CascadeViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customIndicator"></param>
        /// <returns></returns>
        public ICascadeDefinitionsViewModel CreateCascadeDefinitionsViewModel(ICascadeIndicator cascadeIndicator)
        {
            ICascadeDefinitionsViewModel cascadeDefinitionsViewModel = _container.Resolve<ICascadeDefinitionsViewModel>();
            cascadeDefinitionsViewModel.Model = cascadeIndicator;
            return cascadeDefinitionsViewModel;
        }
    }
}