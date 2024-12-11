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
namespace ESys.Notification.Query
{
    using ESys.Infrastructure.Entity;
    using ESys.Infrastructure.Query;
    using ESys.Notification.Entity;
    using System;
    using System.Linq.Expressions;

    internal class SubscriptionQueryVisitor : LocationQueryVisitorBase<Subscription>
    {
        protected override Expression<Func<Subscription, Location>> GetExpression() => t => t.Location;

        public override string VisitQuery(int userId, string locationBreadcrumb, Type entityType)
        {
            return entityType == typeof(Subscription)
                   ? $"(User/Location eq null) or (startswith(User/Location/LocationExtra/Breadcrumb, '{locationBreadcrumb}'))"
                   : string.Empty;
        }
    }
}
