using System;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Driver.Context;
using ULA.AddinsHost.Business.Driver.DataTables;
using ULA.Common.AOM;
using YP.Toolkit.System.ParallelExtensions;

namespace ULA.AddinsHost.Business.Driver
{
    /// <summary>
    ///     Represents basic logical driver finctionality in runtime mode
    ///     Представляет логический драйвер в режиме реального времени
    /// </summary>
    public class RuntimeLogicalDriverBase : IRuntimeLogicalDriver
    {
        #region [Private fields]

        private IDriverContext _context;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Gets an instance of <see cref="IDataContentContainer" /> that represents current device data container
        /// </summary>
        protected IDataContentContainer DataContainer
        {
            get { return this._context.DataContainer; }
        }

        #endregion [Properties]

        #region [Public members]

        /// <summary>
        ///     Initialize current driver asynchronously
        ///     Асинхронная инициализация драйвера
        /// </summary>
        /// <param name="context">
        ///     An instance of <see cref="IDriverContext" /> that represents current driver context
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task InitializeAsync(IDriverContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            this._context = context;
            var dataTableRowType = this.CreateDataTableRowType();
            if (dataTableRowType != null)
            {
                this._context.DataContainer.AddValue("DataTable", new DriverDataTable(dataTableRowType));
            }
            await this.OnInitializeAsync();
        }

        #endregion [Public members]

        #region [IRuntimeLogicalDriver members]

        /// <summary>
        ///     Gets or sets an instance of <see cref="Guid" /> that represents driver's unique identifier
        ///     Id драйвера
        /// </summary>
        Guid ILogicalDriver.DriverId { get; set; }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        ///     Проверяет или текущий объект идентичен другому объекту такого же типа
        /// </summary>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(IDriverCommonContext other)
        {
            return other != null && other.Equals(this._context);
        }

        /// <summary>
        ///     Creates an instance of <see cref="IDriverMomento" /> that represents current driver state
        ///     Возвращает состояние драйвера
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IDriverMomento" /> that represents current driver state
        /// </returns>
        public IDriverMomento CreateMomento()
        {
            return new DefaultRuntimeLogicalDriverMomento(this._context);
        }

        /// <summary>
        ///     Sets a momento to current driver
        ///     Сохраняет состояние драйвера
        /// </summary>
        /// <param name="momento">
        ///     An instance of <see cref="IDriverMomento" /> that represents current the state of current driver
        /// </param>
        public void SetMomento(IDriverMomento momento)
        {
            if (momento == null) throw new ArgumentNullException("momento");
            var context = momento.State;
            if (context == null) throw new ArgumentException();
            this._context = context;
        }

        #endregion [IRuntimeLogicalDriver members]

        #region [Templated members]

        /// <summary>
        ///     Initialize current driver asynchronously
        ///     Асинхронно инициализирует драйвер
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        protected virtual Task OnInitializeAsync()
        {
            return CompletedTask.Default;
        }

        /// <summary>
        ///     Creates an instance of <see cref="AomEntityType" /> that represents current driver data table row template
        ///     Создает шаблон строки данных из таблицы данных драйвера
        /// </summary>
        /// <returns>
        ///     Created instance of <see cref="AomEntityType" /> that represents current driver data table row template
        /// </returns>
        protected virtual AomEntityType CreateDataTableRowType()
        {
            return null;
        }

        #endregion [Templated members]
    }
}
