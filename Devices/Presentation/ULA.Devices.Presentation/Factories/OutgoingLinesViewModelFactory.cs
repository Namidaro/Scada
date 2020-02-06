using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.OutgoingLines;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Devices.Presentation.Runtime;
using ULA.Devices.Presentation.Runtime.OutgoingLines;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Presentation.Factories
{
   public class OutgoingLinesViewModelFactory: IOutgoingLinesViewModelFactory
    {
        private readonly IIoCContainer _container;
        private readonly IResistorViewModelFactory _resistorViewModelFactory;

        public OutgoingLinesViewModelFactory(IIoCContainer container, IResistorViewModelFactory resistorViewModelFactory)
        {
            _container = container;
            _resistorViewModelFactory = resistorViewModelFactory;
        }

        #region Implementation of IOutgoingLinesViewModelFactory

        public IOutgoingLinesViewModel CreateOutgoingLinesViewModel(object runtimeDeviceObj)
        {
            IRuntimeDevice runtimeDevice= runtimeDeviceObj as IRuntimeDevice;
            var fiders =runtimeDevice.DeviceMomento.State.CustomDeviceSchemeTable.FidersTable.GetEnumeratorObjects();
            if (fiders.Any())
            {
                FidersOutgoingLinesViewModel fidersOutgoingLinesViewModel =
                    _container.Resolve<FidersOutgoingLinesViewModel>();

                foreach (var fider in fiders)
                {
                    IFiderViewModel fiderViewModel = _container.Resolve<IFiderViewModel>();
                    bool isFiderEnabled = false;


                    if (fider.IsEnabledResistor1)
                    {
                        if (runtimeDevice.ResistorsOnDevice.Any((resistor =>
                            resistor.ModuleNumber == fider.ModuleResistor1 &&
                            resistor.ResistorNumber == fider.NumberResistor1 - 1)))
                        {
                            IResistor resistor1 = runtimeDevice.ResistorsOnDevice.First((resistor =>
                                resistor.ModuleNumber == fider.ModuleResistor1 &&
                                resistor.ResistorNumber == fider.NumberResistor1 - 1));

                            fiderViewModel.ResistorViewModels.Add(
                                _resistorViewModelFactory.CreateResistorViewModel(resistor1, true));
                            isFiderEnabled = true;
                        }
                        else
                        {
                            fiderViewModel.ResistorViewModels.Add(
                                _resistorViewModelFactory.CreateResistorViewModel(null, false));
                        }

                    }
                    else
                    {
                        fiderViewModel.ResistorViewModels.Add(
                            _resistorViewModelFactory.CreateResistorViewModel(null, false));
                    }


                    if (fider.IsEnabledResistor2)
                    {
                        if (runtimeDevice.ResistorsOnDevice.Any((resistor =>
                            resistor.ModuleNumber == fider.ModuleResistor2 &&
                            resistor.ResistorNumber == fider.NumberResistor2-1)))
                        {
                            IResistor resistor2 = runtimeDevice.ResistorsOnDevice.First((resistor =>
                                resistor.ModuleNumber == fider.ModuleResistor2 &&
                                resistor.ResistorNumber == fider.NumberResistor2-1));

                            fiderViewModel.ResistorViewModels.Add(
                                _resistorViewModelFactory.CreateResistorViewModel(resistor2,true));
                            isFiderEnabled = true;
                        }
                        else
                        {
                            fiderViewModel.ResistorViewModels.Add(
                                _resistorViewModelFactory.CreateResistorViewModel(null, false));
                        }

                    }
                    else
                    {
                        fiderViewModel.ResistorViewModels.Add(
                            _resistorViewModelFactory.CreateResistorViewModel(null, false));
                    }

                    if (fider.IsEnabledResistor3)
                    {
                        if (runtimeDevice.ResistorsOnDevice.Any((resistor =>
                            resistor.ModuleNumber == fider.ModuleResistor3 &&
                            resistor.ResistorNumber == fider.NumberResistor3 - 1)))
                        {
                            IResistor resistor3 = runtimeDevice.ResistorsOnDevice.First((resistor =>
                                resistor.ModuleNumber == fider.ModuleResistor3 &&
                                resistor.ResistorNumber == fider.NumberResistor3 - 1));

                            fiderViewModel.ResistorViewModels.Add(
                                _resistorViewModelFactory.CreateResistorViewModel(resistor3, true));
                            isFiderEnabled = true;
                        }
                        else
                        {
                            fiderViewModel.ResistorViewModels.Add(
                                _resistorViewModelFactory.CreateResistorViewModel(null, false));
                        }

                    }
                    else
                    {
                        fiderViewModel.ResistorViewModels.Add(
                            _resistorViewModelFactory.CreateResistorViewModel(null, false));
                    }
                    if (isFiderEnabled)
                    {
                        fiderViewModel.FiderSignature = fider.Tag;
                        fidersOutgoingLinesViewModel.FiderViewModels.Add(fiderViewModel);
                    }

                }

                return fidersOutgoingLinesViewModel;
            }
            else
            {
                ResistorOutgoingLinesViewModel resistorOutgoingLinesViewModel =
                    _container.Resolve<ResistorOutgoingLinesViewModel>();
                List<IResistorViewModel> resistorViewModels=new List<IResistorViewModel>();
                foreach (var resistor in runtimeDevice.ResistorsOnDevice)
                {
                    resistorViewModels.Add(
                        _resistorViewModelFactory.CreateResistorViewModel(resistor,true));
                }
               var moduleOrdered= resistorViewModels.OrderBy((model => model.ResistorNumber));
                var resOrdered = moduleOrdered.OrderBy((model => model.ModuleNumber));
                resistorOutgoingLinesViewModel.ResistorViewModels.AddRange(resOrdered);
                return resistorOutgoingLinesViewModel;
            }
            throw new ArgumentException();
        }

        #endregion
    }
}
