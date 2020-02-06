namespace ULA.Business.Infrastructure.DTOs
{
    /// <summary>
    ///     Represents logical driver creation information
    /// </summary>
    public class LogicalDriverInformation
    {
        /// <summary>
        ///     Gets or sets the type name of the driver
        /// </summary>
        public string DriverType { get; set; }

        /// <summary>
        ///     Gets or sets the tcp port number of the driver
        /// </summary>
        public int DriverTcpPort { get; set; }

        /// <summary>
        ///     Gets or sets the tcp address of the driver
        /// </summary>
        public string DriverTcpAddress { get; set; }
    }
}