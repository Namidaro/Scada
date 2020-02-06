using System;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events
{
    /// <summary>
    /// Exposes a server events provider functionality
    /// </summary>
    public interface IServerEventsProvider : IDisposable
    {
        /// <summary>
        /// Occures on read inut registers request
        /// </summary>
        event EventHandler<ReadInputRegistersEventArgs> OnReadInputRegistersRequest;
        /// <summary>
        /// Occures on write multiple registers request
        /// </summary>
        event EventHandler<WriteMultipleRegistersEventArgs> OnWriteMultipleRegistersRequest;
    }
}