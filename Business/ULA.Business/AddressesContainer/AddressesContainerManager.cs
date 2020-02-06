using System;
using System.Collections.Concurrent;
using System.IO;
using System.Xml.Serialization;

namespace ULA.Business.AddressesContainer
{
    /// <summary>
    /// класс для управления и извлечения адресов из файла
    /// </summary>
    public static class AddressesContainerManager
    {


        private static ConcurrentDictionary<string,AddressesContainer> _addressesContainerDictionary=new ConcurrentDictionary<string, AddressesContainer>();

        /// <summary>
        /// класс для управления и извлечения адресов из файла
        /// </summary>
        public static Business.AddressesContainer.AddressesContainer Load(string deviceType)
        {

            AddressesContainer addressesContainer;
            
            if (!_addressesContainerDictionary.ContainsKey(deviceType)||_addressesContainerDictionary[deviceType] == null)
            {
                try
                {
                    using (FileStream fs = File.OpenRead("Resources\\Addresses" + deviceType + ".devs"))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(AddressesContainer));
                        addressesContainer = (AddressesContainer) xs.Deserialize(fs);
                        fs.Close();
                    }

                }
                catch (Exception)
                {
                    return null;
                }
                if (!_addressesContainerDictionary.ContainsKey(deviceType))
                {
                    _addressesContainerDictionary.TryAdd(deviceType, addressesContainer);
                }
                _addressesContainerDictionary[deviceType] = addressesContainer;
            }

            return _addressesContainerDictionary[deviceType];
        }


    }
}


