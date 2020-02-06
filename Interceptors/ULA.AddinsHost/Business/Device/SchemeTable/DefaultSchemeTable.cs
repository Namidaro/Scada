using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ULA.AddinsHost.Business.Device.SchemeTable
{
    /// <summary>
    ///     Represent a device scheme table functionality
    ///     Реализация талицы схемы устройства
    /// </summary>

    public class DefaultSchemeTable : IConfiguratedDeviceSchemeTable
    {
        #region [Private fields]

        private List<IDeviceStarterRow>  _starters = new List<IDeviceStarterRow>();
        private List<IDeviceResistorRow> _resistors = new List<IDeviceResistorRow>();
        private List<IDeviceResistorRow> _managementResistors = new List<IDeviceResistorRow>();


        #endregion [Private fields]

        #region [IConfiguratedDeviceSchemeTable]

        /// <summary>
        ///     Add instance of <see cref="IDeviceStarterRow"/> to scheme table. MaxSize = 2
        ///     Добавить строку-пускатель в таблицу схемы
        /// </summary>
        /// <param name="starterRow">Adding instance of <see cref="IDeviceStarterRow"/></param>
        public void AddStarterRow(IDeviceStarterRow starterRow)
        {
            if (starterRow == null)
            {
                throw new ArgumentNullException("AddStarterRow argument must be not null");
            }
            if (this._starters.Count >= 5)
            {
                throw new IndexOutOfRangeException("Max count of starters is 5");
            }
            this._starters.Add(starterRow);
        }

        /// <summary>
        ///     Add instance of <see cref="IDeviceResistorRow"/> to scheme table
        /// Добавить строку-предохранитель в таблицу схемы
        /// </summary>
        /// <param name="resistorRow">Adding instance of <see cref="IDeviceResistorRow"/></param>
        public void AddResistorRow(IDeviceResistorRow resistorRow)
        {
            if (resistorRow == null)
            {
                throw new ArgumentNullException("AddResistorRow argument must be not null");
            }
            if (_resistors.FirstOrDefault(p => p.ResistorId == resistorRow.ResistorId) != null) return;

            this._resistors.Add(resistorRow);
        }
     

        /// <summary>
        ///     Get a instance of <see cref="IDeviceStarterRow"/> from device scheme table. Если Пускателя с таким id нет, то вернет null
        /// Вернуть строку-пускатель из таблицы схемы(по Id)
        /// </summary>
        /// <param name="starterId">Id of requered starter</param>
        /// <returns>Requeres instance of <see cref="IDeviceStarterRow"/> or null if this id not found</returns>
        public IDeviceStarterRow GetStarterRow(int starterId)
        {
            if (this._starters.Any(a => a.StarterId.Equals(starterId)))
            {
                return this._starters.Find(a => a.StarterId.Equals(starterId));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///     Get a instance of <see cref="IDeviceResistorRow"/> from device scheme table. Если резистора с таким id нет, то вернет null
        /// Вернуть строку неиспользованного предохранителя из таблицы схемы(по Id)
        /// </summary>
        /// <param name="resistorId">Id of requered resistor</param>
        /// <returns>Requeres instance of <see cref="IDeviceResistorRow"/> or null if this id not found</returns>
        public IDeviceResistorRow GetResistorRow(int resistorId)
        {
            if (this._resistors.Any(res => res.ResistorId.Equals(resistorId)))
            {
                return this._resistors.Find(res => res.ResistorId.Equals(resistorId));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///     Get a IEnumerable for instancs IDeviceResistorRow  
        ///     Вернет итератор по предохраниелям
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDeviceResistorRow> GetResistorEnumerable()
        {
            return this._resistors.AsEnumerable();
        }
 

        /// <summary>
        ///      Get a instance of <see cref="IDeviceStarterRow"/> from device scheme table
        ///     Вернет итератор по пускателям
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDeviceStarterRow> GetChannelsEnumerable()
        {
            return this._starters.AsEnumerable();
        }

        /// <summary>
        ///     Get starter with index
        ///     Вернет пускатель по его индексу
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>starter</returns>
        public IDeviceStarterRow GetStarterByIndex(int index)
        {
            return this._starters[index];
        }

        /// <summary>
        ///     Get resistor with index
        ///     Вернет предохранитель по индексу
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>resistor</returns>
        public IDeviceResistorRow GetResistorByIndex(int index)
        {
            return this._resistors[index];
        }

        #endregion [IConfiguratedDeviceSchemeTable]
    }
}
