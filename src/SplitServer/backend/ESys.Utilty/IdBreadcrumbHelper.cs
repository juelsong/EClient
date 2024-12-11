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

namespace ESys.Utilty
{
    using System;
    using System.Globalization;
    using System.Linq;

#pragma warning disable 1591
    public static class IdBreadcrumbHelper
    {
        public static bool IsIdInBreadcrumbPath(int? id, string breadcrumbPath)
        {
            return id == null 
                || (!string.IsNullOrEmpty(breadcrumbPath) 
                    && breadcrumbPath.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                     .Contains(id.Value.ToString(CultureInfo.InvariantCulture)));
        }
    }
#pragma warning restore 1591
}
