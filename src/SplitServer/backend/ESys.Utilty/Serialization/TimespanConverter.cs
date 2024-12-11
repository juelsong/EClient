/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ +++ + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting
 *         ┃   ┃ +     神兽保佑, 代码无bug
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

namespace ESys.Utilty.Serialization
{
    using System;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// TimeSpan 转换器 [-][d.]hh:mm:ss[.fffffff]
    /// </summary>
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions option)
        {
            if (reader.TokenType != JsonTokenType.String || typeToConvert != typeof(TimeSpan))
            {
                throw new NotSupportedException();
            }
            // 用常量"c”决定用 [-][d.]hh:mm:ss[.fffffff] 作为 TimeSpans 换的格式
            return TimeSpan.ParseExact(reader.GetString() ?? string.Empty, "c", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("c", CultureInfo.InvariantCulture));
        }
    }
    /// <summary>
    /// TimeSpan 转换器 [-][d.]hh:mm:ss[.fffffff]
    /// </summary>
    public class NullableTimeSpanConverter : JsonConverter<TimeSpan?>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            if(string.IsNullOrEmpty(str)|| !TimeSpan.TryParseExact(str,"c", CultureInfo.InvariantCulture,out var ret))
            {
                return null;
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                var str = value.Value.ToString("c", CultureInfo.InvariantCulture);
                writer.WriteStringValue(str);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
