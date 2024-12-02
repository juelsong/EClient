using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using EHost.Contract.Entity;
using log4net;

namespace EHost.TcpServer.ParserHelper
{
    /// <summary>
    /// 当前文档描述高字节在前 为大端模式
    /// </summary>
    public static class DataParser
    {
        private readonly static ILog logger = LogManager.GetLogger(nameof(DataParser));

        private static int ConvertByteArrayToId(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 4)
            {
                throw new ArgumentException("Data array must be at least 4 bytes long.");
            }
            if (BitConverter.IsLittleEndian) // 若为 小端模式
            {
                Array.Reverse(bytes);
            }
            int intValue = BitConverter.ToInt32(bytes, 0);

            return intValue;
        }
        private static ushort ConvertByteArrayToShortData(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 2)
            {
                throw new ArgumentException("Data array must be at least 4 bytes long.");
            }
            if (BitConverter.IsLittleEndian) // 若为 小端模式
            {
                Array.Reverse(bytes);
            }
            //int intValue = BitConverter.ToInt32(bytes, 0);

            return BitConverter.ToUInt16(bytes, 0); ;
        }
        private static DateTime ParseDateTimeFromBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length != 8)
            {
                throw new ArgumentException("Invalid byte array length.");
            }
            //if (BitConverter.IsLittleEndian) // 若为 小端模式
            //{
            //    Array.Reverse(bytes);
            //}
            // 读取年
            int year = bytes[0];
            // 读取月
            int month = bytes[1];
            // 读取日
            int day = bytes[2];
            // 读取时
            int hour = bytes[3];
            // 读取分
            int minute = bytes[4];
            // 读取秒
            int second = bytes[5];
            int millisecond = bytes[6];

            return new DateTime(
                    year + 2000, month, day, hour, minute, second, millisecond
                 );
        }
        /// <summary>
        /// 从源数组获取指定长度结果
        /// </summary>
        /// <param name="originalArray"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static byte[] GetTargetBytes(byte[] originalArray, int startIndex, int length)
        {
            byte[] newArray = new byte[length]; // 创建一个新数组来存储复制的内容
            Array.Copy(originalArray, startIndex, newArray, 0, length); // 从原始数组复制数据到新数组
            return newArray;
        }


        /// <summary>
        /// 解析byte[] 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static EnvironmentalSensor EnvironmentalSensorParse(this byte[] data)
        {
            if (data == null || data.Length < 32) // 根据需要的字节长度调整这个值
            {
                throw new ArgumentException("Data array is null or not long enough to parse environmental sensor data.");
            }

            EnvironmentalSensor sensorData = new EnvironmentalSensor();

            sensorData.DeviceIdNet = ConvertByteArrayToId(GetTargetBytes(data, 0, 4)).ToString();
            sensorData.DeviceIdNode = ConvertByteArrayToId(GetTargetBytes(data, 4, 4)).ToString();

            // 解析年月日时分秒毫秒
            sensorData.UpdateDate = ParseDateTimeFromBytes(GetTargetBytes(data, 8, 8));

            // 解析PM2.5, PM10, CPM, Noise, WindSpeed, Temperature, Humidity, VOC, AtmosPressure
            using MemoryStream stream = new MemoryStream(GetTargetBytes(data, 32, data.Length - 32));
            byte[] buffer = new byte[10];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                // 获取数据因子类型
                byte dataType = buffer[0];
                // 获取数据长度
                byte dataLength = buffer[1];

                switch (dataType)
                {
                    case 0x01: // PM2.5
                        sensorData.PM2_5Max = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.PM2_5Average = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.PM2_5Min = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.PM2_5Current = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x02: // PM10
                        sensorData.PM10Max = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.PM10Average = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.PM10Min = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.PM10Current = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x03: // CPM
                        sensorData.CPMMax = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.CPMAverage = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.CPMMin = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.CPMCurrent = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x04: // 噪音
                        sensorData.NoiseMax = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.NoiseAverage = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.NoiseMin = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.NoiseCurrent = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x05: // 风向
                        sensorData.WindDirectionMax = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.WindDirectionAverage = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.WindDirectionMin = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.WindDirectionCurrent = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x06: // 风速
                        sensorData.WindSpeedMax = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.WindSpeedAverage = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.WindSpeedMin = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.WindSpeedCurrent = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x07: // 温度
                        sensorData.TemperatureMax = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.TemperatureAverage = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.TemperatureMin = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.TemperatureCurrent = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x08: // 湿度
                        sensorData.HumidityMax = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.HumidityAverage = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.HumidityMin = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.HumidityCurrent = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x09: // VOC
                        sensorData.VOCMax = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.VOCAverage = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.VOCMin = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.VOCCurrent = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x0A: // 电源输入
                        sensorData.IsCalibratingCurrent = (buffer[2] & 0x80) != 0;
                        sensorData.IsPowerOnCurrent = (buffer[2] & 0x40) != 0;
                        sensorData.RTCNeedsTimeSyncCurrent = (buffer[2] & 0x20) != 0;
                        sensorData.Reserved = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    case 0x11: // 大气压
                        sensorData.AtmosPressureMax = ConvertByteArrayToShortData([buffer[2], buffer[3]]);
                        sensorData.AtmosPressureAverage = ConvertByteArrayToShortData([buffer[4], buffer[5]]);
                        sensorData.AtmosPressureMin = ConvertByteArrayToShortData([buffer[6], buffer[7]]);
                        sensorData.AtmosPressureCurrent = ConvertByteArrayToShortData([buffer[8], buffer[9]]);
                        break;
                    default:
                        //ErrorMessage = "未知数据类型";
                        break;
                }

            }
            return sensorData;
        }
        public static FugitiveDust FugitiveDustParse(this string data)
        {
            FugitiveDust result = new FugitiveDust();
            result.OriginData = data;
            try
            {
                Dictionary<string, string> dataMap = ParseKeyValuePairs(data);
                result.Date = ConvertTimeStampToDateTime(dataMap["DataTime"]);//DateTime.Parse(dataMap["DataTime"]);
                //result.QuestCode = dataMap["QN"];
                result.SystemCode = dataMap["ST"];
                result.Password = dataMap["PW"];
                result.EquipmentCode = dataMap["MN"];
                if (dataMap.ContainsKey("a01001-Min"))
                {
                    result.TemperatureMin = decimal.Parse(dataMap["a01001-Min"]);
                    result.TemperatureMax = decimal.Parse(dataMap["a01001-Max"]);
                    result.TemperatureAvg = decimal.Parse(dataMap["a01001-Avg"]);
                    result.TemperatureFlag = dataMap["a01001-Flag"][0];

                }

                // 湿度监测因子
                if (dataMap.ContainsKey("a01002-Min"))
                {
                    result.HumidityMin = decimal.Parse(dataMap["a01002-Min"]);
                    result.HumidityMax = decimal.Parse(dataMap["a01002-Max"]);
                    result.HumidityAvg = decimal.Parse(dataMap["a01002-Avg"]);
                    result.HumidityFlag = dataMap["a01002-Flag"][0];

                }


                // 气压监测因子
                if (dataMap.ContainsKey("a01006-Min"))
                {
                    result.AirPressureMin = decimal.Parse(dataMap["a01006-Min"]);
                    result.AirPressureMax = decimal.Parse(dataMap["a01006-Max"]);
                    result.AirPressureAvg = decimal.Parse(dataMap["a01006-Avg"]);
                    result.AirPressureFlag = dataMap["a01006-Flag"][0];
                }
                // 风速监测因子
                if (dataMap.ContainsKey("a01007-Min"))
                {
                    result.WindSpeedMin = decimal.Parse(dataMap["a01007-Min"]);
                    result.WindSpeedMax = decimal.Parse(dataMap["a01007-Max"]);
                    result.WindSpeedAvg = decimal.Parse(dataMap["a01007-Avg"]);
                    result.WindSpeedFlag = dataMap["a01007-Flag"][0];
                }
                // 风向监测因子
                if (dataMap.ContainsKey("a01008-Min"))
                {
                    result.WindDirectionMin = decimal.Parse(dataMap["a01008-Min"]);
                    result.WindDirectionMax = decimal.Parse(dataMap["a01008-Max"]);
                    result.WindDirectionAvg = decimal.Parse(dataMap["a01008-Avg"]);
                    result.WindDirectionFlag = dataMap["a01008-Flag"][0];
                }
                // 扬尘TSP

                if (dataMap.ContainsKey("a34001-Min"))
                {
                    result.ParticulateMatterMin = decimal.Parse(dataMap["a34001-Min"]);
                    result.ParticulateMatterMax = decimal.Parse(dataMap["a34001-Max"]);
                    result.ParticulateMatterAvg = decimal.Parse(dataMap["a34001-Avg"]);
                    result.ParticulateMatterFlag = dataMap["a34001-Flag"][0];
                }
                // 噪声
                if (dataMap.ContainsKey("a50001-Min"))
                {
                    result.NoiseMin = decimal.Parse(dataMap["a50001-Min"]);
                    result.NoiseMax = decimal.Parse(dataMap["a50001-Max"]);
                    result.NoiseMin = decimal.Parse(dataMap["a50001-Avg"]);
                    result.NoiseFlag = dataMap["a50001-Flag"][0];
                }
                // Cpm
                if (dataMap.ContainsKey("Cpm-Min"))
                {
                    result.NoiseMin = decimal.Parse(dataMap["Cpm-Min"]);
                    result.NoiseMax = decimal.Parse(dataMap["Cpm-Max"]);
                    result.NoiseMin = decimal.Parse(dataMap["Cpm-Avg"]);
                    result.NoiseFlag = dataMap["Cpm-Flag"][0];
                }

            }
            catch (ArgumentException ex)
            {
                logger.Error($"数据转换失败，数据为:[{data}]",ex);
            }
            // 温度监测因子
            return result;
        }
        public static Dictionary<string, string> ParseKeyValuePairs(string data)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            string[] pairs = data.Split(';'); // 假设数据以分号分隔

            foreach (string pair in pairs)
            {
                if (pair.Contains('&'))
                {
                    var dt = ExtractValueAfterAnd(pair);
                    string[] keyValue = dt.Split('='); // 假设键值对以等号分隔
                    if (keyValue.Length == 2)
                    {
                        keyValuePairs.Add(keyValue[0], keyValue[1]);
                    }
                }
                else if (pair.Count(i => i == '=') == 1)
                {
                    string[] keyValue = pair.Split('='); // 假设键值对以等号分隔
                    if (keyValue.Length == 2)
                    {
                        keyValuePairs.Add(keyValue[0], keyValue[1]);
                    }
                }
                else
                {
                    string[] mulitpairs = pair.Split(','); // 假设数据以分号分隔
                    foreach (var newPair in mulitpairs)
                    {
                        string[] keyValue = newPair.Split('='); // 假设键值对以等号分隔
                        if (keyValue.Length == 2)
                        {
                            keyValuePairs.Add(keyValue[0], keyValue[1]);
                        }
                    }

                }
            }

            return keyValuePairs;
        }
        public static string ExtractValueAfterAnd(string input)
        {
            // 使用正则表达式匹配 && 后面的值
            var match = Regex.Match(input, @"&&(.+)");
            if (match.Success)
            {
                // 提取并返回匹配到的值
                return match.Groups[1].Value;
            }
            // 如果匹配失败，返回空字符串
            return string.Empty;
        }
        public static DateTime ConvertTimeStampToDateTime(string timeStamp)
        {
            // 假设时间戳格式为 YYYYMMDDHHMMSSZZZ
            // 注意：这个方法假设时区为 Local，
            var match = Regex.Match(timeStamp, @"(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})"); //(\d{3}) ，没有毫秒
            if (match.Success)
            {
                // 提取并返回匹配到的值
                int year = int.Parse(match.Groups[1].Value);
                int month = int.Parse(match.Groups[2].Value);
                int day = int.Parse(match.Groups[3].Value);
                int hour = int.Parse(match.Groups[4].Value);
                int minute = int.Parse(match.Groups[5].Value);
                int second = int.Parse(match.Groups[6].Value);
                //int milliseconds = int.Parse(match.Groups[7].Value);

                // 构建 DateTime 对象
                DateTime dateTime = new DateTime(year, month, day, hour, minute, second, 0, DateTimeKind.Local);
                return dateTime;
            }
            // 如果匹配失败，返回 DateTime.MinValue
            return DateTime.MinValue;
        }
    }

}
