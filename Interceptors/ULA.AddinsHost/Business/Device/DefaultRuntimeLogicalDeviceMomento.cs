using System.Runtime.Serialization;
using ULA.AddinsHost.Business.Device.Context;

namespace ULA.AddinsHost.Business.Device
{
    /// <summary>
    ///     Represents default configuration logical device momento functionality
    ///     Представляет состояние логического устройства в режиме реального времени(см. паттрн Momento)
    /// </summary>
    [DataContract]
    public class DefaultRuntimeLogicalDeviceMomento : IDeviceMomento
    {
         #region [Const]

        private const string STATE_NAME = "deviceContext";
        #endregion

        #region [Private fields]

        [DataMember(Name = STATE_NAME)]
        private object _state;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="DefaultConfigLogicalDeviceMomento" />
        /// </summary>
        /// <param name="state">
        ///     An instance of <see cref="IDeviceContext" /> that represents a device state
        /// </param>
        public DefaultRuntimeLogicalDeviceMomento(IDeviceContext state)
        {
            this._state = state;
        }

        #endregion [Ctor's]

        #region [IDeviceMomento members]

        /// <summary>
        ///     Gets the state of a momento object
        ///     Возвращает состояние(контекст) увтройства
        /// </summary>
        public IDeviceContext State
        {
            get { return this._state as IDeviceContext; }
        }

        #endregion [IDeviceMomento members]
    }
}
