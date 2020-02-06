using System;
using System.Text;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer
{
    /// <summary>
    /// Represents a string information attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class StringInfoAttribute : Attribute, IStringInfo
    {
        #region [Constants]
        private const string NON_ENCODING = "No Encoding in this version"; 
        #endregion [Constants]


        #region [Properties]
        /// <summary>
        /// Gets a type of a string encoding
        /// </summary>
        public Type EncodingType { get; set; } 
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="StringInfoAttribute"/>
        /// </summary>
        /// <param name="length">The length of a string</param>
        public StringInfoAttribute(int length)
        {
            this.Length = length;
        } 
        #endregion [Ctor's]


        #region [IStringInfo members]
        /// <summary>
        /// Gets a length of a string
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Gets an encoding of a string
        /// </summary>
        Encoding IStringInfo.Encoding
        {
            get
            {
                if (this.EncodingType != null)
                {
                    //return new ASCIIEncoding();
                    throw new Exception(NON_ENCODING);
                }
                //return (Encoding)Activator.CreateInstance(this.EncodingType);
                return null;
            }
        } 
        #endregion [IStringInfo members]
    }
}