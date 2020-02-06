using System;
using System.Runtime.Serialization;
using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Driver.DataTables
{
    /// <summary>
    ///     Represents data table row in the AOM notation
    ///     Представляет строку таблицы данных в AOM нотации
    /// </summary>
 
        [DataContract]
    public class AomDataTableRowEntity : AomEntity
    {
        #region [Private fields]

        private Guid? _id;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Gets the unique identifier of the data table row
        ///     Id строки данных драйвера
        /// </summary>
        public Guid Id
        {
            get
            {
                if (!this._id.HasValue)
                    // ReSharper disable AssignNullToNotNullAttribute
                    this._id = Guid.Parse(this.Properties["Id"].Value as string);
                // ReSharper restore AssignNullToNotNullAttribute
                return this._id.Value;
            }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomDataTableRowEntity" />
        /// </summary>
        /// <param name="id">
        ///     An instance of <see cref="Guid" /> that represents the unique identifier of current data table row
        /// </param>
        /// <param name="type">
        ///     An instance of <see cref="AomEntityType" /> that represents current type
        /// </param>
        public AomDataTableRowEntity(Guid id, AomEntityType type)
            : base(type)
        {
            var propertyType = new AomPropertyType("Id", typeof (string));
            if (!type.Properties.ContainsKey("Id"))
            {
                type.Properties.Add("Id", propertyType);
                this.Properties.Add("Id", new AomProperty(propertyType));
            }
            this.Properties["Id"].Value = id.ToString();
            this._id = id;
        }

        #endregion [Ctor's]

     
    }
}