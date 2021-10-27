using System;
using Filtery.Builders.ExpressionValueConverters.Abstract;

namespace Filtery.Builders.ExpressionValueConverters.Concrete
{
    internal class DateTimeExpressionConverter: IExpressionValueConverter
    {
        public object Convert(object value) => DateTime.Parse(value.ToString());
        public bool IsSatisfied(Type type, object value) => type == typeof(DateTime) || type == typeof(DateTimeOffset);
    }
}