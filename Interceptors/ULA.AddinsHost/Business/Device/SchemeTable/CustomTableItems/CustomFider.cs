using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.Keys;
using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems
{
   /// <summary>
   /// 
   /// </summary>
   public class CustomFider:AomEntity,ICustomFider
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomFider() : base(CustomFider.CreateTypeMetadata())
        {
            IsEnabledResistor1 = false;
            IsEnabledResistor2 = false;
            IsEnabledResistor3 = false;
            ModuleResistor1 = 0;
            ModuleResistor2 = 0;
            ModuleResistor3 = 0;
            NumberResistor1 = 0;
            NumberResistor2 = 0;
            NumberResistor3 = 0;
            Tag=String.Empty;
        }

        #region Implementation of ICustomFider
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabledResistor1
        {
            get { return Convert.ToBoolean(this.Properties[DeviceEntityKeys.CustomSchemeKeys.IS_ENABLED_RESISTOR1].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.IS_ENABLED_RESISTOR1].Value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabledResistor2
        {
            get { return Convert.ToBoolean(this.Properties[DeviceEntityKeys.CustomSchemeKeys.IS_ENABLED_RESISTOR2].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.IS_ENABLED_RESISTOR2].Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEnabledResistor3
        {
            get { return Convert.ToBoolean(this.Properties[DeviceEntityKeys.CustomSchemeKeys.IS_ENABLED_RESISTOR3].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.IS_ENABLED_RESISTOR3].Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModuleResistor1
        {
            get { return Convert.ToInt32(this.Properties[DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR1].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR1].Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModuleResistor2
        {
            get { return Convert.ToInt32(this.Properties[DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR2].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR2].Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ModuleResistor3
        {
            get { return Convert.ToInt32(this.Properties[DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR3].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR3].Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NumberResistor1
        {
            get { return Convert.ToInt32(this.Properties[DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR1].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR1].Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NumberResistor2
        {
            get { return Convert.ToInt32(this.Properties[DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR2].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR2].Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int NumberResistor3
        {
            get { return Convert.ToInt32(this.Properties[DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR3].Value); }
            set { this.Properties[DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR3].Value = value; }
        }


        private static AomEntityType CreateTypeMetadata()
        {
            return new AomEntityType(typeof(AomDeviceContextEntity), new AomPropertyTypeCollection(new[]
            {
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.IS_ENABLED_RESISTOR1, typeof (bool)),
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.IS_ENABLED_RESISTOR2, typeof (bool)),
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.IS_ENABLED_RESISTOR3, typeof (bool)),
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR1, typeof (int)),
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR2, typeof (int)),
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.MODULE_RESISTOR3, typeof (int)),
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR1, typeof (int)),
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR2, typeof (int)),
                new AomPropertyType(DeviceEntityKeys.CustomSchemeKeys.NUMBER_RESISTOR3, typeof (int)),
                new AomPropertyType(DeviceEntityKeys.TAG, typeof (string)),

            }));
        }
     

        #endregion

        #region Implementation of ITagNamedObject
        /// <summary>
        /// 
        /// </summary>
        public string Tag
        {
            get { return this.Properties[DeviceEntityKeys.TAG].Value.ToString(); }
            set { this.Properties[DeviceEntityKeys.TAG].Value = value; }
        }

        #endregion
    }
}
