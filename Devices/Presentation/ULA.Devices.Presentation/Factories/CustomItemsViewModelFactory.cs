using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.CustomItems;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Presentation.Factories
{
  public  class CustomItemsViewModelFactory: ICustomItemsViewModelFactory
    {
        private readonly IIoCContainer _container;

        public CustomItemsViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }

        #region Implementation of ICustomItemsViewModelFactory

        public ICustomItemsViewModel CreateCustomItemsViewModel(object deviceCustomItems)
        {
           IDeviceCustomItems deviceCustomItemsModel= deviceCustomItems as IDeviceCustomItems;
            ICustomItemsViewModel customItemsViewModel = _container.Resolve<ICustomItemsViewModel>();


            if (deviceCustomItemsModel.IsCascadeEnabled)
            {
                customItemsViewModel.IsCascadeEnabled = true;
                List<ICascadeViewModel> cascadeViewModels = new List<ICascadeViewModel>();
                deviceCustomItemsModel.Cascades.ForEach((cascade =>
                {
                    ICascadeViewModel cascadeViewModel = _container.Resolve<ICascadeViewModel>();
                    cascadeViewModel.Model = cascade;
                    cascadeViewModels.Add(cascadeViewModel);
                } ));
                customItemsViewModel.CascadeViewModels = cascadeViewModels;
            }


            if (deviceCustomItemsModel.IsSignalsEnabled)
            {
                customItemsViewModel.IsSignalsEnabled = true;
                List<ISignalViewModel> signalViewModels = new List<ISignalViewModel>();
                deviceCustomItemsModel.Signals.ForEach((signal =>
                {
                    ISignalViewModel signalViewModel = _container.Resolve<ISignalViewModel>();
                    signalViewModel.Model = signal;
                    signalViewModels.Add(signalViewModel);
                }));
                customItemsViewModel.SignalViewModels = signalViewModels;
            }

            if (deviceCustomItemsModel.IsIndicatorsEnabled)
            {
                customItemsViewModel.IsIndicatorsEnabled = true;
                List<IIndicatorViewModel> indicatorViewModels = new List<IIndicatorViewModel>();
                deviceCustomItemsModel.Indicators.ForEach((indicator =>
                {
                    IIndicatorViewModel indicatorViewModel = _container.Resolve<IIndicatorViewModel>();
                    indicatorViewModel.Model = indicator;
                    indicatorViewModels.Add(indicatorViewModel);
                }));
                customItemsViewModel.IndicatorViewModels = indicatorViewModels;
            }


            return customItemsViewModel;


        }

        #endregion
    }
}
