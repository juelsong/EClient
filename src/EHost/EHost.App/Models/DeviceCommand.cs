using System.Text.Json.Serialization;

namespace EHost.App.Models
{
    /// <summary>
    /// 设备查询请求模型
    /// </summary>
    public class DeviceQueryRequest
    {
        /// <summary>
        /// 查询类型，必填，包括：all, board, system, unknown
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 查询指定设备id，非必填
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名，无token时必填
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码，无token时必填
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 权限令牌，无用户名密码时必填
        /// </summary>
        public string Token { get; set; }
    }
    /// <summary>
    /// 设备查询响应模型
    /// </summary>
    public class DeviceQueryResponse
    {
        /// <summary>
        /// 响应状态，可能的值为 "success" 或 "error"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 响应消息，描述操作的结果
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 设备信息列表，仅在 status 为 "success" 时存在
        /// </summary>
        public List<DeviceInfo> Data { get; set; }

        /// <summary>
        /// 错误码，仅在 status 为 "error" 时存在
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// 设备命令请求模型
    /// </summary>
    public class DeviceCommandRequest
    {
        /// <summary>
        /// data_send
        /// </summary>
        [JsonPropertyName("data_send")]
        public DeciceData Request { get; set; }
        public class DeciceData
        {
            /// <summary>
            /// 要连接设备的id列表，必填
            /// </summary>
            [JsonPropertyName("ids")]
            public List<string> Ids { get; set; }

            /// <summary>
            /// 发送给设备的数据，必填
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }

            /// <summary>
            /// 数据类型，必填，可以是 hex, ascii, utf8
            /// </summary>
            [JsonPropertyName("data_type")]
            public string DataType { get; set; }

            /// <summary>
            /// 获取回应类型，必填，可以是 hex, ascii, utf8
            /// </summary>
            [JsonPropertyName("response_type")]
            public string ResponseType { get; set; }

            /// <summary>
            /// 用户名，无token时必填
            /// </summary>
            [JsonPropertyName("username")]
            public string Username { get; set; }

            /// <summary>
            /// 密码，无token时必填
            /// </summary>
            [JsonPropertyName("password")]
            public string Password { get; set; }

            /// <summary>
            /// 权限令牌，无用户名密码时必填
            /// </summary>
            [JsonPropertyName("token")]
            public string Token { get; set; }
        }
    }
    /// <summary>
    /// 设备命令响应模型
    /// </summary>
    public class DeviceCommandResponse
    {
        /// <summary>
        /// 响应状态，可能的值为 "success" 或 "error"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 响应消息，描述操作的结果
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 命令执行结果数据列表
        /// </summary>
        public List<CommandResult> Data { get; set; }

        /// <summary>
        /// 错误码，仅在 status 为 "error" 时存在
        /// </summary>
        public string Code { get; set; }
    }
    /// <summary>
    /// 命令结果
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// 返回数据类型
        /// </summary>
        public string ResponseType { get; set; }

        /// <summary>
        /// 响应状态，可能的值为 "success" 或 "error"
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 响应消息，仅在 status 为 "error" 时存在
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误码，仅在 status 为 "error" 时存在
        /// </summary>
        public string Code { get; set; }
    }
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// 远程地址
        /// </summary>
        public string RemoteAddress { get; set; }

        /// <summary>
        /// 远程端口
        /// </summary>
        public string RemotePort { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 统一识别码
        /// </summary>
        public string UID { get; set; }
    }
}
