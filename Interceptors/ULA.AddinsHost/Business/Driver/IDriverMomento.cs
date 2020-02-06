using ULA.AddinsHost.Business.Driver.Context;

namespace ULA.AddinsHost.Business.Driver
{
    /// <summary>
    ///     Exposes driver momento functionality
    ///     Описывает состояния драйвера (см. паттерн Momento)
    /// </summary>
    public interface IDriverMomento
    {
        /// <summary>
        ///     Gets or sets the state of a driver
        ///     Состояние драйвера
        /// </summary>
        IDriverContext State { get; set; }
    }
}