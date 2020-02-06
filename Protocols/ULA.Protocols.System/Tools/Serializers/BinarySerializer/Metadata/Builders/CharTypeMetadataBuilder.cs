using System;
using System.Text;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Represents a <see cref="Char"/> metadata builder
    /// </summary>
    public class CharTypeMetadataBuilder : IMetadataBuilder
    {
        #region [Private fields]
        private Encoding _encoding;
        private int _capacity;
        private static readonly char[] CharValueAccessor = new[] { 'a' }; 
        #endregion [Private fields]


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
            this._encoding = ((ICharInfo)informationAccessor(typeof(ICharInfo))).Encoding;
            this._capacity = _encoding.GetByteCount(CharValueAccessor);
        }
        /// <summary>
        /// Builds metadata for a type
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <returns>Built metadata</returns>
        public ISerializableTypeMetadata Build()
        {
            return new SerializableTypeMetadata<char>(new CharSerializer(this._capacity, this._encoding), this._capacity);
        }
        /// <summary>
        /// Gets a type to build metadata for
        /// </summary>
        public Type TypeToBuild { get { return typeof(char); } } 
        #endregion [IMetadataBuilder members]
    }
}
