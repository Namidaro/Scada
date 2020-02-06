using System;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer
{
    /// <summary>
    /// Represents metadata that indicates whether the type can be binary serializable
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BinarySerializableAttribute : Attribute { }
}