namespace YP.Toolkit.Protocols.ModBus.Resources
{
    /// <summary>
    /// Represents constants related to the modbus protocol
    /// </summary>
    internal static class ProtocolConstants
    {
        #region [Supported function codes]
        /// <summary>
        /// Modbus function number that represents a function for reading coil status
        /// </summary>
        public const byte READ_COILS = 1;
        /// <summary>
        /// Modbus function number that represents a function for reading discrete inputs
        /// </summary>
        public const byte READ_INPUTS = 2;
        /// <summary>
        /// Modbus function number that represents a function for reading holding registers
        /// </summary>
        public const byte READ_HOLDING_REGISTERS = 3;
        /// <summary>
        /// Modbus function number that represents a function for reading input registers
        /// </summary>
        public const byte READ_INPUT_REGISTERS = 4;
        /// <summary>
        /// Modbus function number that represents a function for writing single coil
        /// </summary>
        public const byte WRITE_SINGLE_COIL = 5;
        /// <summary>
        /// Modbus function number that represents a function for writing single register
        /// </summary>
        public const byte WRITE_SINGLE_REGISTER = 6;
        /// <summary>
        /// Modbus function number that represents a function for writing multiple coils
        /// </summary>
        public const byte WRITE_MULTIPLE_COILS = 15;
        /// <summary>
        /// Modbus function number that represents a function for writing multiple registers 
        /// </summary>
        public const byte WRITE_MULTIPLE_REGISTERS = 16;
        #endregion [Supported function codes]


        #region [Request/Response limitations]
        /// <summary>
        /// The number of maximum discrete request/response size
        /// </summary>
        public const int MAXIMUM_DISCRETE_REQUEST_RESPONSE_SIZE = 2000;
        /// <summary>
        /// The number of maximum registers request/response size
        /// </summary>
        public const int MAXIMUM_REGISTER_REQUEST_RESPONSE_SIZE = 125;
        /// <summary>
        /// The maximum data length of registers request/response size
        /// </summary>
        public const int MAXIMUM_WRITE_REGISTER_REQUEST_RESPONSE_DATA_SIZE = 123;
        /// <summary>
        ///  The maximum data length of discrete request/response size
        /// </summary>
        public const int MAXIMUM_WRITE_DISCRETE_REQUEST_RESPONSE_DATA_SIZE = 1968;
        #endregion [Request/Response limitations]


        #region [Error codes]
        /// <summary>
        /// The slave device does not support the requested operation 
        /// </summary>
        public const byte SLAVE_FUNCTION_SUPPORT_EXCEPTION = 1;
        /// <summary>
        /// Nonexistent slave device registers requested
        /// </summary>
        public const byte NONEXISTENT_REGISTERS_EXCEPTION = 2;
        /// <summary>
        /// Invalid data value requested
        /// </summary>
        public const byte INVALID_DATA_VALUE_EXCEPTION = 3;
        /// <summary>
        /// Slave has accepted long-duration program command
        /// </summary>
        public const byte LONG_DURATION_COMMAND_EXCEPTION = 5;
        /// <summary>
        /// Function can't be performed now: a long-duration command in effect
        /// </summary>
        public const byte LONG_DURATION_COMMAND_IN_EFFECT_EXCEPTION = 6;
        /// <summary>
        /// Slave rejected long-duration program command 
        /// </summary>
        public const byte LONG_DURATION_COMMAND_REJECTION_EXCEPTION = 7;
        #endregion [Error codes]


        #region [Default IO operation fields]
        /// <summary>
        /// The default number of retries for IO operations
        /// </summary>
        public const int DEFAULT_RETRIES = 3;
        /// <summary>
        /// The default number of milliseconds to wait after long-duration operation command exception response 
        /// </summary>
        public const int DEFAULT_WAIT_TO_RETRY_MILLISECONDS = 250;
        /// <summary>
        /// The default setting for IO timeouts in milliseconds 
        /// </summary>
        public const int DEFAULT_TIMEOUT = 1000;
        #endregion [Default IO operation fields]


        /// <summary>
        /// The smallest supported message frame size (sans checksum). 
        /// </summary>
        public const int MINIMUM_FRAME_SIZE = 2;
        /// <summary>
        /// The mask for turn ON coil.
        /// </summary>
        public const ushort COIL_ON = 0xFF00;
        /// <summary>
        /// The mask for turn ON coil.
        /// </summary>
        public const byte COIL_ON_RAW = 0xff;
        /// <summary>
        /// The mask for turn OFF coil.
        /// </summary>
        public const ushort COIL_OFF = 0x0000;
        /// <summary>
        /// The mask for turn OFF coil.
        /// </summary>
        public const ushort COIL_OFF_RAW = 0x0;
        /// <summary>
        /// The end of message identifier that is used by the ASCII transport.
        /// </summary>
        public const string ASCII_NEW_LINE = "\r\n";
        /// <summary>
        /// The exception code offset.
        /// </summary>
        public const byte EXCEPTION_OFFSET = 128;
        /// <summary>
        /// Number of bytes in word
        /// </summary>
        public const int BYTES_IN_WORD = 2;

        #region [TCP Modbus members]
        /// <summary>
        /// Represents a MBAP header length.
        /// </summary>
        public const int MBAP_HEADER_LENGTH = 6;
        /// <summary>
        /// Represents the bytes offset for package length in MBAP header.
        /// </summary>
        public const int LENGTH_OFFSET_IN_MBAP_HEADER = 4;
        /// <summary>
        /// Represents the bytes offset for transaction id in MBAP header.
        /// </summary>
        public const int TRANSACTIONID_OFFSET_IN_MBAP_HEADER = 0;
        /// <summary>
        /// Represents the default slave address for tcp modbus.
        /// </summary>
        public const byte DEFAULT_IP_SLAVE_UNIT_ID = 0;
        #endregion [TCP Modbus members]


        /// <summary>
        /// Represents ASCII frame separator.
        /// </summary>
        public const char ASCII_FRAME_SEPARATOR = ':';
    }
}