using System;
using System.Runtime.Serialization;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.Business.Device.Keys;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Device.Context
{
    /// <summary>
    ///     Represents device context entity in AOM notation
    ///     Представляет контекст сущности устройства в AOM нотации (см. AOM паттерн)
    /// </summary>
    [DataContract]
    public class AomDeviceContextEntity : AomEntity, IDeviceContext
    {
      

        #region [Const]

        private const string PROPERTY_DEVICE_NAME = "DeviceName";
        private const string PROPERTY_DEVICE_DESCRIPTION = "DeviceDescription";
        private const string PROPERTY_DEVICE_TYPE = "DeviceType";
        private const string PROPERTY_ANALOG_METER_TYPE = "AnalogMeterType";
        private const string PROPERTY_RELATED_DRIVER_ID = "RelatedDriverId";
        private const string PROPERTY_DEVICE_NUMBER = "DeviceNumber";
        private const string STARTER1_DESCRIPTION = "Starter1Description";
        private const string STARTER2_DESCRIPTION = "Starter2Description";
        private const string STARTER3_DESCRIPTION = "Starter3Description";

        private const string TRANSFORM_KOEF_A = "TransformKoefA";
        private const string TRANSFORM_KOEF_B = "TransformKoefB";
        private const string TRANSFORM_KOEF_C = "TransformKoefC";


        private const string CHANNEL_NUMBER_OF_STARTER1 = "ChannelNumberOfStarter1";
        private const string CHANNEL_NUMBER_OF_STARTER2 = "ChannelNumberOfStarter2";
        private const string CHANNEL_NUMBER_OF_STARTER3 = "ChannelNumberOfStarter3";



        private ITagNamedObjectCollection<IDeviceDataTableRow> _dataTable;
        private IDataContentContainer _dataContentContainer;
        private string _channelNumberOfStarter1;
        private string _channelNumberOfStarter2;
        private string _channelNumberOfStarter3;

        #endregion

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomDeviceContextEntity" />
        /// </summary>
        public AomDeviceContextEntity()
            : base(AomDeviceContextEntity.CreateTypeMetadata())
        {
        }

        #endregion [Ctor's]

        #region [IDeviceContext members]

       
        /// <summary>
        ///     Gets or sets the name of current device
        ///     Имя устройства
        /// </summary>
        public string DeviceName
        {
            get { return this.Properties[PROPERTY_DEVICE_NAME].Value as string; }
            set { this.Properties[PROPERTY_DEVICE_NAME].Value = value; }
        }

        /// <summary>
        ///     Gets or sets the desciption of current device
        ///     описание устройства
        /// </summary>
        public string DeviceDescription
        {
            get { return this.Properties[PROPERTY_DEVICE_DESCRIPTION].Value as string; }
            set { this.Properties[PROPERTY_DEVICE_DESCRIPTION].Value = value; }
        }

        /// <summary>
        ///     Gets or sets the type of current device
        ///     Тип устройства
        /// </summary>
        public string DeviceType
        {
            get { return this.Properties[PROPERTY_DEVICE_TYPE].Value as string; }
            set { this.Properties[PROPERTY_DEVICE_TYPE].Value = value; }
        }

        /// <summary>
        ///     Gets or sets the type of current device meter
        ///     Тип счетчика устройства
        /// </summary>
        public string AnalogMeterType
        {
            get { return this.Properties[PROPERTY_ANALOG_METER_TYPE].Value as string; }
            set { this.Properties[PROPERTY_ANALOG_METER_TYPE].Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RelatedDriverId
        {
            get { return this.Properties[PROPERTY_RELATED_DRIVER_ID].Value as string; }
            set { this.Properties[PROPERTY_RELATED_DRIVER_ID].Value = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Starter1Description
        {
            get
            {
                if (!Properties.ContainsKey(STARTER1_DESCRIPTION))
                {
                    Properties.Add(STARTER1_DESCRIPTION, new AomProperty(new AomPropertyType(STARTER1_DESCRIPTION, typeof(string))));
                }
                return this.Properties[STARTER1_DESCRIPTION].Value as string;
                
            }
            set { this.Properties[STARTER1_DESCRIPTION].Value = value; }
        }
        public string Starter2Description
        {
            get
            {
                if (!Properties.ContainsKey(STARTER2_DESCRIPTION))
                {
                    Properties.Add(STARTER2_DESCRIPTION, new AomProperty(new AomPropertyType(STARTER2_DESCRIPTION, typeof(string))));
                }
                return this.Properties[STARTER2_DESCRIPTION].Value as string; 
                
            }
            set { this.Properties[STARTER2_DESCRIPTION].Value = value; }
        }
        public string Starter3Description
        {
            get
            {
                if (!Properties.ContainsKey(STARTER3_DESCRIPTION))
                {
                    Properties.Add(STARTER3_DESCRIPTION,new AomProperty(new AomPropertyType(STARTER3_DESCRIPTION, typeof(string))));
                }
                return this.Properties[STARTER3_DESCRIPTION].Value as string;
                
            }
            set { this.Properties[STARTER3_DESCRIPTION].Value = value; }
        }

        public string ChannelNumberOfStarter1
        {
            get
            {
                if (!Properties.ContainsKey(CHANNEL_NUMBER_OF_STARTER1))
                {
                    Properties.Add(CHANNEL_NUMBER_OF_STARTER1, new AomProperty(new AomPropertyType(CHANNEL_NUMBER_OF_STARTER1, typeof(string))));
                    this.Properties[CHANNEL_NUMBER_OF_STARTER1].Value = "1";
                }
                return this.Properties[CHANNEL_NUMBER_OF_STARTER1].Value.ToString();

            }
            set
            {
                if (!Properties.ContainsKey(CHANNEL_NUMBER_OF_STARTER1))
                {
                    Properties.Add(CHANNEL_NUMBER_OF_STARTER1, new AomProperty(new AomPropertyType(CHANNEL_NUMBER_OF_STARTER1, typeof(string))));
                    this.Properties[CHANNEL_NUMBER_OF_STARTER1].Value = "1";
                }
                this.Properties[CHANNEL_NUMBER_OF_STARTER1].Value = value.ToString();

            }
        }

        public string ChannelNumberOfStarter2
        {
            get
            {
                if (!Properties.ContainsKey(CHANNEL_NUMBER_OF_STARTER2))
                {
                    Properties.Add(CHANNEL_NUMBER_OF_STARTER2, new AomProperty(new AomPropertyType(CHANNEL_NUMBER_OF_STARTER2, typeof(string))));
                    this.Properties[CHANNEL_NUMBER_OF_STARTER2].Value = "2";
                }
                return this.Properties[CHANNEL_NUMBER_OF_STARTER2].Value.ToString();

            }
            set
            {
                if (!Properties.ContainsKey(CHANNEL_NUMBER_OF_STARTER2))
                {
                    Properties.Add(CHANNEL_NUMBER_OF_STARTER2, new AomProperty(new AomPropertyType(CHANNEL_NUMBER_OF_STARTER2, typeof(string))));
                    this.Properties[CHANNEL_NUMBER_OF_STARTER2].Value = "2";
                }
                this.Properties[CHANNEL_NUMBER_OF_STARTER2].Value = value.ToString();

            }
        }

        public string ChannelNumberOfStarter3
        {
            get
            {
                if (!Properties.ContainsKey(CHANNEL_NUMBER_OF_STARTER3))
                {
                    Properties.Add(CHANNEL_NUMBER_OF_STARTER3, new AomProperty(new AomPropertyType(CHANNEL_NUMBER_OF_STARTER3, typeof(string))));
                    this.Properties[CHANNEL_NUMBER_OF_STARTER3].Value = "3";
                }
                return this.Properties[CHANNEL_NUMBER_OF_STARTER3].Value.ToString();

            }
            set
            {
                if (!Properties.ContainsKey(CHANNEL_NUMBER_OF_STARTER3))
                {
                    Properties.Add(CHANNEL_NUMBER_OF_STARTER3, new AomProperty(new AomPropertyType(CHANNEL_NUMBER_OF_STARTER3, typeof(string))));
                    this.Properties[CHANNEL_NUMBER_OF_STARTER3].Value = "3";
                }
                this.Properties[CHANNEL_NUMBER_OF_STARTER3].Value = value.ToString();

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int TransformKoefA
        {
            get
            {
                if (!Properties.ContainsKey(TRANSFORM_KOEF_A))
                {
                    Properties.Add(TRANSFORM_KOEF_A, new AomProperty(new AomPropertyType(TRANSFORM_KOEF_A, typeof(string))));
                    this.Properties[TRANSFORM_KOEF_A].Value = "0";
                }
                return int.Parse(this.Properties[TRANSFORM_KOEF_A].Value.ToString());

            }
            set
            {
                if (!Properties.ContainsKey(TRANSFORM_KOEF_A))
                {
                    Properties.Add(TRANSFORM_KOEF_A, new AomProperty(new AomPropertyType(TRANSFORM_KOEF_A, typeof(string))));
                    this.Properties[TRANSFORM_KOEF_A].Value = "0";
                }
                this.Properties[TRANSFORM_KOEF_A].Value = value.ToString(); 
                
            }
        }
        public int TransformKoefB
        {
            get
            {
                if (!Properties.ContainsKey(TRANSFORM_KOEF_B))
                {
                    Properties.Add(TRANSFORM_KOEF_B, new AomProperty(new AomPropertyType(TRANSFORM_KOEF_B, typeof(string))));
                    this.Properties[TRANSFORM_KOEF_B].Value = "0";
                }
                return int.Parse(this.Properties[TRANSFORM_KOEF_B].Value.ToString());

            }
            set
            {
                if (!Properties.ContainsKey(TRANSFORM_KOEF_B))
                {
                    Properties.Add(TRANSFORM_KOEF_B, new AomProperty(new AomPropertyType(TRANSFORM_KOEF_B, typeof(string))));
                    this.Properties[TRANSFORM_KOEF_B].Value = "0";
                }
                this.Properties[TRANSFORM_KOEF_B].Value = value.ToString(); 
                
            }
        }
        public int TransformKoefC
        {
            get
            {
                if (!Properties.ContainsKey(TRANSFORM_KOEF_C))
                {
                    Properties.Add(TRANSFORM_KOEF_C, new AomProperty(new AomPropertyType(TRANSFORM_KOEF_C, typeof(string))));
                    this.Properties[TRANSFORM_KOEF_C].Value = "0";
                }
                return int.Parse(this.Properties[TRANSFORM_KOEF_C].Value.ToString());

            }
            set
            {
                if (!Properties.ContainsKey(TRANSFORM_KOEF_C))
                {
                    Properties.Add(TRANSFORM_KOEF_C, new AomProperty(new AomPropertyType(TRANSFORM_KOEF_C, typeof(string))));
                    this.Properties[TRANSFORM_KOEF_C].Value = "0";
                }
                this.Properties[TRANSFORM_KOEF_C].Value = value.ToString();
                
            }
        }








        /// <summary>
        /// номер устройства
        /// </summary>
        public int DeviceNumber
        {
            get
            {
                var s=  this.Properties[PROPERTY_DEVICE_NUMBER].Value ;
                if (s != null) return int.Parse(s.ToString());
                throw new ArgumentException();
            }
            set
            {
                this.Properties[PROPERTY_DEVICE_NUMBER].Value = value.ToString();

            }
        }


        /// <summary>
        ///     Gets or sets an instance  that represents current device data table
        ///     Таблица данных устройства
        /// </summary>
        public ITagNamedObjectCollection<IDeviceDataTableRow> DataTable
        {
            get
            {
                if (_dataTable == null)
                {
                    _dataTable=new DefaultTagNamedObjectCollection<IDeviceDataTableRow>();
                }
                return _dataTable;
                
            }
        }
        
        /// <summary>
        ///     Gets or sets an instance of <see cref="IConfiguratedDeviceSchemeTable" /> that represents current device scheme table
        ///     Таблица схемы устройства
        /// </summary>
        public IConfiguratedDeviceSchemeTable SchemeTable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public ICustomDeviceSchemeTable CustomDeviceSchemeTable { get; set; }

     

        #endregion [IDeviceContext members]

        #region [Help members]

        /// <summary>
        ///     Создает метаданные для устройства
        /// </summary>
        /// <returns></returns>
        private static AomEntityType CreateTypeMetadata()
        {
            return new AomEntityType(typeof(AomDeviceContextEntity), new AomPropertyTypeCollection(new[]
                {
                    new AomPropertyType(PROPERTY_DEVICE_NAME, typeof (string)),
                    new AomPropertyType(PROPERTY_DEVICE_DESCRIPTION, typeof (string)),
                    new AomPropertyType(PROPERTY_DEVICE_TYPE, typeof (string)),
                    new AomPropertyType(PROPERTY_ANALOG_METER_TYPE, typeof (string)),
                    new AomPropertyType(PROPERTY_RELATED_DRIVER_ID, typeof (string)),
                    new AomPropertyType(PROPERTY_DEVICE_NUMBER, typeof (string)),
                    new AomPropertyType(STARTER1_DESCRIPTION, typeof (string)),
                    new AomPropertyType(STARTER2_DESCRIPTION, typeof (string)),
                    new AomPropertyType(STARTER3_DESCRIPTION, typeof (string)),

                }));
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            /*none*/
        }

        #endregion [Help members]
    }
}