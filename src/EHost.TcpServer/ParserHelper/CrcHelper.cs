using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHost.TcpServer.ParserHelper
{
    public class CrcHelper
    {
        public static ushort CRC16_Checkout(byte[] puchMsg, int usDataLen)
        {
            ushort crc_reg = 0xFFFF;
            for (int i = 0; i < usDataLen; i++)
            {
                crc_reg = (ushort)((crc_reg >> 8) ^ puchMsg[i]);
                for (int j = 0; j < 8; j++)
                {
                    if ((crc_reg & 0x0001) != 0)
                    {
                        crc_reg = (ushort)((crc_reg >> 1) ^ 0xA001);
                    }
                    else
                    {
                        crc_reg >>= 1;
                    }
                }
            }
            return crc_reg;
        }
         
    }
}
