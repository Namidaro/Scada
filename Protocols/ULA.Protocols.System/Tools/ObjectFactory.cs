using System;
using System.Reflection;

namespace YP.Toolkit.System.Tools
{
    /// <summary>
    /// Represents a factory fonctionality for an object
    /// </summary>
    public static class ObjectFactory
    {
        #region [Public members]
        /// <summary>
        /// Creates an instance of <see cref="T"/>.
        /// This method can create an object even if it has non-public constructor
        /// </summary>
        /// <typeparam name="T">The type of an object to create</typeparam>
        /// <param name="paramTypes">An instance of a collection that represents constructor parameter types</param>
        /// <param name="paramValues">An instance of a collection that represents constructors parameters</param>
        /// <returns>An instance of required object</returns>
        public static T CreateObject<T>(Type[] paramTypes, object[] paramValues)
        {
            var type = typeof(T);
            var constructorInfo = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null, paramTypes, null);
            return (T)constructorInfo.Invoke(paramValues);
        }
        /// <summary>
        /// Creates an instance of <see cref="T"/>.
        /// This method can create an object even if it has non-public constructor.
        /// This method creates an instance of the object that has default constructor.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>An instance of required object</returns>
        public static T CreateObject<T>()
        {
            var type = typeof(T);
            var constructor = type.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null, null, null);
            return (T)constructor.Invoke(null);
        }
        /// <summary>
        /// Creates an instance of <see cref="objectType"/>.
        /// This method can create an object even if it has non-public constructor.
        /// This method creates an instance of the object that has default constructor.
        /// </summary>
        /// <param name="objectType">The type of an object to create</param>
        /// <returns>An instance of required object</returns>
        public static object CreateObject(Type objectType)
        {
            var constructorInfo = objectType.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
                null, Type.EmptyTypes, null);
            return constructorInfo.Invoke(null);
        }
        #endregion [Public members]
    }
}