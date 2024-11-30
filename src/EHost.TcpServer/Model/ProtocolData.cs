namespace EHost.TcpServer.Model
{
    /// <summary>
    /// 用于标准协议
    /// </summary>
    public class ProtocolData
    {
        /// <summary>
        /// 帧头，用于标识数据帧的开始，固定为0xAC
        /// </summary>
        public byte Head { get; set; }

        /// <summary>
        /// 指令类型
        ///0xF0	后台指令
        ///0xF1	系统指令
        ///0xF2	配置指令
        ///0xF3	数据交互指令
        ///0xF4	测试指令
        ///0xD0	预留（内部协议）
        ///0xD1	系统指令（内部协议）
        ///0xD2	配置指令（内部协议）
        ///0xD3	数据交互指令（内部协议）
        ///其他 预留
        /// </summary>
        public byte CommandType { get; set; }

        /// <summary>
        /// 指令符
        /// </summary>
        public byte CommandSymbol { get; set; }

        /// <summary>
        /// 操作码
        /// </summary>
        public byte CommandControl { get; set; }

        /// <summary>
        /// 请求码，长度为2字节
        /// </summary>
        public byte[] CommandRequest { get; set; }

        /// <summary>
        /// 负载长度，表示负载数据的长度，长度为2字节，以大端字节序表示
        /// </summary>
        public ushort PayloadLength { get; set; }

        /// <summary>
        /// 负载数据，包含实际的数据内容，长度为PayloadLength字节
        /// </summary>
        public byte[] PayloadData { get; set; }

        /// <summary>
        /// CRC校验值，用于检测数据传输过程中是否发生错误，长度为2字节，以大端字节序表示 CRC16-CCITT
        /// </summary>
        public ushort Crc { get; set; }

        /// <summary>
        /// 帧尾，用于标识数据帧的结束，固定为0xB1
        /// </summary>
        public byte Tail { get; set; }

        /// <summary>
        /// 数据是否有效，如果有效，表示数据帧格式正确，CRC校验通过
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 错误消息，如果数据无效，存储错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }


}
