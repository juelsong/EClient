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

namespace ESys.Contract.Entity
{
    using Furion.DatabaseAccessor;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    /// <summary>
    /// 数据库视图类型配置依赖接口
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public interface IPrivateViewTypeBuilder<TView> where TView : class
    {
        /// <summary>
        /// 实体类型配置
        /// </summary>
        /// <param name="entityBuilder">实体类型构建器</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dbContextLocator">数据库上下文定位器</param>
        void Configure(EntityTypeBuilder<TView> entityBuilder, DbContext dbContext, Type dbContextLocator);
    }

    /// <summary>
    /// 数据库视图类型配置依赖接口
    /// </summary>
    /// <typeparam name="TView">实体类型</typeparam>
    public interface IViewTypeBuilder<TView> : IViewTypeBuilder<TView, MasterDbContextLocator>
        where TView : class
    {
    }
    /// <summary>
    /// 数据库视图类型配置依赖接口
    /// </summary>
    /// <typeparam name="TView">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    public interface IViewTypeBuilder<TView, TDbContextLocator1> : IPrivateViewTypeBuilder<TView>
        where TView : class
        where TDbContextLocator1 : class, IDbContextLocator
    { }
    /// <summary>
    /// 数据库视图类型配置依赖接口
    /// </summary>
    /// <typeparam name="TView">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator1">数据库上下文定位器</typeparam>
    /// <typeparam name="TDbContextLocator2">数据库上下文定位器</typeparam>
    public interface IViewTypeBuilder<TView, TDbContextLocator1, TDbContextLocator2> : IPrivateViewTypeBuilder<TView>
        where TView : class
        where TDbContextLocator1 : class, IDbContextLocator
        where TDbContextLocator2 : class, IDbContextLocator
    { }
}
