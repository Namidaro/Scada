using System;
using ULA.Business.Infrastructure.DTOs;
using ULA.Presentation.Infrastructure.DTOs;

namespace ULA.Presentation.DTOs
{
    /// <summary>
    ///     Represetns a log information
    ///     Представляет класс описывающий действие в журнале
    /// </summary>
    public class LogInformation : ILogInformation
    {
        #region [ILogInformation members]

        /// <summary>
        ///     Gets or sets the description action
        ///     Описание действия
        /// </summary>
        public string ActionDescription { get; set; }

        /// <summary>
        ///     Gets or sets thr action date
        ///     Дата действия
        /// </summary>
        public DateTime ActionDate { get; set; }

        #endregion [ILogInformation members]

    }
}