using System;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Exposes member information basic functionality
    /// </summary>
    public interface IMemberInfo : ITypeInfo
    {
        /// <summary>
        /// Gets the type of a member
        /// </summary>
        /// <returns>The type of member</returns>
        Type GetMemberType();
        /// <summary>
        /// Gets the value of a member
        /// 
        /// 
        /// <exception cref="ArgumentNullException"></exception>
        /// 
        /// </summary>
        /// <param name="valueAccessor">The accessor to get value from</param>
        /// <returns>The value accepted from the value accessor</returns>
        object GetValue(object valueAccessor);
        /// <summary>
        /// Sets the value to an accessor
        /// 
        /// 
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// 
        /// </summary>
        /// <param name="valueAccessor">The accessor to set value to</param>
        /// <param name="value">The value to set to accessor</param>
        void SetValue(object valueAccessor, object value);
        /// <summary>
        /// Gets the information of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">Type of information to get</typeparam>
        /// <returns>Resulting information</returns>
        T GetInfo<T>() where T : class;
    }
}