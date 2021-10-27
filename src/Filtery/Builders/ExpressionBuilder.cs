using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
// ReSharper disable AssignNullToNotNullAttribute

namespace Filtery.Builders
{
    internal enum OperatorComparer
    {
        Equals = ExpressionType.Equal,
        NotEqual = ExpressionType.NotEqual,
        Contains,
        GreaterThan = ExpressionType.GreaterThan,
        LessThan = ExpressionType.LessThan,
        GreaterThanOrEqual = ExpressionType.GreaterThanOrEqual,
        LessThanOrEqual = ExpressionType.LessThan,
        StartsWith,
        EndsWith
    }

    //https://stackoverflow.com/a/23754707
    internal class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> BuildPredicate<T>(object value, OperatorComparer comparer,
            bool caseSensitive,
            params string[] properties)
        {
            var parameterExpression = Expression.Parameter(typeof(T), typeof(T).Name);
            return (Expression<Func<T, bool>>) BuildNavigationExpression(parameterExpression, comparer, value,
                caseSensitive,
                properties);
        }

        private static Expression BuildNavigationExpression(Expression parameter, OperatorComparer comparer,
            object value, bool caseSensitive, params string[] properties)
        {
            Expression resultExpression;
            Expression childParameter, predicate;
            Type childType = null;

            if (properties.Count() > 1)
            {
                //build path
                parameter = Expression.Property(parameter, properties[0]);
                var isCollection = typeof(IEnumerable).IsAssignableFrom(parameter.Type);
                //if it´s a collection we later need to use the predicate in the methodexpressioncall
                if (isCollection)
                {
                    childType = parameter.Type.GetGenericArguments()[0];
                    childParameter = Expression.Parameter(childType, childType.Name);
                }
                else
                {
                    childParameter = parameter;
                }

                //skip current property and get navigation property expression recursivly
                var innerProperties = properties.Skip(1).ToArray();
                predicate = BuildNavigationExpression(childParameter, comparer, value, caseSensitive ,innerProperties);
                if (isCollection)
                {
                    //build subquery
                    resultExpression = BuildSubQuery(parameter, childType, predicate);
                }
                else
                {
                    resultExpression = predicate;
                }
            }
            else
            {
                //build final predicate
                resultExpression = BuildCondition(parameter, properties[0], comparer, value, caseSensitive);
            }

            return resultExpression;
        }

        private static Expression BuildSubQuery(Expression parameter, Type childType, Expression predicate)
        {
            var anyMethod = typeof(Enumerable).GetMethods()
                .Single(m => m.Name == "Any" && m.GetParameters().Length == 2);
            anyMethod = anyMethod.MakeGenericMethod(childType);
            predicate = Expression.Call(anyMethod, parameter, predicate);
            return MakeLambda(parameter, predicate);
        }

        private static Expression BuildCondition(Expression parameter, string property, OperatorComparer comparer,
            object value, bool caseSensitive)
        {
            var childProperty = parameter.Type.GetProperty(property);
            var left = Expression.Property(parameter, childProperty);
            var right = Expression.Constant(value);
            var predicate = BuildComparsion(left, comparer, right, caseSensitive);
            return MakeLambda(parameter, predicate);
        }

        private static Expression BuildComparsion(Expression left, OperatorComparer comparer, Expression right,
            bool caseSensitive)
        {
            var mask = new List<OperatorComparer>
            {
                OperatorComparer.Contains,
                OperatorComparer.StartsWith,
                OperatorComparer.EndsWith
            };
            if (mask.Contains(comparer) && left.Type != typeof(string))
            {
                comparer = OperatorComparer.Equals;
            }

            if (!mask.Contains(comparer))
            {
                return Expression.MakeBinary((ExpressionType) comparer, left, Expression.Convert(right, left.Type));
            }

            return BuildStringCondition(left, comparer, right, caseSensitive);
        }

        private static Expression BuildStringCondition(Expression left, OperatorComparer comparer, Expression right,
            bool caseSensitive)
        {
            var compareMethod = typeof(string).GetMethods().First(m =>
                m.Name.Equals(Enum.GetName(typeof(OperatorComparer), comparer)) && m.GetParameters().Count() == 1);

            if (!caseSensitive)
            {
                var toLowerMethod = typeof(string).GetMethods()
                    .First(m => m.Name.Equals("ToLower") && m.GetParameters().Count() == 0);
                left = Expression.Call(left, toLowerMethod);
                right = Expression.Call(right, toLowerMethod);
            }

            return Expression.Call(left, compareMethod, right);
        }

        private static Expression MakeLambda(Expression parameter, Expression predicate)
        {
            var resultParameterVisitor = new ParameterVisitor();
            resultParameterVisitor.Visit(parameter);
            var resultParameter = resultParameterVisitor.Parameter;
            return Expression.Lambda(predicate, (ParameterExpression) resultParameter);
        }

        private class ParameterVisitor : ExpressionVisitor
        {
            public Expression Parameter { get; private set; }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                Parameter = node;
                return node;
            }
        }
    }
}