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
    public class SignalViewModelFactory : ISignalViewModelFactory
    {
        private readonly IIoCContainer _container;

        /// <summary>
        /// 
        /// </summary>
        public SignalViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="customSignal"></param>
        /// <returns></returns>
        public ISignalDefinitionsViewModel CreateSignalDefinitionsViewModel(ICustomSignal customSignal)
        {
            ISignalDefinitionsViewModel signalDefinitionsViewModel = _container.Resolve<ISignalDefinitionsViewModel>();
            signalDefinitionsViewModel.Model = customSignal;
            return signalDefinitionsViewModel;
        }
    }
}
