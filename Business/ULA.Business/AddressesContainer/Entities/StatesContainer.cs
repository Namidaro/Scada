using System.Collections.Generic;
using System.Xml.Serialization;
using ULA.Business.AddressesContainer.Interfaces;

namespace ULA.Business.AddressesContainer.Entities
{
    /// <summary>
    /// контейнер для адресов состояний
    /// </summary>
    public class StatesContainer : IDataObjectContainer
    {
        /// <summary>
        /// контейнер для адресов состояний
        /// </summary>
        public StatesContainer()
        {
            Addresses = new List<AddressDataInFile>();
        }

        /// <summary>
        /// Варианты адресов
        /// </summary>
        [XmlElement("Address")]
        public List<AddressDataInFile> Addresses { get; set; }
    }
}