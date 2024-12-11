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

//namespace ESys.Contract.Entity
//{
//    using Microsoft.EntityFrameworkCore;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Linq.Expressions;
//    using System.Reflection;


//    /// <summary>
//    /// 禁用检查器基类
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public abstract class DeactiveCheckerBase<T> : IDeactiveChecker<T> where T : IActiveEntity
//    {
//        private readonly Dictionary<string, (Func<T, bool> predicate, Func<T, DeactivateAgainst> detail)> predicateDic = new();
//        private static readonly MethodInfo includeMethod = typeof(EntityFrameworkQueryableExtensions)
//            .GetMethods(BindingFlags.Static | BindingFlags.Public)
//            .First(m => m.Name == nameof(EntityFrameworkQueryableExtensions.Include)
//                        && m.GetParameters().Last().ParameterType == typeof(string))
//            .MakeGenericMethod(typeof(T));
//        private static readonly PropertyInfo idProperty = typeof(T).GetProperty("Id");

//        /// <summary>
//        /// 注册一对一属性
//        /// </summary>
//        /// <typeparam name="TProperty"></typeparam>
//        /// <typeparam name="TDisplay"></typeparam>
//        /// <param name="expression"></param>
//        /// <param name="display"></param>
//        protected void RegisterSingle<TProperty, TDisplay>(
//            Expression<Func<T, TProperty>> expression,
//            Func<TProperty, TDisplay> display = null) where TProperty : IActiveEntity
//        {
//            if (expression.Body is MemberExpression memberExpression)
//            {
//                static bool propertyFunc(TProperty one) => one == null || !one.IsActive;
//                var oneFunc = expression.Compile();
//                var realDisplay = display == null ? null
//                    : typeof(TDisplay) == typeof(string) ? display as Func<TProperty, string>
//                    : p => display(p).ToString();
//                this.predicateDic.Add(memberExpression.Member.Name,
//                    (
//                        predicate: t => propertyFunc(oneFunc(t)),
//                        detail: t => new DeactivateAgainst()
//                        {
//                            EntityType = typeof(TProperty),
//                            NavigationProperty = memberExpression.Member.Name,
//                            Values = realDisplay == null
//                                ? Array.Empty<string>()
//                                : new[] { realDisplay(oneFunc(t)) }
//                        }
//                    ));
//            }
//            else
//            {
//                throw new ArgumentException("表达式必须为成员属性", nameof(expression));
//            }
//        }

//        /// <summary>
//        /// 注册一对多属性
//        /// </summary>
//        /// <typeparam name="TProperty"></typeparam>
//        /// <typeparam name="TDisplay"></typeparam>
//        /// <param name="expression"></param>
//        /// <param name="display"></param>
//        protected void RegisterMultis<TProperty, TDisplay>(
//            Expression<Func<T, ICollection<TProperty>>> expression,
//            Func<TProperty, TDisplay> display = null) where TProperty : IActiveEntity
//        {
//            if (expression.Body is MemberExpression memberExpression)
//            {
//                static bool propertyFunc(ICollection<TProperty> many) => many == null || many.All(m => !m.IsActive);
//                static string[] valueFunc(ICollection<TProperty> many, Func<TProperty, string> labelFunc) =>
//                    many == null || labelFunc == null
//                    ? Array.Empty<string>()
//                    : many.Select(m => labelFunc(m)).ToArray();
//                var manyFunc = expression.Compile();
//                var realDisplay = display == null ? null
//                    : typeof(TDisplay) == typeof(string) ? display as Func<TProperty, string>
//                    : p => display(p).ToString();
//                this.predicateDic.Add(memberExpression.Member.Name,
//                    (
//                        predicate: t => propertyFunc(manyFunc(t)),
//                        detail: t => new DeactivateAgainst()
//                        {
//                            EntityType = typeof(TProperty),
//                            NavigationProperty = memberExpression.Member.Name,
//                            Values = valueFunc(manyFunc(t), realDisplay)
//                        }
//                    ));
//            }
//            else
//            {
//                throw new ArgumentException("表达式必须为成员属性", nameof(expression));
//            }
//        }

//        /// <summary>
//        /// 检查是否能禁用
//        /// </summary>
//        /// <param name="source">数据源</param>
//        /// <param name="key">主键</param>
//        /// <returns></returns>
//        public IReadOnlyList<DeactivateAgainst> CheckCanDeactive(IQueryable<T> source, object key)
//        {
//            var tmp = source;
//            foreach (var item in this.predicateDic)
//            {
//                tmp = includeMethod.Invoke(null, new object[] { tmp, item.Key }) as IQueryable<T>;
//            }

//            var parExpression = Expression.Parameter(typeof(T));
//            var idExpression = Expression.Lambda<Func<T, bool>>(
//                Expression.Equal(Expression.Property(parExpression, idProperty), Expression.Constant(key)),
//                parExpression);
//            var entity = tmp.FirstOrDefault(idExpression);
//            if (entity == null)
//            {
//                throw new ArgumentOutOfRangeException(nameof(key));
//            }
//            var ret = new List<DeactivateAgainst>();
//            foreach (var predicate in this.predicateDic)
//            {
//                if (!predicate.Value.predicate(entity))
//                {
//                    ret.Add(predicate.Value.detail(entity));
//                }
//            }
//            return ret;
//        }
//    }
//}
