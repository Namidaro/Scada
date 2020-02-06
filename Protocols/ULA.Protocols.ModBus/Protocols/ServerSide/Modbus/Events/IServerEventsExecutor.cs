namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events
{
    /// <summary>
    /// Exposes a server event execution functionality
    /// </summary>
    public interface IServerEventsExecutor
    {
        /// <summary>
        /// Executes OnReadInputRegistersRequest event
        /// </summary>
        /// <param name="args">An instance of OnReadInputRegistersRequest arguments</param>
        void OnReadInputRegistersRequest(ReadInputRegistersEventArgs args);
        /// <summary>
        /// Executes OnWriteMultipleRegistersRequest event
        /// </summary>
        /// <param name="args">An instance of OnWriteMultipleRegistersRequest arguments</param>
        void OnWriteMultipleRegistersRequest(WriteMultipleRegistersEventArgs args);

    }
}