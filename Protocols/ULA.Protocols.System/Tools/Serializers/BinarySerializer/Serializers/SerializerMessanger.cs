namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    /// <summary>
    /// Represents serializer messsanger
    /// </summary>
    internal static class SerializerMessanger
    {
        /// <summary>
        /// Null writer detected message
        /// </summary>
        public const string NULL_WRITER_MESSAGE = "Can't serialize with Null writer";
        /// <summary>
        /// Null reader detected message
        /// </summary>
        public const string NULL_READER_MESSAGE = "Can't deserialize with Null reader";
        /// <summary>
        /// Null object while serialization detected message
        /// </summary>
        public const string NULL_OBJECT_TO_SERIALIZE_MESSAGE = "Can't serialize null object";
        /// <summary>
        /// Unexpected error while serialization message
        /// </summary>
        public const string UNEXPECTED_SERIALIZATION_EXCEPTION_MESSAGE = "While serialization an error occured";
        /// <summary>
        /// Unexpected error while deserialization message
        /// </summary>
        public const string UNEXPECTED_DESERIALIZATION_EXCEPTION_MESSAGE = "While deserialization an error occured";
    }
}