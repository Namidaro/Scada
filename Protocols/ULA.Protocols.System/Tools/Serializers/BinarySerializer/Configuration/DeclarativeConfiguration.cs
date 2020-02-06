using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders.Declarative;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Configuration
{
    /// <summary>
    /// Represents declarative configuration for binary serialization
    /// </summary>
    public class DeclarativeConfiguration : BinarySerializationConfiguration
    {
        #region [Public members]
        /// <summary>
        /// Registrates a type for binary serialization
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type to registrate for binary serialization</typeparam>
        public void RegisterType<T>() where T : new()
        {
            var customTypeMetadataBuilder = new DeclarativeCustomTypeMetadataBuilder();
            customTypeMetadataBuilder.SetTypeToBuild(typeof(T));
            this.RegisterMetadata(customTypeMetadataBuilder);
        } 
        #endregion [Public members]
    }
}