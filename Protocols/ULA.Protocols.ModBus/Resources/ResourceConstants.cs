namespace YP.Toolkit.Protocols.ModBus.Resources
{
    internal static class ResourceConstants
    {
        public const string TCP_MODBUS_READ_RESULT_WITH_ZERO_BYTES_MESSAGE = "Read resulted in 0 bytes returned.";

        public const string ARGUMENT_READHOLD_INGINPUT_REGISTERS_RESPONSE_MESSAGE = "Argument response should be of type ReadHoldingInputRegistersResponse.";
        public const string READ_COILS_INPUTS_UNEXPECTED_BYTES_COUNT = "Unexpected byte count.";
        public const string READ_COILS_INPUTS_INITIALIZE_ERROR_MESSAGE = "Message frame data segment does not contain enough bytes.";
        public const string NOT_EQUALS_SLAVEADDRESSES_MESSAGE = "Received response with unexpected Function Code.";
        public const string NOT_EQUALS_FUNCTIONCODES_MESSAGE = "Response slave address does not match request.";

        public const string ILLEGAL_FUNCTION_MESSAGE = "The function code received in the query is not an allowable action for the server (or slave). This may be because the function code is only applicable to newer devices, and was not implemented in the unit selected. It could also indicate that the server (or slave) is in the wrong state to process a request of this type, for example because it is un-configured and is being asked to return register values.";
        public const string ILLEGAL_DATA_ADDRESS_MESSAGE = "The data address received in the query is not an allowable address for the server (or slave).";
        public const string ILLEGAL_DATA_VALUE_MESSAGE = "A value contained in the query data field is not an allowable value for server (or slave). This indicates a fault in the structure of the remainder of a complex request, such as that the implied length is incorrect. It specifically does NOT mean that a data item submitted for storage in a register has a value outside the expectation of the application program, since the MODBUS protocol is unaware of the significance of any particular value of any particular register.";
        public const string SLAVE_DEVICE_FAILURE_MESSAGE = "An unrecoverable error occurred while the server (or slave) was attempting to perform the requested action.";
        public const string ACKNOWLEGE_MESSAGE = "Specialized use in conjunction with programming commands. The server (or slave) has accepted the request and is processing it, but a long duration of time will be required to do so. This response is returned to prevent a timeout error from occurring in the client (or master). The client (or master) can next issue a Poll Program Complete message to determine if processing is completed.";
        public const string SLAVE_DEVICE_BUSY_MESSAGE = "Specialized use in conjunction with programming commands. The server (or slave) is engaged in processing a long–duration program command. The client (or master) should retransmit the message later when the server (or slave) is free.";
        public const string MEMORY_PARITY_ERROR_MESSAGE = "Specialized use in conjunction with function codes 20 and 21 and reference type 6, to indicate that the extended file area failed to pass a consistency check.";
        public const string GATEWAY_PATH_UNAVAILABLE_MESSAGE = "Specialized use in conjunction with gateways, indicates that the gateway was unable to allocate an internal communication path from the input port to the output port for processing the request. Usually means that the gateway is mis-configured or overloaded.";
        public const string GATEWAY_TARGET_DEVICE_FAILED_TO_RESPOND_MESSAGE = "Specialized use in conjunction with gateways, indicates that no response was obtained from the target device. Usually means that the device is not present on the network.";
        public const string UNKNOWN_EXCEPTION_CODE_DESCRIPTION = "Unknown error.";
        public const string TRANSACTION_ID_ERROR_MESSAGE = "Response was not of expected transaction ID";
        public const string CHECKSUM_NOT_MATCH_MESSAGE = "Checksums failed to match";
        public const string ASCII_MESSAGE_ERROR_MESSAGE = "Premature end of stream, message truncated.";
        public const string READ_EIGHT_BITS_RESPONSE_ERROR_MESSAGE = "Message frame does not contain enough bytes.";
    }
}