using System;
using System.Xml.Serialization;

namespace ULA.Business.AddressesContainer.Entities
{
    /// <summary>
    /// класс, определяющий адресс для конкретного устройства
    /// </summary>
    [Serializable]
    public class AddressDataInFile
    {
        /// <summary>
        /// модуль
        /// </summary>
        [XmlAttribute]
        public string Modul { get; set; }


        /// <summary>
        /// дискрет
        /// </summary>
        [XmlAttribute]
        public string Diskret { get; set; }

        /// <summary>
        /// Значение адреса
        /// </summary>
        [XmlText]
        public string Value { get; set; }


    }
}