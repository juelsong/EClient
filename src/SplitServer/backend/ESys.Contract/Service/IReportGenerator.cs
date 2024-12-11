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
    /// <summary>
    /// 报告生成器
    /// </summary>
    public interface IReportGenerator
    {
        /// <summary>
        /// 是否能处理此类型报告
        /// </summary>
        /// <param name="reportType">报告类型</param>
        /// <param name="jsonParameter">json格式参数</param>
        /// <param name="locale">语言</param>
        /// <param name="reportString">报告字符串</param>
        /// <returns></returns>
        public bool TryProcess(string reportType,string jsonParameter, string locale, ref string reportString);
    }
}
