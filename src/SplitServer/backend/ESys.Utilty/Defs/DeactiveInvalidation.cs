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

namespace ESys.Utilty.Defs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// 无法禁用原因
    /// </summary>
    public class DeactiveInvalidation
    {
        internal DeactiveInvalidation(string navigationProperty, IEnumerable values, string entityName)
        {
            this.NavigationProperty = navigationProperty;
            this.Values = values;
            this.EntityName = entityName;
        }

        /// <summary>
        /// 导航属性
        /// </summary>
        public string NavigationProperty { get; }

        /// <summary>
        /// 实体值
        /// </summary>
        public IEnumerable Values { get; }

        /// <summary>
        /// 实体类型名称
        /// </summary>
        public string EntityName { get; }
        /// <summary>
        /// 生成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static DeactiveInvalidation Generate<T>(Expression<Func<T, object>> property, IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }
            var func = property.Compile();
            MemberExpression member = null;
            if (property.Body is UnaryExpression unary && unary.Operand is MemberExpression member1)
            {
                member = member1;
            }
            else if (property.Body is MemberExpression member2)
            {
                member = member2;
            }
            return member == null ?
                 throw new InvalidOperationException(nameof(property))
                : new DeactiveInvalidation(member.Member.Name, values.Select(v => func(v)).ToArray(), typeof(T).Name);

        }
    }

}
