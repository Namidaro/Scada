using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Presentation.Factories
{
  public  class ResistorViewModelFactory: IResistorViewModelFactory
    {
        private readonly IIoCContainer _container;

        public ResistorViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }


        #region Implementation of IResistorViewModelFactory

        public IResistorViewModel CreateResistorViewModel(object resistor,bool isEnabled)
        {
           IResistorViewModel resistorViewModel= _container.Resolve<IResistorViewModel>();
            if (isEnabled)
            {
                resistorViewModel.IsEnabled = true;
                resistorViewModel.Model = resistor;
            }
            else
            {
                resistorViewModel.IsEnabled = false;

            }

            return resistorViewModel;
        }

        #endregion
    }
}
