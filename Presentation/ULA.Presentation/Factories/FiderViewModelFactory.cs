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
    /// <summary>
    /// 
    /// </summary>
    public class FiderViewModelFactory : IFiderViewModelFactory
    {
        private readonly IIoCContainer _container;

        /// <summary>
        /// 
        /// </summary>
        public FiderViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customFider"></param>
        /// <returns></returns>
        public IFiderDefinitionsViewModel CreateFiderDefinitionsViewModel(ICustomFider customFider)
        {
            IFiderDefinitionsViewModel fiderDefinitionsViewModel = _container.Resolve<IFiderDefinitionsViewModel>();
            fiderDefinitionsViewModel.FiderDefinitionModel = customFider;
            return fiderDefinitionsViewModel;
        }
    }
}
