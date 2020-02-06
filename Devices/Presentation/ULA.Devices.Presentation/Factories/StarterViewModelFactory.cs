using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Devices.Presentation.Interfaces;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Presentation.Factories
{
   public class StarterViewModelFactory:IStarterViewModelFactory
    {
        private readonly IIoCContainer _container;

        public StarterViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }



        #region Implementation of IStarterViewModelFactory

        public IStarterViewModel CreateStarterViewModel(IStarter starter)
        {
            IStarterViewModel starterViewModel = _container.Resolve<IStarterViewModel>();
            starterViewModel.Model = starter;
            return starterViewModel;
        }

        #endregion
    }
}
