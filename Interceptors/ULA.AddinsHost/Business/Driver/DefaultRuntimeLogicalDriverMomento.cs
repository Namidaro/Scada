using System.Runtime.Serialization;
using ULA.AddinsHost.Business.Driver.Context;

namespace ULA.AddinsHost.Business.Driver
{
    /// <summary>
    ///     Represents default configuration logical driver momento functionality
    ///     Представляет состояние логического драйвера в режиме реального времени
    /// </summary>
    [DataContract]
    public class DefaultRuntimeLogicalDriverMomento : IDriverMomento
    {
        #region [Const]

        private const string CONTEXT_NAME = "driverContext";

        #endregion

        #region [Private fields]

        [DataMember(Name = CONTEXT_NAME)]
        private object _context;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="DefaultRuntimeLogicalDriverMomento" />
        /// </summary>
        /// <param name="context">
        ///     An instance of <see cref="IDriverContext" /> that represents the state of a driver
        /// </param>
        public DefaultRuntimeLogicalDriverMomento(IDriverContext context)
        {
            this._context = context;
        }

        #endregion [Ctor's]

        #region [IDriverMomento members]

        /// <summary>
        ///     Gets or sets the state of a driver
        ///     Состояние драйвера
        /// </summary>
        public IDriverContext State
        {
            get { return this._context as IDriverContext; }
            set { this._context = value; }
        }

        #endregion [IDriverMomento members]
    }
}
