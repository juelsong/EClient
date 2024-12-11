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
    using System.IO;

    /// <summary>
    /// 报告生成器
    /// </summary>
    public interface IReportExcelGenerator 
    {
        /// <summary>
        /// 是否能处理此类型报告
        /// </summary>
        /// <param name="reportType">报告类型</param>
        /// <param name="jsonParameter">json格式参数</param>
        /// <param name="locale">语言</param>
        /// <param name="ms">excel</param>
        /// <returns></returns>
        public bool TryProcess(string reportType,string jsonParameter, string locale, ref MemoryStream ms);
    }
}
