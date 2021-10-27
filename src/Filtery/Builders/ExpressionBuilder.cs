using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Filtery.Builders.ExpressionValueConverters.Abstract;
using Filtery.Builders.ExpressionValueConverters.Concrete;
using Filtery.Extensions;
using Filtery.Models.Filter;
using Filtery.Models.Order;

namespace Filtery.Builders
{
    public class ExpressionBuilder
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains");
        private static readonly MethodInfo StartsWithMethod = typeof(string).GetMethod("StartsWith", new[] {typeof(string)});
        private static readonly MethodInfo EndsWithMethod = typeof(string).GetMethod("EndsWith", new[] {typeof(string)});
        private static readonly MethodInfo EnumerableAllMethod = typeof(Enumerable).GetMethods().Last(c => c.Name.StartsWith("All"));
        private static readonly MethodInfo EnumerableAnyMethod = typeof(Enumerable).GetMethods().Last(c => c.Name.StartsWith("Any"));
        private static readonly MethodInfo EnumerableFirstOrDefaultMethod = typeof(Enumerable).GetMethods().First(c => c.Name.StartsWith("FirstOrDefault"));
        private BinaryExpression GetExpression<T>(Expression param, FilterExpression filter1, FilterExpression filter2) => Expression.OrElse(GetExpression<T>(param, filter1), GetExpression<T>(param, filter2));
        private BinaryExpression GetExpressionAnd<T>(Expression param, FilterExpression filter1, FilterExpression filter2) => Expression.AndAlso(GetExpression<T>(param, filter1), GetExpression<T>(param, filter2));

        private static readonly Random Random = new Random();
        // private static ConcurrentDictionary<string, LambdaExpression> RegisteredFilterExpressions { get; set; }
        // private static ConcurrentDictionary<string, string> ReversedRegisteredExpressions { get; set; }
        private string GenerateRandomParameterName() => "parameter" + Random.Next();
        private bool IsIEnumerableCall(LambdaExpression expression) =>
            expression.Body.NodeType.In(ExpressionType.Call, ExpressionType.MemberAccess) &&
            expression.Body.Type.IsGenericType &&
            expression.Body.Type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        
        
        
        
        
        /// <summary>
        /// Initializes static members of the <see cref="ExpressionBuilder" /> class.
        /// </summary>
        static ExpressionBuilder()
        {
            // RegisteredFilterExpressions = new ConcurrentDictionary<string, LambdaExpression>();
            // ReversedRegisteredExpressions = new ConcurrentDictionary<string, string>();
        }

        /// <summary>
        /// Gets the value converters.
        /// </summary>
        public static List<IExpressionValueConverter> ValueConverters { get; } = new List<IExpressionValueConverter>();
 
        public static void RegisterExpressions(Type type)
        {
            foreach (var fieldInfo in type.GetFields())
            {
                var lambdaExpression = (LambdaExpression)fieldInfo.GetValue(null);
                var modelType = lambdaExpression.Parameters.First();

                RegisterExpression(fieldInfo.Name.Substring(modelType.Type.Name.Length), lambdaExpression);
            }
        }

        /// <summary>
        /// Registers the expression by expression string.
        /// </summary>
        /// <param name="expressionString">The expression string.</param>
        /// <param name="expression">The expression.</param>
        private static void RegisterExpression(string expressionString, LambdaExpression expression)
        {
            var key = $"{expression.Parameters.First().Type.FullName}-{expressionString}";

            // RegisteredFilterExpressions.TryAdd(key, expression);
            // ReversedRegisteredExpressions.TryAdd(SerializeExpression(expression), expressionString);
        }

        private static string SerializeExpression(LambdaExpression expression)
        {
            return expression.Parameters.First().Type.Name +
                   "-" +
                   expression.ReturnType.Name +
                   "-" +
                   string.Join(".",
                       expression.Body.ToString().Split('.').Skip(1).Where(c => !c.Contains("Select("))
                           .Select(q => q.Replace(")", string.Empty)));
        }

