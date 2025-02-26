using EHost.TcpServer.Model;
using System.Buffers.Binary;

namespace EHost.TcpServer.Service
{

    /// <summary>
    /// CRC 表的计算是基于 CCITT 16 位的 CRC 算法
    /// </summary>
    public class Crc16
    {
        private static readonly ushort[] CrcTable = CreateCrcTable();

        private static ushort[] CreateCrcTable()
        {
            ushort[] crcTable = new ushort[256];
            ushort crc;
            for (ushort i = 0; i < 256; i++)
            {
                crc = i;
                for (ushort j = 0; j < 8; j++)
                {
                    if ((crc & 1) != 0)
                    {
                        crc = (ushort)(crc >> 1 ^ 0x1021);
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
                crcTable[i] = crc;
            }
            return crcTable;
        }

        public static ushort Compute(byte[] buffer, int offset)
        {
            ushort crc = 0xFFFF;
            for (int i = offset; i < buffer.Length; i++)
            {
                crc = (ushort)(crc >> 8 ^ CrcTable[(crc ^ buffer[i]) & 0xFF]);
            }
            return crc;
        }

        public static byte[] ComputeBytes(byte[] buffer, int offset)
        {
            ushort crc = 0xFFFF;
            for (int i = offset; i < buffer.Length; i++)
            {
                crc = (ushort)(crc >> 8 ^ CrcTable[(crc ^ buffer[i]) & 0xFF]);
            }

            // 将crc结果转换为字节数组
            byte[] crcBytes = new byte[2];
            crcBytes[0] = (byte)(crc & 0xFF); // 低8位
            crcBytes[1] = (byte)(crc >> 8 & 0xFF); // 高8位
            return crcBytes;
        }
    }
    public class Protocol
    {
        public const int ProtocolLengthMin = 10; // 最小协议长度

        /// <summary>
        /// 用于byte [] 解析标准协议
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataStructure"></param>
        /// <returns></returns>
        public static bool ProtocolExtract(byte[] data, out ProtocolData dataStructure)
        {
            dataStructure = new ProtocolData();
            dataStructure.IsValid = false;

            if (data == null || data.Length < ProtocolLengthMin)
            {
                dataStructure.ErrorMessage = "数据不足";
                return false;
            }

            dataStructure.Head = data[0];
            dataStructure.CommandType = data[1];
            dataStructure.CommandSymbol = data[2];
            dataStructure.CommandControl = data[3];
            dataStructure.CommandRequest = data.AsSpan(4, 2).ToArray();
            dataStructure.PayloadLength = BinaryPrimitives.ReadUInt16BigEndian(data.AsSpan(6, 2));

            if (data.Length < dataStructure.PayloadLength + ProtocolLengthMin)
            {
                dataStructure.ErrorMessage = "数据长度错误";
                return false;
            }

            if (dataStructure.PayloadLength == 0)
            {
                dataStructure.PayloadData = Array.Empty<byte>();
            }
            else
            {
                dataStructure.PayloadData = new byte[dataStructure.PayloadLength];
                Buffer.BlockCopy(data, 8, dataStructure.PayloadData, 0, dataStructure.PayloadLength);
            }

            dataStructure.Crc = BinaryPrimitives.ReadUInt16BigEndian(data.AsSpan(8 + dataStructure.PayloadLength, 2));
            dataStructure.Tail = data[10 + dataStructure.PayloadLength];

            if (dataStructure.Head != 0xAC)
            {
                dataStructure.ErrorMessage = "头部字节错误";
                return false;
            }
            else if (dataStructure.Tail != 0xB1)
            {
                dataStructure.ErrorMessage = "尾部字节错误";
                return false;
            }
            else
            {
                var crcValue = Crc16.ComputeBytes(data, 8 + dataStructure.PayloadLength);
                //if (crcValue == dataStructure.Crc)
                //{
                //    dataStructure.IsValid = true;
                //    return true;
                //}
                //else
                //{
                //    dataStructure.ErrorMessage = "CRC 错误";
                //    return false;
                //}
                return true;

            }
        }

        public static bool ProtocolExtract(string packet, out BasicProtocolData dataStructure)
        {
            var dataLength = int.Parse(packet.Substring(2, 4));
            var data = packet.Substring(6, dataLength);
            dataStructure = new BasicProtocolData()
            {
                Header = packet.Substring(0, 2),
                DataLength = dataLength,
                DataSegment = data,
                CRC = packet.Substring(6 + dataLength, 4),
                //Trailer = data.Substring(data.Length - 2, 2),
            };
            // Parse the packet based on the given structure
            if (dataStructure.DataSegment.EndsWith("&&"))
            {
                return true;
            }
            return false;
        }

    }
}
