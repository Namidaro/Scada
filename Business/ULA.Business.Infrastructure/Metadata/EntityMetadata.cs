using System;

namespace ULA.Business.Infrastructure.Metadata
{
    /// <summary>
    ///     Represent a metadata for work with deviceViewModel
    /// </summary>
    public class EntityMetadata:IEquatable<EntityMetadata>
    {
        #region [Public fields]
        /// <summary>
        ///     Represent a number of points for read from deviceViewModel
        /// </summary>
        public ushort NumberOfPoints { get; set; }
        /// <summary>
        ///     Represent a listening address
        /// </summary>
        public ushort StartAddress { get; set; }
        #endregion [Public fields]

        #region Equality members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(EntityMetadata other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return NumberOfPoints == other.NumberOfPoints && StartAddress == other.StartAddress;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EntityMetadata) obj);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (NumberOfPoints.GetHashCode() * 397) ^ StartAddress.GetHashCode();
            }
        }

        #endregion
    }
}
