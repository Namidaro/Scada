using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Configuration;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;
using YP.Toolkit.System.Validation;
using CoreStream = System.IO.Stream;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer
{
    /// <summary>
    /// Represents entry point for binary serialization mechanism
    /// </summary>
    public class BinarySerialization
    {
        //TODO: [Yauheni Parshiniou] IMPLEMENT SINGLETON PATTERN HERE!!!

        #region [Constants]
        private const string NULL_CONFIGURATION_MESSAGE = "Can't registrate null configuration";
        private const string TYPE_CONFLICT_MESSAGE_PATTERN = "The type {0} has already been registered.";
        private const string NULL_TYPE_MESSAGE = "Can't get serializaer for null type";
        private const string NO_METADATA_FOR_TYPE_MESSAGE_PATTERN = "The type {0} is not configured. Can't get metadata for the type.";
        private const string NULL_OBJECT_SERIALIZATION_MESSAGE = "Can't serialize null object";
        private const string NULL_STREAM_MESSAGE = "Can't serialize object into null stream";
        #endregion [Constants]


        #region [Private fields]
        private static readonly Lazy<BinarySerialization> _instance = new Lazy<BinarySerialization>(() => new BinarySerialization());
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// Gets a collection of mapping information
        /// </summary>
        protected IDictionary<Type, ISerializableTypeMetadata> ConfigurationMapping { get; private set; }
        #endregion [Properties]


        #region [BinarySerialization members]

        /// <summary>
        /// Get an instance of <see cref="BinarySerialization"/>.
        /// </summary>
        public static BinarySerialization Instance
        {
            get { return _instance.Value; }
        }

        #endregion


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="BinarySerialization"/>
        /// </summary>
        /// <param name="mapping">Initial mapping collection</param>
        protected BinarySerialization(IDictionary<Type, ISerializableTypeMetadata> mapping)
        {
            this.ConfigurationMapping = mapping;
        }
        /// <summary>
        /// Creates an instance of <see cref="BinarySerialization"/>
        /// </summary>
        protected BinarySerialization()
            : this(new ConcurrentDictionary<Type, ISerializableTypeMetadata>())
        { }
        #endregion [Ctor's]


        #region [Public members]

        /// <summary>
        /// Configurates binary serialization mechanism
        /// 
        /// 
        /// <exception cref="MetadataRegistrationException"></exception>
        /// 
        /// </summary>
        /// <param name="configuration">The configuration to use</param>
        public void Configure([NotNull] BinarySerializationConfiguration configuration)
        {
            Guard.AgainstNullReference<MetadataRegistrationException>(configuration, "configuration", NULL_CONFIGURATION_MESSAGE);

            IDictionary<Type, ISerializableTypeMetadata> newMappingCollection = new ConcurrentDictionary<Type, ISerializableTypeMetadata>(this.ConfigurationMapping);
            foreach (var metadata in configuration.GetConfiguratedMetadata())
            {
                if (newMappingCollection.ContainsKey(metadata.Key))
                    Guard.Throw<MetadataRegistrationException>(string.Format(TYPE_CONFLICT_MESSAGE_PATTERN, metadata.Key));
                newMappingCollection.Add(metadata);
            }
            this.ConfigurationMapping = newMappingCollection;
        }

        /// <summary>
        /// Serializes a <see cref="value"/> into <see cref="stream"/>
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="value">The object to serialize</param>
        /// <param name="stream">The stream to serialize object into</param>
        public void Serialize([NotNull]object value, [NotNull] CoreStream stream)
        {
            Guard.AgainstNullReference<SerializationException>(value, "value", NULL_OBJECT_SERIALIZATION_MESSAGE);
            Guard.AgainstNullReference<SerializationException>(stream, "stream", NULL_STREAM_MESSAGE);

            var metadata = this.GetMetadataForType(value.GetType());
            var capacity = metadata.Capacity;
            var bufferValue = new BinaryBuffer(new byte[capacity]);
            var serializer = metadata.Serializer;
            serializer.Serialize(bufferValue, value);
            stream.Write(bufferValue.Data, 0, capacity);
        }
        /// <summary>
        /// Serializes a <see cref="value"/> into <see cref="stream"/>
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="value">The object to serialize</param>
        /// <param name="stream">The stream to serialize object into</param>
        public void Serialize<T>(T value, [NotNull] CoreStream stream) where T : new()
        {
            this.Serialize((object)value, stream);
        }
        /// <summary>
        /// Deserializes an object from a <see cref="stream"/>
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="objectType">The type of object to deserialize</param>
        /// <param name="stream">A stream to use to deserialize object from</param>
        /// <returns>The resulting instance of <see cref="objectType"/></returns>
        public object DeserializeObjectOfType([NotNull]Type objectType, [NotNull] CoreStream stream)
        {
            Guard.AgainstNullReference<SerializationException>(objectType, "objectType", "Can't de-serialize an object of null type");
            Guard.AgainstNullReference<SerializationException>(stream, "stream", "Can't de-serialize object from null stream");

            var metadata = this.GetMetadataForType(objectType);
            var serializer = metadata.Serializer;
            var capacity = metadata.Capacity;
            var data = new byte[capacity];
            stream.Read(data, 0, capacity);
            return serializer.Deserialize(new BinaryBuffer(data));
        }
        /// <summary>
        /// Deserializes an object from a <see cref="stream"/>
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="stream">A stream to use to deserialize object from</param>
        /// <returns>The resulting instance of <see cref="T"/></returns>
        public T Deserialize<T>([NotNull] CoreStream stream) where T : new()
        {
            return (T)this.DeserializeObjectOfType(typeof(T), stream);
        }
        #endregion [Public members]


        #region [Help members]
        ///// <summary>
        ///// Gets a serializer for a type
        ///// 
        ///// 
        ///// <exception cref="SerializationException"></exception>
        ///// 
        ///// </summary>
        ///// <param name="type">The type to get serializer for</param>
        ///// <returns>Found serializer</returns>
        //[NotNull]
        //protected internal ISerializer GetSerializerForType([NotNull] Type type)
        //{
        //    Guard.AgainstNullReference<SerializationException>(type, "type", NULL_TYPE_MESSAGE);

        //    if (!this.ConfigurationMapping.ContainsKey(type))
        //        Guard.Throw<SerializationException>(string.Format(NO_METADATA_FOR_TYPE_MESSAGE_PATTERN, type));

        //    return this.ConfigurationMapping[type].Serializer;
        //}

        /// <summary>
        /// Gets a metadata for type
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="type">The type to get metadata for</param>
        /// <returns>Found metadata</returns>
        [NotNull]
        protected internal ISerializableTypeMetadata GetMetadataForType([NotNull] Type type)
        {
            Guard.AgainstNullReference<SerializationException>(type, "type", NULL_TYPE_MESSAGE);

            if (!this.ConfigurationMapping.ContainsKey(type))
                Guard.Throw<SerializationException>(string.Format(NO_METADATA_FOR_TYPE_MESSAGE_PATTERN, type));

            return this.ConfigurationMapping[type];
        }
        
        ///// <summary>
        /////  Gets a serializer for a type
        ///// 
        ///// 
        ///// <exception cref="SerializationException"></exception>
        ///// 
        ///// </summary>
        ///// <typeparam name="T">The type to get serializer for</typeparam>
        ///// <returns>Found serializer</returns>
        //[NotNull]
        //protected internal ISerializer GetSerializaerFor<T>()
        //{
        //    return this.GetSerializerForType(typeof(T));
        //} 

        protected internal ISerializableTypeMetadata GetMetadataFor<T>()
        {
            return this.GetMetadataForType(typeof (T));
        }
        #endregion [Help members]
    }
}