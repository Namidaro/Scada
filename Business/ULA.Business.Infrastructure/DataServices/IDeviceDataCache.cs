using ULA.Business.Infrastructure.Metadata;

namespace ULA.Business.Infrastructure.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceDataCache
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityMetadata"></param>
        /// <returns></returns>
        byte[] GetBytesFromMetadata(EntityMetadata entityMetadata);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityMetadata"></param>
        /// <param name="bytes"></param>
        void SaveBytesByMetadata(EntityMetadata entityMetadata, byte[] bytes);


    }

}