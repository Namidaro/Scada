using System;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Exposes member information basic functionality
    /// </summary>
    /// <typeparam name="TValue">A type of value</typeparam>
    internal interface IMemberInfo<TValue> : IMemberInfo
    {
        /// <summary>
        /// Gets the value of a member
        /// 
        /// 
        /// <exception cref="ArgumentNullException"></exception>
        /// 
        /// </summary>
        /// <param name="valueAccessor">The accessor to get value from</param>
        /// <returns>The value accepted from the value accessor</returns>
        new TValue GetValue(object valueAccessor);
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
        void SetValue(object valueAccessor, TValue value);
    }
}