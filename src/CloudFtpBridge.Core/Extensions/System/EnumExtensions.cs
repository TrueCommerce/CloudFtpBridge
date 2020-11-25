using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        public static string ToDisplayName(this Enum enumeration)
        {
            var type = enumeration.GetType();
            var descriptionAttribute = type.GetField(Enum.GetName(type, enumeration)).GetCustomAttribute<DescriptionAttribute>();

            if (descriptionAttribute == null)
            {
                return enumeration.ToString();
            }

            return descriptionAttribute.Description;
        }
    }
}
