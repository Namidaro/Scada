using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.Business.Device.Keys;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomCollections;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;
using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Device.SchemeTable
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CustomDeviceSchemeTable : AomEntity, ICustomDeviceSchemeTable
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomDeviceSchemeTable() : base(CustomDeviceSchemeTable.CreateTypeMetadata())
        {
            FidersTable=new CustomFidersCollection();
            SignalsTable=new CustomSignalsCollection();
            IndicatorsTable=new CustomIndicatorsCollections();
            CascadeIndicatorsTable=new CascadeIndicatorCollection();
        }


        private static AomEntityType CreateTypeMetadata()
        {
            return new AomEntityType(typeof(AomDeviceContextEntity), new AomPropertyTypeCollection(new[]
            {
                new AomPropertyType(DeviceEntityKeys.DeviceDataTable.DATATABLE_FIDERS,
                    typeof(CustomFidersCollection)),
                new AomPropertyType(DeviceEntityKeys.DeviceDataTable.DATATABLE_SIGNALS,
                    typeof(CustomSignalsCollection)),
                new AomPropertyType(DeviceEntityKeys.DeviceDataTable.DATATABLE_INDICATORS,
                    typeof(CustomIndicatorsCollections)),
                new AomPropertyType(DeviceEntityKeys.DeviceDataTable.DATATABLE_CASCADE,
                    typeof(CascadeIndicatorCollection)),

            }));
        }


        #region Implementation of ICustomDeviceSchemeTable

        /// <summary>
        /// 
        /// </summary>
        public ITagNamedObjectCollection<ICustomFider> FidersTable
        {
            get
            {
                return Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_FIDERS].Value as
                    ITagNamedObjectCollection<ICustomFider>;
            }
            set
            {
                Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_FIDERS].Value = value;
            }
        }

        public ITagNamedObjectCollection<ICustomSignal> SignalsTable
        {
            get
            {
                return Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_SIGNALS].Value as
                    ITagNamedObjectCollection<ICustomSignal>;
            }
            set
            {
                Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_SIGNALS].Value = value;
            }
        }

        public ITagNamedObjectCollection<ICustomIndicator> IndicatorsTable
        {
            get
            {
                return Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_INDICATORS].Value as
                    ITagNamedObjectCollection<ICustomIndicator>;
            }
            set
            {
                Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_INDICATORS].Value = value;
            }
        }

        public ITagNamedObjectCollection<ICascadeIndicator> CascadeIndicatorsTable
        {
            get
            {
                return Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_CASCADE].Value as
                    ITagNamedObjectCollection<ICascadeIndicator>;
            }
            set
            {
                Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_CASCADE].Value = value;
            }
        }

        #endregion
    }
}