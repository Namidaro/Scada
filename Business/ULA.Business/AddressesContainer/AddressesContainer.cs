using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ULA.AddinsHost.Business.Strings;
using ULA.Business.AddressesContainer.Entities;
using ULA.Business.AddressesContainer.Interfaces;

namespace ULA.Business.AddressesContainer
{

    /// <summary>
    /// класс для хранения адресов для различных устройств
    /// </summary>
    [Serializable]
    public class AddressesContainer
    {
        /// <summary>
        /// контейнер для неисправностей
        /// </summary>
        [XmlElement("FailsContainer")]
        public FailsContainer FailsContainer { get; set; }

        /// <summary>
        /// контейнер для даты и времени
        /// </summary>
        [XmlElement("DateTimeContainer")]
        public DateTimeContainer DateTimeContainer { get; set; }

        /// <summary>
        /// контейнер для неисправностей
        /// </summary>
        [XmlElement("StatesContainer")]
        public StatesContainer StatesContainer { get; set; }

        private List<IDataObjectContainer> GetFailsStatesContainers()
        {
            List<IDataObjectContainer> toReturn = new List<IDataObjectContainer> {FailsContainer, StatesContainer};
            return toReturn;
        }

        /// <summary>
        /// получить все адреса данных всех контейнеров
        /// </summary>
        public List<AddressData> GetAllAddressData()
        {
            List<AddressData> toRet = new List<AddressData>();
            foreach (IDataObjectContainer container in GetFailsStatesContainers())
            {
                foreach (AddressDataInFile addressDataInFile in container.Addresses)
                {
                    AddressData addressData = new AddressData();
                    string lastStr=string.Empty;
                    if (container is FailsContainer) lastStr = (AddressesStrings.SPLITTER_STR + AddressesStrings.FAIL_STR);
                    if (container is StatesContainer) lastStr = (AddressesStrings.SPLITTER_STR + AddressesStrings.STATE_STR);

                    addressData.Name = AddressesStrings.MODUL_STR + addressDataInFile.Modul + AddressesStrings.SPLITTER_STR +
                                       AddressesStrings.DISKRET_STR + addressDataInFile.Diskret + lastStr;

                    string addressString = addressDataInFile.Value;


                    string[] strings = addressString.Split(',');

                    if (strings.Length == 2)
                    {
                        int byteIndex = 0;
                        byteIndex = int.Parse(strings[0]);
                        byteIndex = byteIndex*2 - 1;
                        int bitIndex = 0;
                        bitIndex = int.Parse(strings[1]);
                        if (bitIndex >= 8)
                        {
                            byteIndex -= 1;
                            bitIndex -= 8;
                        }
                        addressData.ByteIndex = (ushort) byteIndex;
                        addressData.BitIndex = (ushort) bitIndex;
                    }
                    toRet.Add(addressData);
                }

            }
            return toRet;
        }


    }
}