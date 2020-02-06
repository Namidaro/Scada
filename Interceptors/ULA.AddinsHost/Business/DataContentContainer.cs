using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ULA.AddinsHost.Business
{
    /// <summary>
    ///     Represents device content container
    ///     Контейнер данных устройства
    /// </summary>
    [CollectionDataContract
        (Name = COLLECTION_DATA_CONTRACT_NAME,
        ItemName = COLLECTION_DATA_CONTRACT_ITEM_NAME,
        KeyName = COLLECTION_DATA_CONTRACT_KEY_NAME,
        ValueName = COLLECTION_DATA_CONTRACT_VALUE_NAME)]
    public class DataContentContainer : Dictionary<string, object>, IDataContentContainer
    {
        #region [Const]

        private const string COLLECTION_DATA_CONTRACT_NAME = "dataContentContainer";
        private const string COLLECTION_DATA_CONTRACT_ITEM_NAME = "data";
        private const string COLLECTION_DATA_CONTRACT_KEY_NAME = "name";
        private const string COLLECTION_DATA_CONTRACT_VALUE_NAME = "value";

        #endregion

        #region [IDataContentContainer members]

        /// <summary>
        ///     Adds value to a container
        ///     Добавить значение в контэйнер
        /// </summary>
        /// <param name="name">The name of data</param>
        /// <param name="value">The value of data</param>
        public void AddValue(string name, object value)
        {
            if (this.Keys.Contains(name))
            {
                this[name] = value;
            }
            else
            {

                this.Add(name, value);   
            }
        }


        /// <summary>
        ///     Adds value to a container
        ///     Добавить значения в контэйнер
        /// </summary>
        /// <param name="name">The name of data</param>
        /// <param name="value">The value of data</param>
        public void AddValues(string name, object value)
        {
            
            if ((value!=null)&&(value.GetType() == typeof (List<string>)))
            {
                int index = 1;
                foreach (string str in ((List<string>)value))
                {
                    if (Keys.Contains(name+index))
                    {
                        this[name + index] = str;
                    }
                    else
                    {
                        this.Add(name+index, str);
                    }
                    index++;
                }
            }

            //if (this.Keys.Contains(name))
            //{
            //    this[name] = value;
            //}
            //else
            //{
            //    this.Add(name, value);
            //}
        }

        /// <summary>
        ///     Gets a value from a container
        ///     Вернуть значение из контэйнера
        /// </summary>
        /// <typeparam name="T">
        ///     <see cref="Type" /> of value
        /// </typeparam>
        /// <param name="name">The name of data</param>
        /// <returns>Resulted data value</returns>
        public T GetValue<T>(string name)
        {
            object result;
            if (!this.TryGetValue(name, out result)) return default(T);
            return (T) result;
        }


        /// <summary>
        ///     Gets a value from a container
        ///     Вернуть значения из контэйнера
        /// </summary>
        /// <typeparam name="T">
        ///     <see cref="Type" /> of value
        /// </typeparam>
        /// <param name="name">The name of data</param>
        /// <returns>Resulted data value</returns>
        public T GetValues<T>(string name)
        {
            object result=new object();
            if(typeof(T) == typeof(List<string>))
            {
                object str = "";
                result=new List<string>();
                int index = 1;
                while (this.TryGetValue(name+index, out str))
                {
                    ((List<string>)result).Add((string)str);
                    index++;
                }
            }
            return (T)result;
            //if (!this.TryGetValue(name, out result)) return default(T);
            // return (T)result;
        }



        #endregion [IDataContentContainer members]
    }
}