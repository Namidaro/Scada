using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.Keys;
using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems
{
    [DataContract]
    public class CustomSignal:AomEntity, ICustomSignal
    {

        public CustomSignal():base(CustomSignal.CreateTypeMetadata())
        {
            ResistorNumber = 0;
            ResistorModule = 0;
            Tag = "1";
        }


        private static AomEntityType CreateTypeMetadata()
        {
            return new AomEntityType(typeof(AomDeviceContextEntity), new AomPropertyTypeCollection(new[]
            {
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR, typeof (int)),
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR, typeof (int)),
                new AomPropertyType(DeviceEntityKeys.TAG, typeof (string)),

            }));
        }


        #region Implementation of ICustomSignal

        public string Tag
        {
            get { return this.Properties[DeviceEntityKeys.TAG].Value?.ToString(); }
            set { this.Properties[DeviceEntityKeys.TAG].Value = value; }
        }

        public int ResistorModule
        {
            get { return Convert.ToInt32(this.Properties[DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR].Value = value; }

        }
        public int ResistorNumber
        {
            get { return Convert.ToInt32(this.Properties[DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR].Value = value; }

        }

        #endregion
    }
}
