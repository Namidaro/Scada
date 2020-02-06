using System.Collections.Generic;
using ULA.Business.AddressesContainer.Entities;

namespace ULA.Business.AddressesContainer.Interfaces
{
    /// <summary>
    /// интерфейс, описывающий контейнеры для объектов данных адресов
    /// </summary>
    public interface IDataObjectContainer
    {
        /// <summary>
        /// содержащиеся обхекты данных адресов
        /// </summary>
        List<AddressDataInFile> Addresses { get; set; }
    }
}