using System;
using System.Collections.Generic;

namespace System.Collections.Generic
{
    public static class IDictionary_string_string_Extensions
    {
        public static T ToObject<T>(this IDictionary<string, string> dictionary, string keyPrefix = "", T instance = default)
            where T : class, new()
        {
            instance = instance ?? new T();

            foreach (var keyValuePair in dictionary)
            {
                var propertyInfo = typeof(T).GetProperty(keyValuePair.Key.Replace(keyPrefix, string.Empty));

                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(instance, Convert.ChangeType(keyValuePair.Value, propertyInfo.PropertyType));
                }
            }

            return instance;
        }
    }
}
