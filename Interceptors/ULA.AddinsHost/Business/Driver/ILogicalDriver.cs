using System;

namespace ULA.AddinsHost.Business.Driver
{
    /// <summary>
    ///     Exposes basic driver functinality
    ///     Описывает базовую функциональность драйвера
    /// </summary>
    public interface ILogicalDriver
    {
        /// <summary>
        ///     Gets or sets an instance of <see cref="Guid" /> that represents driver's unique identifier
        ///     Id драйвера
        /// </summary>
        Guid DriverId { get; set; }
    }
}