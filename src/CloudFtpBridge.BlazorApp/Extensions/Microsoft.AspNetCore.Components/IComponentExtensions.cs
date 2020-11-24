using System;
using System.Linq;
using System.Reflection;

using Microsoft.AspNetCore.WebUtilities;

namespace Microsoft.AspNetCore.Components
{
    public static class IComponentExtensions
    {
        public static void BindQueryParameters(this IComponent component, NavigationManager navigationManager)
        {
            var propertyInfos = component.GetType()
                .GetProperties(BindingFlags.Public)
                .Where(pi => pi.GetCustomAttribute<ParameterAttribute>() != null)
                .ToArray();

            var queryString = new Uri(navigationManager.Uri).Query;
            var parsedQueryString = QueryHelpers.ParseQuery(queryString);

            foreach (var keyValuePair in parsedQueryString)
            {
                var matchingProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase));

                if (matchingProperty != null)
                {
                    matchingProperty.SetValue(component, Convert.ChangeType(keyValuePair.Value, matchingProperty.PropertyType));
                }
            }
        }
    }
}
