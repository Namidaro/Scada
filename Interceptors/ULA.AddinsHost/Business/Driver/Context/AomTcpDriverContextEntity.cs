using System.Runtime.Serialization;
using ULA.AddinsHost.Business.Device.Context;
using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Driver.Context
{
    /// <summary>
    ///     Represents device context entity in AOM notation.
    ///     TODO: When mnore than one driver type exists this entity should be refactored!!!!!
    /// 
    ///     Представляет сущность контекста устройства в AOM нотации (см. паттерн AOM)
    /// </summary>
    [DataContract(Name = "tcpDriverContext")]
    public class AomTcpDriverContextEntity : AomEntity, IDriverContext
    {
        private IDriverContext _driverContextImplementation;

        #region [Const]

        private const string PROPERTY_TCP_ADDRESS = "TcpAddress";
        private const string PROPERTY_TCP_PORT_NUMBER = "TcpPortNumber";
        private const string PROPERTY_DEVICE_TYPE = "DeviceType";

        private IDataContentContainer _contentContainer;
        #endregion

        #region [Properties]

        /// <summary>
        ///     Gets or sets the address of tcp driver end point.
        ///     TODO: When mnore than one driver type exists this entity should be refactored!!!!!
        ///     Tcp Ip - адрес
        /// </summary>
        public string TcpAddress
        {
            get { return this.Properties[PROPERTY_TCP_ADDRESS].Value as string; }
            set { this.Properties[PROPERTY_TCP_ADDRESS].Value = value; }
        }

        /// <summary>
        ///     Gets or sets the port number of tcp driver end point.
        ///     TODO: When mnore than one driver type exists this entity should be refactored!!!!!
        /// 
        ///     Tcp порт
        /// </summary>
        public int TcpPortNumber
        {
            get { return System.Convert.ToInt32(this.Properties[PROPERTY_TCP_PORT_NUMBER].Value); }
            set { this.Properties[PROPERTY_TCP_PORT_NUMBER].Value = value; }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomDeviceContextEntity" />
        /// </summary>
        public AomTcpDriverContextEntity()
            : base(AomTcpDriverContextEntity.CreateTypeMetadata())
        {
        }

        #endregion [Ctor's]

        #region [IDriverContext members]

        /// <summary>
        ///     Gets or sets the type of current driver
        ///     Тип драйвера
        /// </summary>
        public string DriverType
        {
            get { return this.Properties[PROPERTY_DEVICE_TYPE].Value as string; }
            set { this.Properties[PROPERTY_DEVICE_TYPE].Value = value; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IDataContentContainer" /> that represents current device content
        ///     Контейнер данных специфичных для устройства
        /// </summary>
        public IDataContentContainer DataContainer
        {
            get
            {
                if (_contentContainer == null)
                {
                    _contentContainer=new DataContentContainer();
                }
                return _contentContainer;
                
            }
        }

        

        /// <summary>
        ///     Get a tcp address
        ///     Вернет TcpIp адрес
        /// </summary>
        /// <returns></returns>
        public string GetTcpAddress()
        {
            return this.TcpAddress;
        }

        /// <summary>
        ///     Get a tcp port
        ///     Вернет Tcp порт
        /// </summary>
        /// <returns></returns>
        public int GetTcpPort()
        {
            return this.TcpPortNumber;
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        ///     метод проерки двух объектов на равенство
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IDriverCommonContext other)
        {
            /*TODO: When mnore than one driver type exists this entity should be refactored to use AOM strategy model!!!!!*/
            var typedEntity = other as AomTcpDriverContextEntity;
            if (typedEntity == null) return false;
            return typedEntity.TcpAddress.Equals(this.TcpAddress) & typedEntity.TcpPortNumber.Equals(this.TcpPortNumber);
        }

        #endregion [IDriverContext members]

        #region [Help members]

        /// <summary>
        ///     СОздает метаданные для данного типа
        /// </summary>
        /// <returns></returns>
        private static AomEntityType CreateTypeMetadata()
        {
            return new AomEntityType(typeof (AomDeviceContextEntity), new AomPropertyTypeCollection(new[]
                {
                    new AomPropertyType(PROPERTY_TCP_ADDRESS, typeof (string)),
                    new AomPropertyType(PROPERTY_TCP_PORT_NUMBER, typeof (int)),
                    new AomPropertyType(PROPERTY_DEVICE_TYPE, typeof (string)),

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