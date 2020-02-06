using System;
using System.Text;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Represents a <see cref="String"/> type metadata builder
    /// </summary>
    public class StringTypeMetadataBuilder : IMetadataBuilder
    {
        #region [Constants]
        private const string BAD_INFO_MESSAGE = "Can't create instance of metadatabuilder with Null information"; 
        #endregion [Constants]


        #region [Private members]
        private Encoding _encoding;
        private int _capacity;
        #endregion [Private members]


        #region [IMetadataBuilder members]
        /// <summary>
        /// Sets a type to build metadata for
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="type">A type to build metadata for</param>
        public void SetTypeToBuild(Type type)
        {
            /*none*/
        }
        /// <summary>
        /// Gets a type information accessor
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="informationAccessor">The instance of information  accessor</param>
        public void SetInfoAccessor(Func<Type, object> informationAccessor)
        {
            Guard.AgainstNullReference<MetadataBuilderException>(informationAccessor, "info", BAD_INFO_MESSAGE);
            var info = (IStringInfo) informationAccessor(typeof (IStringInfo));
            Guard.AgainstNullReference<MetadataBuilderException>(info, BAD_INFO_MESSAGE);
            this._encoding = info.Encoding;
            this._capacity = info.Length;
        }
        /// <summary>
        /// Build metadata for type
        /// </summary>
        /// <returns>Build metadata</returns>
        public ISerializableTypeMetadata Build()
        {
            return new SerializableTypeMetadata(new StringSerializer(this._capacity, this._encoding), typeof (string), this._capacity);
        }
        /// <summary>
        /// Gets a type to build metadata for
        /// </summary>
        public Type TypeToBuild { get { return typeof (string); } }
        #endregion [IMetadataBuilder members]
    }
}
