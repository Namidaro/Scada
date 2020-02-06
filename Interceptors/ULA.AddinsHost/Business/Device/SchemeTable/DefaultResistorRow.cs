using System;
using System.Runtime.Serialization;

namespace ULA.AddinsHost.Business.Device.SchemeTable
{
    /// <summary>
    ///     Represent device scheme data table Resistor row functionally
    ///     Реализует строку предохранителя в таблице схемы устройства
    /// </summary>
    public class DefaultResistorRow : IDeviceResistorRow
    {
        #region [IDeviceResistor]

        /// <summary>
        ///     Id of resistor
        ///     Id предохранителя
        /// </summary>
        [DataMember(Name = "ResistorId")]
        public int ResistorId { get; set; }

        /// <summary>
        ///     Description of resistor
        ///     Описание предохранителя
        /// </summary>
        [DataMember(Name = "ResistorDescription")]
        public string ResistorDescription { get; set; }

        /// <summary>
        ///     Id of parent starter
        ///     Id пускателя которому принадлежит данный предохранитель </summary>
        [DataMember(Name = "ParentStarterId")]
        public int? ParentStarterId { get; set; }
        /// <summary>
        /// Modul 
        /// </summary>
        public int ResistorModul { get; set; }
        /// <summary>
        /// Diskret
        /// </summary>
        public int ResistorDiskret { get; set; }

        #endregion [IDeviceResistor]
    }
}
