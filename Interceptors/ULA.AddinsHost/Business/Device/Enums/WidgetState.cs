using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULA.AddinsHost.Business.Device.Enums
{
    /// <summary>
    ///     Represent a state of widget(device)
    ///     Представляет набор состояний устройства(нормально, авария, нет связи, ремонт)
    /// </summary>
    public enum WidgetState
    {
        /// <summary>
        /// в нормальном режиме
        /// </summary>
        Norm,
        /// <summary>
        /// в режиме аварии
        /// </summary>
        Crash,
        /// <summary>
        /// режим нет подключения
        /// </summary>
        NoConnection,
        /// <summary>
        /// режим ремонта
        /// </summary>
        Repair
    }
}
