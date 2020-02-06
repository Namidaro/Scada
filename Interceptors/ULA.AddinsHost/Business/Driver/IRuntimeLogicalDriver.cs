using System;
using ULA.AddinsHost.Business.Driver.Context;

namespace ULA.AddinsHost.Business.Driver
{
    /// <summary>
    ///     Exposes driver functionality in the runtime mode
    ///     Описывает функционал логического драйвера в режиме реального времени
    /// </summary>
    public interface IRuntimeLogicalDriver : ILogicalDriver, IEquatable<IDriverCommonContext>
    {
        /// <summary>
        ///     Creates an instance of <see cref="IDriverMomento" /> that represents current driver state
        ///     Вернет состояние драйвера
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IDriverMomento" /> that represents current driver state
        /// </returns>
        IDriverMomento CreateMomento();

        /// <summary>
        ///     Sets a momento to current driver
        ///     Сохраняет состояние драйвера
        /// </summary>
        /// <param name="momento">
        ///     An instance of <see cref="IDriverMomento" /> that represents current the state of current driver
        /// </param>
        void SetMomento(IDriverMomento momento);
    }
}