        /// <summary>
        /// Builds lambda expression from specified sort expression and sort type then appends the specified query.
        /// <param name="sortExpression">Expression strings</param>
        /// should be registered from <see cref="RegisterExpression" />.
        /// </summary>
        /// <typeparam name="T">Type of the instance that will be filtered.</typeparam>
        /// <param name="query"><see cref="IQueryable{T}" /> instance that will be filtered.</param>
        /// <param name="sortExpression">
        /// The sort expression. Expression should be registered from
        /// <see cref="RegisterExpression" />.
        /// </param>
        /// <param name="sortType">Type of the sort.</param>
        /// <returns>Filtered <see cref="IQueryable{T}" /> instance.</returns>
        public IQueryable<T> AppendQuery<T>(IQueryable<T> query, string sortExpression, OrderOperation sortType)
        {
            if (string.IsNullOrWhiteSpace(sortExpression))
            {
                return query;
            }

            var expression = GetRegisteredExpression(typeof(T), sortExpression);
            if (IsIEnumerableCall(expression))
            {
                var returnType = expression.ReturnType.GetGenericArguments().First();
                var callExpression = Expression.Call(EnumerableFirstOrDefaultMethod.MakeGenericMethod(returnType),
                    expression.Body);

                var delegateType = typeof(Func<,>).MakeGenericType(expression.Parameters.First().Type, returnType);

                expression = Expression.Lambda(delegateType, callExpression, expression.Parameters);
            }

            if (expression.Body.Type == typeof(TimeSpanDuration))
            {
                expression = (LambdaExpression)TimeSpanDurationCastRemoveVisitor.Convert(expression);
            }

            return sortType == OrderOperation.Ascending
                ? Queryable.OrderBy(query, expression as dynamic)
                : Queryable.OrderByDescending(query, expression as dynamic);
        }

        private string GenerateExpressionKey(Type objectType, string expressionString) => $"{objectType}-{expressionString}";
 
        private Expression GetExpression<T>(Expression param, FilterExpression filter)
        {
            var expression = GetRegisteredExpression(typeof(T), filter.PropertyName);

            expression =
                (LambdaExpression)new ParameterUpdateVisitor(expression.Parameters.First(),
                    param as ParameterExpression).Visit(expression);

            if (IsIEnumerableCall(expression))
            {
                var memberType = expression.Body.Type.GetGenericArguments().First();

                var parameter = Expression.Parameter(memberType, GenerateRandomParameterName());

                var lambda = Expression.Lambda(typeof(Func<,>).MakeGenericType(memberType, typeof(bool)),
                    GetInnerExpression(parameter, filter.Operator, memberType, filter.Value), parameter);

                var enumerableCall = filter.Operator == FilterOperation.NotEqual
                    ? EnumerableAllMethod.MakeGenericMethod(memberType)
                    : EnumerableAnyMethod.MakeGenericMethod(memberType);

                return Expression.Call(enumerableCall, expression.Body, lambda);
            }

            return GetInnerExpression(expression.Body, filter.Operator, expression.Body.Type, filter.Value);
        }
 
        private Expression GetInnerExpression(Expression left, FilterOperation filterOperator, Type valueType,
            object filterValue)
        {
            ConstantExpression valueConstant;

            if (valueType == typeof(TimeSpanDuration))
            {
                valueConstant = Expression.Constant((long)MakeUpValue(filterValue, valueType), typeof(long));
                left = TimeSpanDurationCastRemoveVisitor.Convert(left);
            }

            else
            {
                valueConstant = Expression.Constant(MakeUpValue(filterValue, valueType), valueType);
            }

