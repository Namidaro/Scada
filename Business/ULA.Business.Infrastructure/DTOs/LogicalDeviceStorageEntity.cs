using System;

namespace ULA.Business.Infrastructure.DTOs
{
    /// <summary>
    ///     Represents deviceViewModel storage entity
    /// </summary>
    public class LogicalDeviceStorageEntity
    {
        /// <summary>
        ///     Gets or sets the unique identifier of current entity
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name of current entity
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description of current entity
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the name of the driver that current deviceViewModel suppose to use
        /// </summary>
        public string DriverTypeName { get; set; }
    }
}