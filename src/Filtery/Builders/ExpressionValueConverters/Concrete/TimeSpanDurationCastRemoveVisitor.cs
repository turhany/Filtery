using System;
using System.Linq.Expressions;

namespace Filtery.Builders.ExpressionValueConverters.Concrete
{
    public class TimeSpanDurationCastRemoveVisitor: ExpressionVisitor
    {
        public static Expression Convert(Expression expression)
        {
            var visitor = new TimeSpanDurationCastRemoveVisitor();

            var visitedExpression = visitor.Visit(expression);

            return visitedExpression;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Convert && node.Type == typeof(TimeSpanDuration))
            {
                return node.Operand;
            }

            return base.VisitUnary(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (node.ReturnType != typeof(TimeSpanDuration) || node.Parameters.Count <= 0)
            {
                return base.VisitLambda(node);
            }

            var delegateType = typeof(Func<,>).MakeGenericType(node.Parameters[0].Type, typeof(long));

            return Expression.Lambda(delegateType, Visit(node.Body), node.Parameters);
        }

        protected override Expression VisitMember(MemberExpression node) => node.Member.DeclaringType == typeof(TimeSpanDuration) ? Expression.Property(Visit(node.Expression), node.Member.Name) : base.VisitMember(node);
    }
}