using System;

namespace ULA.AddinsHost.Business.Driver.Context
{
    /// <summary>
    ///     Represents device context entity in AOM notation builder functionality.
    ///     TODO: When more than one driver type exists this builder should be refactored!!!!
    ///     Представляет строителя сущности контекста устройства в AOM нотации(см. паттерн AOM)
    /// </summary>
    public class AomTcpDriverContextEntityBuilder
    {
        #region [Private fields]

        private string _address;
        private string _driverType;
        private int _portNumber;

        #endregion [Private fields]

        #region [Public members]

        /// <summary>
        ///     Sets the tcp address value of tcp driver's endpoint
        ///     установка Tcp Ip адреса
        /// </summary>
        /// <param name="address">The tcp address value to set</param>
        public void SetTcpAddress(string address)
        {
            if (address == null) throw new ArgumentNullException("address");
            this._address = address;
        }

        /// <summary>
        ///     Sets the tcp port number value of tcp driver's endpoint
        ///     Установка Tcp порт
        /// </summary>
        /// <param name="portNumber">The tcp port number value</param>
        public void SetTcpPortNumber(int portNumber)
        {
            if (portNumber < 0) throw new ArgumentOutOfRangeException("portNumber");
            this._portNumber = portNumber;
        }

        /// <summary>
        ///     Sets the type of the device
        ///     Установка типа драйвера
        /// </summary>
        /// <param name="driverType">The type of the device</param>
        public void SetDriverType(string driverType)
        {
            if (driverType == null) throw new ArgumentNullException("driverType");
            this._driverType = driverType;
        }

        /// <summary>
        ///     Builds an instance of <see cref="AomTcpDriverContextEntity" />
        ///     Создает сущность контекста драйвера(<see cref="AomTcpDriverContextEntity" />)
        /// </summary>
        /// <returns>
        ///     Built instance of <see cref="AomTcpDriverContextEntity" />
        /// </returns>
        public AomTcpDriverContextEntity Build()
        {
            var result = new AomTcpDriverContextEntity
                {
                    DriverType = this._driverType,
                    TcpAddress = this._address,
                    TcpPortNumber = this._portNumber
                };
            return result;
        }

        #endregion [Public members]
    }
}