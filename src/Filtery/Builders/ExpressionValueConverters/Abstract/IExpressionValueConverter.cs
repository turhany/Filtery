using System;

namespace Filtery.Builders.ExpressionValueConverters.Abstract
{
    public interface IExpressionValueConverter
    {
        object Convert(object value);
        bool IsSatisfied(Type type, object value);
    }
}