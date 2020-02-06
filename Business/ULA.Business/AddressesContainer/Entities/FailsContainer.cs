using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ULA.Business.AddressesContainer.Interfaces;

namespace ULA.Business.AddressesContainer.Entities
{
    /// <summary>
    /// контейнер для неисправностей
    /// </summary>
    [Serializable]
    public class FailsContainer : IDataObjectContainer
    {
        /// <summary>
        /// контейнер для неисправностей
        /// </summary>
        public FailsContainer()
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