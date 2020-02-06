using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Modbus;
using YP.Toolkit.System.Tools.Patterns;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide
{
    /// <summary>
    /// Represents a basic client modbus facade functionality
    /// </summary>
    public class ModbusClientFacadeBase : Disposable
    {
        #region [Protected fields]
        /// <summary>
        /// An instance of client modbus basic implementation to create facaade for
        /// </summary>
        protected ModbusClientBase _modbus;
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Protected fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of a basic client modbus facade. 
        /// </summary>
        /// <param name="modbus">The modbus protocol functionality to use.</param>
        protected ModbusClientFacadeBase(ModbusClientBase modbus)
        {
            this._modbus = modbus;
        } 
        #endregion [Ctor's]


        #region [Help members]
        /// <summary>
        /// Validates the request data.
        /// </summary>
        /// <typeparam name="T">The type of data to validate.</typeparam>
        /// <param name="data">The data to validate.</param>
        /// <param name="maxDataLength">The maximum data length.</param>
        protected void ValidateData<T>(T[] data, int maxDataLength)
        {
            if (data == null || data.Length == 0 || data.Length > maxDataLength)
            {
                var errorMessage = string.Format("The data length in client request must be between 1 and {0} inclusive.", maxDataLength);
                this._logger.Log(errorMessage, Category.ERROR, Priority.HIGH);
                Guard.Throw<ClientModbusRequestException>(errorMessage);
            }
        }
        /// <summary>
        /// Validates the number of request points.
        /// </summary>
        /// <param name="numberOfPoints">The request number of points to validate.</param>
        /// <param name="maxNumberOfPoints">The maximum number of points that available for processing.</param>
        protected void ValidateNumberOfPoints(ushort numberOfPoints, ushort maxNumberOfPoints)
        {
            if (numberOfPoints < 1 || numberOfPoints > maxNumberOfPoints)
            {
                var errorMessage = string.Format("The number of points in client request must be between 1 and {0} inclusive.", maxNumberOfPoints);
                this._logger.Log(errorMessage, Category.ERROR, Priority.HIGH);
                Guard.Throw<ClientModbusRequestException>(errorMessage);
            }
        }
        #endregion [Help members]
        

        #region [Override members]
        /// <summary>
        /// Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this.ThrowIfDisposed();
            if (this._modbus != null)
                this._modbus.Dispose();
            this._modbus = null;
            base.OnDisposing();
        } 
        #endregion [Override members]
    }
}