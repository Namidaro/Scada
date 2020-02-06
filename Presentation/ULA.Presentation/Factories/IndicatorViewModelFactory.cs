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
    public class IndicatorViewModelFactory : IIndicatorViewModelFactory
    {
        private readonly IIoCContainer _container;

        /// <summary>
        /// 
        /// </summary>
        public IndicatorViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customIndicator"></param>
        /// <returns></returns>
        public IIndicatorDefinitionsViewModel CreateIndicatorDefinitionsViewModel(ICustomIndicator customIndicator)
        {
            IIndicatorDefinitionsViewModel indicatorDefinitionsViewModel = _container.Resolve<IIndicatorDefinitionsViewModel>();
            indicatorDefinitionsViewModel.Model = customIndicator;
            return indicatorDefinitionsViewModel;
        }
    }
}
