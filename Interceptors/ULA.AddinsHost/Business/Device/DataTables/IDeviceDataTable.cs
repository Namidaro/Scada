using System;
using System.Collections.Generic;
using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Device.DataTables
{
    /// <summary>
    ///     Exposes a device data table functionality
    /// </summary>
    public interface ITagNamedObjectCollection<T> where T : ITagNamedObject
    {
        /// <summary>
        ///     Adds an instance of <see cref="IDeviceDataTableRow" /> to a collection of rows
        /// </summary>
        /// <param name="row">
        ///     Aninstance of <see cref="IDeviceDataTableRow" /> to add to a collection
        /// </param>
        void AddObject(T row);

        /// <summary>
        ///     Get data row by device Id and Tag
        /// </summary>
        /// <param name="tag">Tag of value</param>
        /// <returns>Requires data row</returns>
        T GetObjectByTag(string tag);

        /// <summary>
        ///     Индексатор для получения доступа к _content
        /// </summary>
        /// <param name="tag">Tag для Row</param>
        /// <returns></returns>
        T this[string tag] { get; set; }

        /// <summary>
        ///     Get Enumerator for this data table
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetEnumeratorObjects();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        bool TryDeleteObjectByTag(string tag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        bool IsObjectExists(string tag);

    }
}