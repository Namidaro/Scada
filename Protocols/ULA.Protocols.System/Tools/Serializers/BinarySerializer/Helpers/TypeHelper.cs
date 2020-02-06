using System;
using System.Collections;
using System.Linq;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Helpers
{
    /// <summary>
    /// Represents type helper
    /// </summary>
    internal static class TypeHelper
    {
        #region [Public members]
        /// <summary>
        /// Gets a value that indicates whether a type is primitive
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>The value that indicates whether a type is primitive</returns>
        public static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type.Equals(typeof(string));
        }
        /// <summary>
        /// Gets a value that indicates whether a type is inherited from <see cref="Enumerable"/>
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>The value that indicates whether a type is inherited from <see cref="Enumerable"/></returns>
        public static bool IsEnumerable(Type type)
        {
            return type.IsArray || type.GetInterfaces().Any(a => a.Equals(typeof(IEnumerable)));
        }
        /// <summary>
        /// Gets a value that indicates whether a type has attribute of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">The type of attribute to check</typeparam>
        /// <param name="type">The type to check</param>
        /// <returns>The value that indicates whether a type has attribute of type <see cref="T"/></returns>
        public static bool HasAttributeOf<T>(Type type) where T : Attribute
        {
            return Attribute.IsDefined(type, typeof(T));
        } 
        #endregion [Public members]
    }
}