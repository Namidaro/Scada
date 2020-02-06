using System.Threading.Tasks;
using ULA.AddinsHost.Business.Driver;

namespace ULA.Business.Infrastructure.PersistanceServices
{
    /// <summary>
    ///     Exposes driver persisting context functionality
    /// </summary>
    public interface IDriverPersistableContext
    {
        /// <summary>
        ///     Saves an instance of <see cref="IDriverMomento" /> that represents drivers state asynchronously
        /// </summary>
        /// <param name="driverMomento">
        ///     An instance of <see cref="IDriverMomento" /> to save
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task SaveDriverMomentoAsync(IDriverMomento driverMomento);

        /// <summary>
        ///     Restores an instance of <see cref="IDriverMomento" /> that reporesents drivber's state
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task<IDriverMomento> GetDriverMomentoAsync();
    }
}