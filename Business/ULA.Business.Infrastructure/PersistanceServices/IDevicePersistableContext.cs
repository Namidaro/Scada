using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device;

namespace ULA.Business.Infrastructure.PersistanceServices
{
    /// <summary>
    ///     Exposes deviceViewModel persisting context functionality
    /// </summary>
    public interface IDevicePersistableContext
    {
        /// <summary>
        ///     Saves deviceViewModel momento asynchronously
        /// </summary>
        /// <param name="deviceMomento">
        ///     An instance of <see cref="IDeviceMomento" /> that represents deviceViewModel momento
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        void SaveDeviceMomentoAsync(IDeviceMomento deviceMomento);

        /// <summary>
        ///     Gets current deviceViewModel momento asynchronously
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        Task<IDeviceMomento> GetMomentoAsync();
    }
}