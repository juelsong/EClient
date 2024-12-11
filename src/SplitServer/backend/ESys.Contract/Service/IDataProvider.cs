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

namespace ESys.Contract.Service
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 签名数据
    /// </summary>
    public class ESignData
    {
        /// <summary>
        /// 签名账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 签名用户姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 签名用户Id
        /// </summary>
		public int ESignedBy { get; set; }
        /// <summary>
        /// 修改、创建数据用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// ip地址
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// 签名分类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 签名的顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSystemOperation { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }
    }

    /// <summary>
    /// 签名数据提供者
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// 获取签名数据
        /// </summary>
        /// <param name="esign"></param>
        /// <returns></returns>
        bool TryGetESignData(out IEnumerable<ESignData> esign);
        /// <summary>
        /// 获取当前用户Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool TryGetCurrentUserId(out int userId);
    }
}
