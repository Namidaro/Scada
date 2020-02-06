using System.Runtime.Serialization;

namespace ULA.AddinsHost.Business.Device.SchemeTable
{
    /// <summary>
    ///     Represents device scheme data table Starter row functionally
    ///     Реализует строку пускателя в таблице схемы устройства
    /// </summary>
    public class DefaultStarterRow : IDeviceStarterRow
    {
        #region [IDeviceStarterRow]

        /// <summary>
        ///     StarterId
        ///     Id пускателя
        /// </summary>
        [DataMember(Name = "StarterId")]
        public int StarterId { get; set; }

        /// <summary>
        ///     Description of starter
        ///     Описание пускателя
        /// </summary>
        [DataMember(Name = "StarterDescription")]
        public string StarterDescription { get; set; }

        #endregion [IDeviceStarterRow]
    }
}
