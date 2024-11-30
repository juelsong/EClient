namespace EHost.TcpServer.Model
{
    public class BasicProtocolData
    {
        //固定包头 长度2 固定为## 
        public string Header { get; set; }
        //数据段长度  长度4 十进制整数 
        public int DataLength { get; set; }
        /// <summary>
        /// 数据段 
        /// </summary>
        public string DataSegment { get; set; }
        /// <summary>
        /// CRC校验  十六进制整数
        /// </summary>
        public string CRC { get; set; }
        /// <summary>
        /// 包尾 固定为 CRLF
        /// </summary>
        public string Trailer { get; set; }

    }
}
