/*
 *        ┏┓   ┏┓+ +
 *       ┏┛┻━━━┛┻┓ + +
 *       ┃       ┃
 *       ┃   ━   ┃ ++ + + +
 *       ████━████ ┃+
 *       ┃       ┃ +
 *       ┃   ┻   ┃
 *       ┃       ┃ + +
 *       ┗━┓   ┏━┛
 *         ┃   ┃
 *         ┃   ┃ + + + +
 *         ┃   ┃    Code is far away from bug with the animal protecting       
 *         ┃   ┃ +     神兽保佑,代码无bug  
 *         ┃   ┃
 *         ┃   ┃  +
 *         ┃    ┗━━━┓ + +
 *         ┃        ┣┓
 *         ┃        ┏┛
 *         ┗┓┓┏━┳┓┏┛ + + + +
 *          ┃┫┫ ┃┫┫
 *          ┗┻┛ ┗┻┛+ + + +
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ESys.Utilty.Defs
{

    /// <summary>
    /// 无返回对象的操作结果
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 结果码，正常为0
        /// </summary>
        [JsonPropertyName("code")]
        [JsonInclude]
        public int Code { get; internal set; } = ErrorCode.NoError;
        /// <summary>
        /// 失败原因，正常为空字符串
        /// </summary>
        [JsonPropertyName("message")]
        [JsonInclude]
        public string Message { get; internal set; } = "";
        /// <summary>
        /// 是否操作成功
        /// </summary>
        [JsonPropertyName("success")]
        [JsonInclude]
        public bool Success { get; internal set; } = true;
        /// <summary>
        /// 时间戳
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonInclude]
        public DateTimeOffset Timestamp { get; private set; } = DateTimeOffset.Now;
    }

    /// <summary>
    /// 带返回对象的操作结果
    /// </summary>
    /// <typeparam name="T">返回对象类型</typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// 操作返回对象
        /// </summary>
        [JsonPropertyName("data")]
        [JsonInclude]
        public T Data { get; internal set; }
        public Result()
        {
        }
    }

    /// <summary>
    /// ResultBuilder
    /// </summary>
    public static class ResultBuilder
    {
        /// <summary>
        /// 创建成功结果
        /// </summary>
        /// <param name="data">结果值</param>
        /// <returns></returns>
        public static Result<T> Ok<T>(T data)
        {
            return new Result<T>()
            {
                Data = data
            };
        }

        /// <summary>
        /// 创建错误结果
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        public static Result<T> Error<T>(int code, string msg = null)
        {
            return new Result<T>()
            {
                Code = code,
                Message = msg,
                Success = false
            };
        }
        /// <summary>
        /// 创建错误结果
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="data">数据</param>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        public static Result<T> Error<T>(int code, T data, string msg = null)
        {
            return new Result<T>()
            {
                Code = code,
                Message = msg,
                Success = false,
                Data = data
            };
        }

        /// <summary>
        /// 创建成功结果
        /// </summary>
        /// <returns></returns>
        public static Result Ok()
        {
            return new Result();
        }

        /// <summary>
        /// 创建错误结果
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        public static Result Error(int code, string msg = null)
        {
            return new Result()
            {
                Code = code,
                Message = msg,
                Success = false
            };
        }
        /// <summary>
        /// 创建OData错误结果
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="msg">错误消息</param>
        /// <param name="detail">详细内容</param>
        /// <returns></returns>
        public static BadRequestObjectResult CreateODataError(int code, string msg = "", object detail = null)
        {
            var odataError = new ODataError()
            {
                ErrorCode = code.ToString(),
                Message = msg,
            };
            if (detail != null)
            {
                odataError.Target = JsonSerializer.Serialize(detail);
            }
            return new BadRequestObjectResult(odataError);
        }
    }
}