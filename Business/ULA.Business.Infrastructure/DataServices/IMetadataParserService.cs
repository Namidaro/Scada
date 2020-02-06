using System;
using System.Collections.Generic;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Metadata;

namespace ULA.Business.Infrastructure.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMetadataParserService:IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        List<MetadataFromDevice> GetPartlyUpdateMetadata(IRuntimeDevice runtimeDevice);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        List<MetadataFromDevice> GetFullUpdateMetadata(IRuntimeDevice runtimeDevice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        List<MetadataFromDevice> GetSignalUpdateMetadata(IRuntimeDevice runtimeDevice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        List<MetadataFromDevice> GetDeviceSignatureMetadata(IRuntimeDevice runtimeDevice);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        List<MetadataFromDevice> GetAnalogsMetadata(IRuntimeDevice runtimeDevice);


    }

}