namespace ULA.Business.AddressesContainer.Entities
{
    /// <summary>
/// определяет данные адреса через стрингу, смещение байта и бита
/// </summary>
   public class AddressData
    {
        /// <summary>
        /// наименование элемента, к которому относится адрес
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// индекс байта
        /// </summary>
        public ushort ByteIndex { get; set; }
        /// <summary>
        /// индекс бита
        /// </summary>
        public ushort BitIndex { get; set; }
        /// <summary>
        /// номер в памяти
        /// </summary>
        public int? Number { get; set; }
    }
}
