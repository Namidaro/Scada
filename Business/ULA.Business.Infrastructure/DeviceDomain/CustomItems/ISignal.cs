namespace ULA.Business.Infrastructure.DeviceDomain
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISignal
    {
        /// <summary>
        /// описание сигнала
        /// </summary>
        string SignalDescription { get; set; }
        /// <summary>
        /// состояние сигнала
        /// </summary>
        bool SignalState { get; set; }

    }
}