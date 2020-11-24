namespace System.Collections.Generic
{
    public static class IDictionary_TKey_TValue_Extensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (!dictionary.ContainsKey(key))
            {
                return default;
            }

            return dictionary[key];
        }
    }
}
