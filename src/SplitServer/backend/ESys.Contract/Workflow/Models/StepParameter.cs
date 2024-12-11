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

    /// <summary>
    /// 输入输出参数
    /// </summary>
    public class IOParameter
    {
        /// <summary>
        /// 等号左边属性
        /// </summary>
        public string Left { get; set; }
        /// <summary>
        /// 等号右边属性
        /// </summary>
        public string Right { get; set; }
    }

    /// <summary>
    /// 步骤输入输出参数
    /// </summary>
    public class StepParameter
    {
        /// <summary>
        /// 输入参数
        /// </summary>
        public IOParameter[] Inputs { get; set; } = Array.Empty<IOParameter>();
        /// <summary>
        /// 输出参数
        /// </summary>
        public IOParameter[] Outputs { get; set; } = Array.Empty<IOParameter>();
    }
}
