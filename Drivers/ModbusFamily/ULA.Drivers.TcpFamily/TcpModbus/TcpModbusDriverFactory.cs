using ULA.AddinsHost.Business.Driver;
using ULA.Interceptors.IoC;

namespace ULA.Drivers.TcpFamily.TcpModbus
{
    /// <summary>
    ///     Represents tcp modbus driver factory
    ///     Фабрика Modbus драйверов для разных режимов работы
    /// </summary>
    public class TcpModbusDriverFactory : ILogicalDriverFactory
    {
        #region [Private fields]

        private readonly IIoCContainer _container;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="TcpModbusDriverFactory" />
        /// </summary>
        /// <param name="container">
        ///     An instanc of <see cref="IIoCContainer" /> to use
        /// </param>
        public TcpModbusDriverFactory(IIoCContainer container)
        {
            this._container = container;
        }

        #endregion [Ctor's]

        #region [ILogicalDriverFactory members]

        /// <summary>
        ///     Creates an instance of <see cref="ConfigLogicalDriverBase" /> that represents basic configuration logical driver functionality
        /// </summary>
        /// <returns>
        ///     Created instance of <see cref="ConfigLogicalDriverBase" /> that represents basic configuration logical driver functionality
        /// </returns>
        public ConfigLogicalDriverBase CreateConfigLogicalDriver()
        {
            return this._container.Resolve<TcpModbusDriver>();
        }

        /// <summary>
        ///     Creates an instance of <see cref="RuntimeLogicalDriverBase" /> that represents basic runtime logical driver functionality
        /// </summary>
        /// <returns>
        ///     Created instance of <see cref="RuntimeLogicalDriverBase" /> that represents basic runtime logical driver functionality
        /// </returns>
        public RuntimeLogicalDriverBase CreateRuntimeLogicalDriver()
        {
            var a  = this._container.Resolve<RuntimeLogicalDriverBase>();
            return a;
        }

        #endregion [ILogicalDriverFactory members]
    }
}