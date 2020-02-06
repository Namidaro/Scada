using System.Net.Configuration;
using NLog;

namespace ULA.Business.Infrastructure.DeviceDomain
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDefectState
    {
        /// <summary>
        /// неисправность управления
        /// </summary>
        bool? IsManagementDefect { get; set; }

        /// <summary>
        /// неисправность цепей управления
        /// </summary>
        bool? IsManagementChainsDefect { get; set; }
        /// <summary>
        /// неисправность предохранителей
        /// </summary>
        bool? IsFusesDefect { get; set; }
        /// <summary>
        /// неисправность охраны
        /// </summary>
        bool? IsProtectionDefect { get; set; }
        /// <summary>
        /// неисправность питания
        /// </summary>
        bool? IsNoPowerDefect { get; set; }
        /// <summary>
        /// есть ли хоть одна неисправность
        /// </summary>
        bool HasAnyDefect { get; }

        void SetLogger(Logger logger);

    }
}