using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Factories;
using ULA.Interceptors.IoC;

namespace ULA.Business.Factories
{
   public class ResistorFactory: IResistorFactory
    {
        private readonly IIoCContainer _container;

        public ResistorFactory(IIoCContainer container)
        {
            _container = container;
        }



        #region Implementation of IResistorFactory

        public List<IResistor> CreateResistorsOnDevice(IConfiguratedDeviceSchemeTable configuratedDeviceSchemeTable,IRuntimeDevice runtimeDevice)
        {
            List<IResistor> resistors=new List<IResistor>();
            if (runtimeDevice.StartersOnDevice.Count < 1) return resistors;
            IEnumerable<IDeviceResistorRow> resistorRowsInDevice = configuratedDeviceSchemeTable.GetResistorEnumerable();
            foreach (var resistorRow in resistorRowsInDevice)
            {
                IResistor resistor = _container.Resolve<IResistor>();
                resistor.ModuleNumber = resistorRow.ResistorModul;
                resistor.ResistorNumber = resistorRow.ResistorDiskret;
                if (resistorRow.ParentStarterId != null) resistor.ParentStarter =
                    runtimeDevice.StartersOnDevice.First((starter =>
                        starter.StarterNumber == resistorRow.ParentStarterId));
                resistors.Add(resistor);
            }
            return resistors;
        }

        #endregion
    }
}
