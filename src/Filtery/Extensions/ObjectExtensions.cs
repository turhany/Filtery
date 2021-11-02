using System;
using System.Linq;
using System.Reflection;

namespace Filtery.Extensions
{
    internal static class ObjectExtensions
    {
        public static T GetFieldValue<T>(this object item, string fieldName)
        {
            var fieldInfo = item.GetFieldInfo(fieldName);
            return (T)fieldInfo.GetValue(item);
        }

        private static FieldInfo GetFieldInfo(this object item, string fieldName)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var type = item.GetType();

            var fieldInfo = type
                .GetFields(BindingFlags.NonPublic |
                               BindingFlags.Public |
                               BindingFlags.Instance |
                               BindingFlags.Static).FirstOrDefault(l => l.Name == fieldName);

            if (fieldInfo == null)
            {
                throw new ArgumentOutOfRangeException(fieldName);
            }

            return fieldInfo;
        }
    }
}