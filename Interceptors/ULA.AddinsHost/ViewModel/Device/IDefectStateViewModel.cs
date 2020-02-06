using System.Windows.Input;

namespace ULA.AddinsHost.ViewModel.Device
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDefectStateViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        object Model { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void Update();

        void SetLostConnection();


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


        
    }
}