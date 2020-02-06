using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.Keys;
using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Device.DataTables
{
    /// <summary>
    ///     Represents default device data table functionality
    ///    Реализация таблицы данных устройства
    /// </summary>
    [DataContract]
    public class DefaultTagNamedObjectCollection<T> : AomEntity, ITagNamedObjectCollection<T> where T : class,ITagNamedObject
    {
    

        /// <summary>
        /// 
        /// </summary>
        public DefaultTagNamedObjectCollection() : base(DefaultTagNamedObjectCollection<T>.CreateTypeMetadata())
        {
        }





        private static AomEntityType CreateTypeMetadata()
        {
            return new AomEntityType(typeof(AomDeviceContextEntity), new AomPropertyTypeCollection(new AomPropertyType[]
            {

            }));
        }



        #region [Private fields]

        #endregion [Private fields]

        #region [ITagNamedObjectCollection members]

        /// <summary>
        ///     Adds an instance of <see cref="IDeviceDataTableRow" /> to a collection of rows
        ///     Добавить строку данных в таблицу
        /// </summary>
        /// <param name="row">
        ///     Aninstance of <see cref="IDeviceDataTableRow" /> to add to a collection
        /// </param>
        public void AddObject(T row)
        {
            if(Properties.ContainsKey(row.Tag))
                throw new ArgumentException("Collection have object with the same tag");
            AomProperty aomProperty = new AomProperty(new AomPropertyType(row.Tag, typeof(IDeviceDataTableRow)));
            aomProperty.Value = row;

            this.Properties.Add(row.Tag, aomProperty);
        }

        /// <summary>
        ///     Get data row by device Id and Tag
        ///     Вернуть строку данных из таблицы
        /// </summary>
        /// <param name="tag">Tag of value
        ///     Тэг строки</param>
        /// <returns>Requires data row or null-value when row not found</returns>
        public T GetObjectByTag(string tag)
        {
            //Когда в deviceDataTable добавиться поле deviceId добавить проверку и на него
            if (Properties.ContainsKey(tag))
            {
                return Properties[tag].Value as T;
            }
            else
            {
                return default(T);
            }
        }



        /// <summary>
        ///     Get all rows with equal device and driver id
        ///     Вернет все строки с данным driverDataId - id данного в драйвере
        /// </summary>
        /// <param name="driverDataId">driverDataId</param>
        /// <returns>all rows with equal device and driver id</returns>
        public IEnumerable<T> GetDataRowsByDriverData(Guid driverDataId)
        {
            //Когда в deviceDataTable добавиться поле deviceId и driverId добавить проверку и на него
            return from property in Properties where (property.Value as IDeviceDataTableRow).DriverDataId.Equals(driverDataId) select property.Value as T;
        }

        /// <summary>
        ///     Индексатор для получения доступа к _content
        /// </summary>
        /// <param name="tag">Tag для Row</param>
        /// <returns></returns>
        public T this[string tag]
        {
            get { return GetObjectByTag(tag); }
            set
            {
                if (!Properties.ContainsKey(tag))
                {
                    AddObject(value);
                }
                else
                {
                    Properties[tag].Value = value;
                }
            }
        }

        /// <summary>
        ///     Get Enumerator for this data table
        ///     Вернет IEnumerable для таблицы данных устройства
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetEnumeratorObjects()
        {
            return from property in Properties select property.Value.Value as T;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool TryDeleteObjectByTag(string tag)
        {
            if (!Properties.ContainsKey(tag))
            {
                this.Properties.Remove(tag);
                return true;
            }
            return false;
        }

        public bool IsObjectExists(string tag)
        {
            return Properties.ContainsKey(tag);
        }

        #endregion [ITagNamedObjectCollection members]


    }
}