            if (valueType.IsEnum)
            {
                var valueConstantInt = Expression.Convert(valueConstant, typeof(int));

                if (valueType.GetCustomAttribute<FlagsAttribute>() != null)
                {
                    var leftExpression = Expression.And(Expression.Convert(left, typeof(int)), valueConstantInt);

                    switch (filterOperator)
                    {
                        case FilterOperation.Equal:
                            return Expression.Equal(leftExpression, valueConstantInt);
                        case FilterOperation.NotEqual:
                            return Expression.NotEqual(leftExpression, valueConstantInt);
                        default:
                            throw new ArgumentOutOfRangeException(nameof(filterOperator), filterOperator, null);
                    }
                }

                var leftExpressionConvert = Expression.Convert(left, typeof(int));

                switch (filterOperator)
                {
                    case FilterOperation.Equal:
                        return Expression.Equal(left, valueConstant);
                    case FilterOperation.NotEqual:
                        return Expression.Not(Expression.Equal(left, valueConstant));
                    case FilterOperation.GreaterThan:
                        return Expression.GreaterThan(leftExpressionConvert, valueConstantInt);
                    case FilterOperation.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(leftExpressionConvert, valueConstantInt);
                    case FilterOperation.LowerThan:
                        return Expression.LessThan(leftExpressionConvert, valueConstantInt);
                    case FilterOperation.LowerThanOrEqual:
                        return Expression.LessThanOrEqual(leftExpressionConvert, valueConstantInt);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(filterOperator), filterOperator, null);
                }
            }

            switch (filterOperator)
            {
                case FilterOperation.Equal:
                    return Expression.Equal(left, valueConstant);
                case FilterOperation.NotEqual:
                    return Expression.Not(Expression.Equal(left, valueConstant));
                case FilterOperation.GreaterThan:
                    return Expression.GreaterThan(left, valueConstant);
                case FilterOperation.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(left, valueConstant);
                case FilterOperation.LowerThan:
                    return Expression.LessThan(left, valueConstant);
                case FilterOperation.LowerThanOrEqual:
                    return Expression.LessThanOrEqual(left, valueConstant);
                case FilterOperation.Contains:
                    return Expression.Call(left, ContainsMethod, valueConstant);
                case FilterOperation.StartsWith:
                    return Expression.Call(left, StartsWithMethod, valueConstant);
                case FilterOperation.EndsWith:
                    return Expression.Call(left, EndsWithMethod, valueConstant);
            }

