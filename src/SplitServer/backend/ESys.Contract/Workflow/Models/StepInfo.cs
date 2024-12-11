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

namespace CN.Metaura.EMIS.Contract.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    /// <summary>
    /// 流程步骤信息
    /// </summary>
    public class StepInfo
    {
        /// <summary>
        /// 参数信息
        /// </summary>
        public StepParameter Parameter { get; set; } = new();
        /// <summary>
        /// 状态信息
        /// </summary>
        public StepStatus Status { get; set; } = new();
    }
    /// <summary>
    /// 流程步骤带数据信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StepInfoOfT<T> : StepInfo
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
