namespace System
{
    public static class StringExtensions
    {
        public static T ConvertTo<T>(this string str)
        {
            return (T)Convert.ChangeType(str, typeof(T));
        }
    }
}
