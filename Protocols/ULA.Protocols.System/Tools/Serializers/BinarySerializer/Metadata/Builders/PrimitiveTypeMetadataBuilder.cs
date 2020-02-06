using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Represents a primitive type metadata builder
    /// </summary>
    public class PrimitiveTypeMetadataBuilder : IMetadataBuilder
    {
        #region [Constants]
        private const string NULL_TYPE_METADATABUILDER_MESSAGE =
            "Can't create instance of metadatabuilder with Null type information";
        private const string UNSUPPORTABLE_PRIMITIVE_TYPE_MESSAGE = "Unsupportable primitive types";
        #endregion [Constants]


        #region [Private members]
        private Type _valueType;
        private static readonly Dictionary<Type, Func<ISerializableTypeMetadata>> Map;
        #endregion [Private members]


        #region [Ctor's]
        static PrimitiveTypeMetadataBuilder()
        {
            Map = new Dictionary<Type, Func<ISerializableTypeMetadata>>
                {
                    { typeof(bool), () => new SerializableTypeMetadata<bool>(new BooleanSerializer(), Marshal.SizeOf(typeof(bool))) },
                    { typeof(sbyte), () => new SerializableTypeMetadata<sbyte>(new SByteSerializer(), Marshal.SizeOf(typeof(sbyte))) },
                    { typeof(byte), () => new SerializableTypeMetadata<byte>(new ByteSerializer(), Marshal.SizeOf(typeof(byte))) },
                    { typeof(ushort), () => new SerializableTypeMetadata<UInt16>(new UInt16Serializer(), Marshal.SizeOf(typeof(UInt16))) },
                    { typeof(short), () =>new SerializableTypeMetadata<Int16>(new Int16Serializer(), Marshal.SizeOf(typeof(Int16))) },
                    { typeof(uint), () => new SerializableTypeMetadata<UInt32>(new UInt32Serializer(), Marshal.SizeOf(typeof(UInt32))) },
                    { typeof(int), () => new SerializableTypeMetadata<Int32>(new Int32Serializer(), Marshal.SizeOf(typeof(Int32))) },
                    { typeof(ulong), () => new SerializableTypeMetadata<UInt64>(new UInt64Serializer(), Marshal.SizeOf(typeof(UInt64))) },
                    { typeof(long), () => new SerializableTypeMetadata<Int64>(new Int64Serializer(), Marshal.SizeOf(typeof(Int64))) }
                };
        }
        #endregion [Ctor's]


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
            Guard.AgainstNullReference<MetadataBuilderException>(type, "type", NULL_TYPE_METADATABUILDER_MESSAGE);

            this._valueType = type;
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
            /*none*/
        }
        /// <summary>
        /// Build metadata for type
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <returns>Build metadata</returns>
        public ISerializableTypeMetadata Build()
        {
            Func<ISerializableTypeMetadata> serializer;
            if (Map.TryGetValue(this._valueType, out serializer)) return serializer.Invoke();
            Guard.Throw<MetadataBuilderException>(UNSUPPORTABLE_PRIMITIVE_TYPE_MESSAGE, new NotSupportedException());
            return null;
        }
        /// <summary>
        /// Gets a type to build metadata for
        /// </summary>
        public Type TypeToBuild { get { return this._valueType; } }
        #endregion [IMetadataBuilder members]
    }
}
