using System.Collections.Generic;
using System.Xml.Serialization;
using ULA.Business.AddressesContainer.Interfaces;

namespace ULA.Business.AddressesContainer.Entities
{
    /// <summary>
    /// контейнер для адресов состояний
    /// </summary>
    public class DateTimeContainer : IDataObjectContainer
    {
        /// <summary>
        /// контейнер для адресов состояний
        /// </summary>
        public DateTimeContainer()
        {
            Addresses = new List<AddressDataInFile>();
        }

        /// <summary>
        /// Варианты адресов
        /// </summary>
        [XmlElement("Address")]
        public List<AddressDataInFile> Addresses { get; set; }

        /// <summary>
        /// Значение адреса напрямую
        /// </summary>
        public string Value => Addresses[0].Value;

    }
}