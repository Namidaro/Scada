using System;
using System.Text;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer
{
    /// <summary>
    /// Represents a character information attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CharInfoAttribute : Attribute, ICharInfo
    {
        #region [Properties]
        /// <summary>
        /// Gets a type of character encoding
        /// </summary>
        public Type EncodingType { get; private set; } 
        #endregion [Properties]


        #region [ICharInfo members]
        /// <summary>
        /// An encoding of a character
        /// </summary>
        Encoding ICharInfo.Encoding
        {
            get
            {
                if (this.EncodingType == null)
                {
                    return new ASCIIEncoding();
                }
                return (Encoding)Activator.CreateInstance(this.EncodingType);
            }
        } 
        #endregion [ICharInfo members]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="CharInfoAttribute"/>
        /// </summary>
        /// <param name="encodingType">A type of a character encosing</param>
        public CharInfoAttribute(Type encodingType)
        {
            this.EncodingType = encodingType;
        } 
        #endregion [Ctor's]
    }
}