using System.Collections.Generic;
using System.Reflection;

namespace System
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, string> ToStringDictionary<T>(this T obj, string keyPrefix = "", Dictionary<string, string> dictionary = null)
        {
            dictionary = dictionary ?? new Dictionary<string, string>();
            var propertyInfos = obj.GetType().GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                dictionary[$"{keyPrefix}{propertyInfo.Name}"] = propertyInfo.GetValue(obj)?.ToString();
            }

            return dictionary;
        }
    }
}