            return null;
        }

        private LambdaExpression GetRegisteredExpression(Type objectType, string expressionString)
        {
            var key = GenerateExpressionKey(objectType, expressionString);
            return !IsExpressionRegistered(key)
                ? throw new ArgumentException(
                    $"'{expressionString}' is not valid filter name. Register your filter expression at ExpressionConstants class.")
                : RegisteredFilterExpressions[key];
        }

        //private bool IsExpressionRegistered(string key) => RegisteredFilterExpressions.ContainsKey(key);

        /// <summary>
        /// Returns true if the filter expression name is registered.
        /// </summary>
        /// <param name="toBeFilteredObjectType">Type of the model that will be filtered.</param>
        /// <param name="filterExpression">The filter expression</param>
        /// <summary />
        public bool IsExpressionRegistered(FilterExpression filterExpression, Type toBeFilteredObjectType) => IsExpressionRegistered(GenerateExpressionKey(toBeFilteredObjectType, filterExpression.PropertyName));

        

        private object MakeUpValue(object input, Type memberType)
        {
            var isNullable = false;
            if (memberType.IsGenericType && (isNullable = memberType.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                memberType = Nullable.GetUnderlyingType(memberType);
            }

            if (input is string[])
            {
                var singleItem = string.Join(" ", (string[])input);

                if (FilterDefinition.NullSeedValue == singleItem)
                {
                    return null;
                }

                var customValueConverter = ValueConverters.FirstOrDefault(l => l.IsSatisfied(memberType, input));
                if (customValueConverter != null)
                {
                    return customValueConverter.Convert(singleItem);
                }

                if (memberType == typeof(string))
                {
                    return singleItem?.Trim();
                }

                if (isNullable && string.IsNullOrEmpty(singleItem))
                {
                    return null;
                }

                if (memberType == typeof(Guid))
                {
                    // Assume User has entered Guid.Empty if input is not parsable
                    // It makes sense because query will yield empty result set when user enters either invalid or empty guid value.
                    Guid.TryParse(singleItem, out Guid guidVal);
                    return guidVal;
                }

                if (memberType == typeof(DateTime) || memberType == typeof(DateTimeOffset))
                {
                    return DateTime.Parse(singleItem);
                }

                if (memberType.IsEnum)
                {
                    return Enum.Parse(memberType, MakeUpValue(singleItem, memberType).ToString());
                }

                if (memberType == typeof(bool))
                {
                    return bool.Parse(singleItem);
                }

                if (memberType == typeof(int))
                {
                    return int.Parse(singleItem);
                }

                if (memberType == typeof(long))
                {
                    return long.Parse(singleItem);
                }

                if (memberType == typeof(decimal))
                {
                    return decimal.Parse(singleItem);
                }
            }

            return input;
        }

        
        /// <summary>
        /// Builds the specified filters as lambda expression then appends the specified query. Expression strings should be
        /// registered from <see cref="RegisterExpression" />.
        /// </summary>
        /// <typeparam name="T">Type of the instance that will be filtered.</typeparam>
        /// <param name="query"><see cref="IQueryable{T}" /> instance that will be filtered.</param>
        /// <param name="filters">The filters expressions.</param>
        /// <returns>Filtered <see cref="IQueryable{T}" /> instance.</returns>
        public IQueryable<T> AppendQuery<T>(IQueryable<T> query, IList<FilterExpression> filters)
        {
            if (filters != null && filters.Any())
            {
                var expressionList = new List<Expression<Func<T, bool>>>();
                var groupedFilters = filters.GroupBy(p => p.PropertyName);
                foreach (var filterGroup in groupedFilters)
                {
                    expressionList.Add(Build<T>(filterGroup.ToList()));
                }

                if (expressionList.Count == 1)
                {
                    return query.Where(expressionList.First());
                }

                var filterExpressions = new List<Expression>();
                var baseParameterName = expressionList.First().Parameters[0];
                foreach (var expression in expressionList)
                {
                    filterExpressions.Add(new SwapVisitor(expression.Parameters[0], baseParameterName).Visit(expression.Body));
                }

                Expression mainExpression = null;
                while (filterExpressions.Any())
                {
                    if (mainExpression == null)
                    {
                        var e1 = filterExpressions[0];
                        var e2 = filterExpressions[1];

                        mainExpression = Expression.AndAlso(e1, e2);

                        filterExpressions.Remove(e1);
                        filterExpressions.Remove(e2);
                    }
                    else
                    {
                        var e = filterExpressions[0];
                        mainExpression = Expression.AndAlso(mainExpression, e);
                        filterExpressions.Remove(e);
                    }

                    if (filterExpressions.Count == 1)
                    {
                        mainExpression = Expression.AndAlso(mainExpression, filterExpressions[0]);
                        filterExpressions.RemoveAt(0);
                    }
                }

                var lambda = Expression.Lambda<Func<T, bool>>(mainExpression, expressionList.First().Parameters);

                return query.Where(lambda);
            }

            return query;
        }

        /// <summary>
        /// Builds lambda expression from specified filter model then appends the specified query.
        /// </summary>
        /// <typeparam name="T">Type of the instance that will be filtered.</typeparam>
        /// <param name="query"><see cref="IQueryable{T}" /> instance that will be filtered.</param>
        /// <param name="filterModel">The filter model.</param>
        /// <returns>Filtered <see cref="IQueryable{T}" /> instance.</returns>
        public IQueryable<T> AppendQuery<T>(IQueryable<T> query, FilterModel filterModel)
        {
            Check.NotNull(filterModel, "filterModel");
            Check.NotNull(query, "query");

            return AppendQuery(AppendQuery(query, filterModel.FilterExpressions.ToList()), filterModel.SortExpression,
                filterModel.SortType);
        }

        /// <summary>
        /// Builds the specified filters as lambda expression. Expression strings should be registered from
        /// <see cref="RegisterExpression" />.
        /// </summary>
        /// <typeparam name="T">Type of the instance that will be filtered.</typeparam>
        /// <param name="filters">The filters expressions.</param>
        /// <returns>A <see cref="LambdaExpression" /> that built from filter expressions.</returns>
        public Expression<Func<T, bool>> Build<T>(IList<FilterExpression> filters)
        {
            if (filters == null || !filters.Any())
            {
                throw new ArgumentException("filters");
            }

            var filterList = filters.ToList();

            var parameter = Expression.Parameter(typeof(T), GenerateRandomParameterName());
            Expression expression = null;

            var andMergeGroup = filterList.Where(p =>
                p.Operator == FilterOperation.NotEqual ||
                GetRegisteredExpression(typeof(T), p.PropertyName).Body.Type == typeof(DateTime) ||
                GetRegisteredExpression(typeof(T), p.PropertyName).Body.Type == typeof(DateTimeOffset) ||
                GetRegisteredExpression(typeof(T), p.PropertyName).Body.Type == typeof(DateTime?) ||
                GetRegisteredExpression(typeof(T), p.PropertyName).Body.Type == typeof(DateTimeOffset?)
            ).ToList();
            var orMergeGroup = filterList.Where(p => !andMergeGroup.Contains(p)).ToList();

            Expression expressionOr = null;
            if (orMergeGroup.Count == 1)
            {
                expressionOr = GetExpression<T>(parameter, orMergeGroup[0]);
            }
            else
            {
                while (orMergeGroup.Any())
                {
                    var f1 = orMergeGroup[0];
                    var f2 = orMergeGroup[1];

                    expressionOr = expressionOr == null
                        ? GetExpression<T>(parameter, f1, f2)
                        : Expression.OrElse(expressionOr, GetExpression<T>(parameter, f1, f2));

                    orMergeGroup.Remove(f1);
                    orMergeGroup.Remove(f2);

                    if (orMergeGroup.Count == 1)
                    {
                        expressionOr = Expression.OrElse(expressionOr, GetExpression<T>(parameter, orMergeGroup[0]));
                        orMergeGroup.RemoveAt(0);
                    }
                }
            }

            Expression expressionAnd = null;
            if (andMergeGroup.Count == 1)
            {
                expressionAnd = GetExpression<T>(parameter, andMergeGroup[0]);
            }
            else
            {
                while (andMergeGroup.Any())
                {
                    var f1 = andMergeGroup[0];
                    var f2 = andMergeGroup[1];

                    expressionAnd = expressionAnd == null
                        ? GetExpressionAnd<T>(parameter, f1, f2)
                        : Expression.AndAlso(expressionAnd, GetExpressionAnd<T>(parameter, f1, f2));

                    andMergeGroup.Remove(f1);
                    andMergeGroup.Remove(f2);

                    if (andMergeGroup.Count == 1)
                    {
                        expressionAnd = Expression.AndAlso(expressionAnd, GetExpression<T>(parameter, andMergeGroup[0]));
                        andMergeGroup.RemoveAt(0);
                    }
                }
            }

            if (expressionAnd != null && expressionOr != null)
            {
                expression = Expression.AndAlso(expressionAnd, expressionOr);

            }
            else if (expressionAnd != null)
            {
                expression = expressionAnd;
            }
            else
            {
                expression = expressionOr;
            }

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }

        /// <summary>
        /// Gets the property name by expression.
        /// </summary>
        /// <typeparam name="T">Type of the model that will be filtered.</typeparam>
        /// <typeparam name="TResult">The type of the expression result.</typeparam>
        /// <param name="expression">The expression.</param>
        //public string GetPropertyNameByExpression<T, TResult>(Expression<Func<T, TResult>> expression) => ReversedRegisteredExpressions[SerializeExpression(expression)];

        
        
        
        protected class ParameterUpdateVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _newParameter;
            private readonly ParameterExpression _oldParameter;

            /// <summary>
            /// Initializes a new instance of the <see cref="ParameterUpdateVisitor" /> class.
            /// </summary>
            /// <param name="oldParameter">The old parameter.</param>
            /// <param name="newParameter">The new parameter.</param>
            public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            /// <summary>
            /// Visits the <see cref="T:System.Linq.Expressions.ParameterExpression" />.
            /// </summary>
            /// <param name="node">The expression to visit.</param>
            /// <returns>The modified expression, if it or any sub expression was modified; otherwise, returns the original expression.</returns>
            protected override Expression VisitParameter(ParameterExpression node) =>
                ReferenceEquals(node, _oldParameter) ? _newParameter : base.VisitParameter(node);
        }
        
        private class SwapVisitor : ExpressionVisitor
        {
            private readonly Expression from, to;

            public SwapVisitor(Expression from, Expression to)
            {
                this.from = from;
                this.to = to;
            }

            public override Expression Visit(Expression node) => node == from ? to : base.Visit(node);
        }

    }
}