using System;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer
{
    /// <summary>
    /// Represents a collection information attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CollectionInfoAttribute : Attribute, ICollectionInfo
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets a type of a type information accessor 
        /// </summary>
        public Type ElementTypeInfoAccessor { get; set; } 
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="CollectionInfoAttribute"/>
        /// </summary>
        /// <param name="length">The length of a collection</param>
        public CollectionInfoAttribute(int length)
        {
            this.Length = length;
        } 
        #endregion [Ctor's]


        #region [ICollectionInfo members]
        /// <summary>
        /// Gets a length of a collection
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// Gets the information of member
        /// </summary>
        /// <param name="infoType">The type of information to get</param>
        /// <returns>Resulting information</returns>
        object ITypeInfo.GetInfo(Type infoType)
        {
            try
            {
                if (this.ElementTypeInfoAccessor == null) return null;
                var accessor = (ITypeInfo)Activator.CreateInstance(this.ElementTypeInfoAccessor);
                return accessor.GetInfo(infoType);
            }
            catch (Exception error)
            {
                Guard.Throw<MetadataBuilderException>("An unexpected error occured while retreaving type information accessor", error);
                return null;
            }
        } 
        #endregion [ICollectionInfo members]
    }
}
