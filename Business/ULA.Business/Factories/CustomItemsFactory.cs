using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Strings;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Factories;
using ULA.Interceptors.IoC;

namespace ULA.Business.Factories
{
    public class CustomItemsFactory : ICustomItemsFactory
    {
        private readonly IIoCContainer _container;

        public CustomItemsFactory(IIoCContainer container)
        {
            _container = container;
        }


        #region Implementation of ICustomItemsFactory

        public IDeviceCustomItems CreateDeviceCustomItems(IDeviceContext deviceContext)
        {
            IDeviceCustomItems deviceCustomItems = _container.Resolve<IDeviceCustomItems>();

          

           

               

            var cascades = deviceContext.CustomDeviceSchemeTable.CascadeIndicatorsTable.GetEnumeratorObjects();
            if (cascades.Any())
            {
                deviceCustomItems.IsCascadeEnabled = true;
                cascades.ForEach((indicator =>
                {
                    if (deviceContext.DataTable.IsObjectExists(AddressesStrings.MODUL_STR + indicator.ResistorModule + AddressesStrings.SPLITTER_STR +
                                                                 AddressesStrings.DISKRET_STR + (indicator.ResistorNumber - 1) + AddressesStrings.SPLITTER_STR + AddressesStrings.STATE_STR))
                    {
                        ICascade cascade = _container.Resolve<ICascade>();
                        cascade.SignalDescription = indicator.Tag;
                        deviceCustomItems.Cascades.Add(cascade);
                    }
                }));
            }



            var indicators = deviceContext.CustomDeviceSchemeTable.IndicatorsTable.GetEnumeratorObjects();
            if (indicators.Any())
            {
                deviceCustomItems.IsIndicatorsEnabled = true;
                indicators.ForEach((indicator =>
                {
                    if (deviceContext.DataTable.IsObjectExists(AddressesStrings.MODUL_STR + indicator.ResistorModule + AddressesStrings.SPLITTER_STR +
                                                               AddressesStrings.DISKRET_STR + (indicator.ResistorNumber - 1) + AddressesStrings.SPLITTER_STR + AddressesStrings.STATE_STR))
                    {
                        IIndicator indicatorModel = _container.Resolve<IIndicator>();
                        indicatorModel.SignalDescription = indicator.Tag;
                        deviceCustomItems.Indicators.Add(indicatorModel);
                    }
                }));
            }



            var signals = deviceContext.CustomDeviceSchemeTable.SignalsTable.GetEnumeratorObjects();
            if (signals.Any())
            {
                deviceCustomItems.IsSignalsEnabled = true;
                signals.ForEach((signal =>
                {
                    if (deviceContext.DataTable.IsObjectExists(AddressesStrings.MODUL_STR + signal.ResistorModule + AddressesStrings.SPLITTER_STR +
                                                               AddressesStrings.DISKRET_STR + (signal.ResistorNumber - 1) + AddressesStrings.SPLITTER_STR + AddressesStrings.STATE_STR))
                    {
                        ISignal signalModel = _container.Resolve<ISignal>();
                        signalModel.SignalDescription = signal.Tag;
                        deviceCustomItems.Signals.Add(signalModel);
                    }
                }));
            }

            return deviceCustomItems;
        }

        #endregion
    }
}
