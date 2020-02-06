using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.BytesParsers;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Factories;
using ULA.Interceptors.IoC;

namespace ULA.Business.Factories
{
    public class RawBytesToDeviceStateParserFactory : IRawBytesToDeviceStateParserFactory
    {
        private readonly IIoCContainer _container;

        public RawBytesToDeviceStateParserFactory(IIoCContainer container)
        {
            _container = container;
        }


        #region Implementation of IRawBytesToDeviceStateParserFactory

        public IRawBytesToDeviceStateParser CreatePartialModeParser()
        {
            return _container.Resolve<IRawBytesToDeviceStateParser>(DeviceStringKeys.DeviceBytesParsers.PARTIAL_MODE_PARSER);
        }

        public IRawBytesToDeviceStateParser CreateDateTimeParser()
        {
            return _container.Resolve<IRawBytesToDeviceStateParser>(DeviceStringKeys.DeviceBytesParsers.DATETIME_PARSER);
        }

        public IRawBytesToDeviceStateParser CreateSignalLevelParser()
        {
            return _container.Resolve<IRawBytesToDeviceStateParser>(DeviceStringKeys.DeviceBytesParsers.SIGNAL_LEVEL_PARSER);
        }
        public IRawBytesToDeviceStateParser CreateDeviceSignatureParser()
        {
            return _container.Resolve<IRawBytesToDeviceStateParser>(DeviceStringKeys.DeviceBytesParsers.DEVICE_SIGNATURE_PARSER);
        }

        public IRawBytesToDeviceStateParser CreatAnalogParser()
        {
            return _container.Resolve<IRawBytesToDeviceStateParser>(DeviceStringKeys.DeviceBytesParsers.ANALOGS_PARSER);

        }

        public IRawBytesToDeviceStateParser CreatAnalogDateTimeParser()
        {
            return _container.Resolve<IRawBytesToDeviceStateParser>(DeviceStringKeys.DeviceBytesParsers.ANALOG_DATETIME_PARSER);
        }

        #endregion
    }
}
