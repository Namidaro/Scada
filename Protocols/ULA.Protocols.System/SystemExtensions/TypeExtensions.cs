using System;

namespace YP.Toolkit.System.SystemExtensions
{
    /// <summary>
    /// 	Extension methods for the reflection meta data type "Type"
    /// </summary>
    public static class TypeExtensions
    {
        ///<summary>
        ///	Check if this is a base type
        ///</summary>
        ///<param name = "type"></param>
        ///<param name = "checkingType"></param>
        ///<returns></returns>
        public static bool IsBaseType(this Type type, Type checkingType)
        {
            while (type != typeof(object))
            {
                if (type == null)
                    continue;

                if (type == checkingType)
                    return true;

                type = type.BaseType;
            }
            return false;
        }

        ///<summary>
        ///	Check if this is a sub class generic type
        ///</summary>
        ///<param name = "generic"></param>
        ///<param name = "toCheck"></param>
        ///<returns></returns>
        public static bool IsSubclassOfRawGeneric(this Type generic, Type toCheck)
        {
            while (toCheck != typeof(object))
            {
                if (toCheck == null)
                    continue;

                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                    return true;
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        /// <summary>
        /// Closes the passed generic type with the provided type arguments and returns an instance of the newly constructed type.
        /// </summary>
        /// <typeparam name="T">The typed type to be returned.</typeparam>
        /// <param name="genericType">The open generic type.</param>
        /// <param name="typeArguments">The type arguments to close the generic type.</param>
        /// <returns>An instance of the constructed type casted to T.</returns>
        public static T CreateGenericTypeInstance<T>(this Type genericType, params Type[] typeArguments) where T : class
        {
            var constructedType = genericType.MakeGenericType(typeArguments);
            var instance = Activator.CreateInstance(constructedType);
            return (instance as T);
        }

        #region [Public members]
        /// <summary>
        /// Gets a qualified name of a type without version and assembly token information
        /// </summary>
        /// <param name="type">An instance of type to obtain information from</param>
        /// <returns>A qualified name of a type without version and assembly token information</returns>
        public static string GetQualifiedNameWithoutVersionAndTokenInfo(this Type type)
        {
            return string.Format("{0}, {1}", type.FullName, type.Assembly.GetName().Name);
        }
        #endregion [Public members]
    }
}
