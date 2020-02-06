using System;

namespace ULA.Business.Infrastructure.DTOs
{
    /// <summary>
    ///   Log information
    ///   Описывает информацию для журналирования
    /// </summary>
    public interface ILogInformation
    {
        /// <summary>
        ///     Gets or sets the description action
        ///     Описание действия
        /// </summary>
        string ActionDescription { get; set; }

        /// <summary>
        ///     Gets or sets thr action date
        ///     Дата действия
        /// </summary>
        DateTime ActionDate { get; set; }
    }
}