namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using Nancy.ModelBinding;

    internal static class Extensions
    {
        private static readonly Dictionary<Type, PropertyInfo> NancyModulePropertiesLookup =
            ConstructNancyModulePropertiesLookup();

        private static readonly MethodInfo BindMethod =
            typeof(ModuleExtensions).GetMethod("Bind", new Type[] { typeof(INancyModule) });

        public static bool IsPrimitive(this Type type)
        {
            return type.IsPrimitive ||
                type == typeof(string) ||
                type == typeof(Guid) ||
                type == typeof(DateTime) ||
                type == typeof(TimeSpan);
        }

        public static IDictionary<string, string> ToDictionary(this object thisObject)
        {
            return TypeDescriptor.GetProperties(thisObject)
                .OfType<PropertyDescriptor>()
                .ToDictionary(p => p.Name, p => Convert.ToString(p.GetValue(thisObject)));
        }

        public static Dictionary<T1, T2> Merge<T1, T2>(this IDictionary<T1, T2> origin, IDictionary<T1, T2> dictionary)
        {
            var result = new Dictionary<T1, T2>(origin);

            foreach (KeyValuePair<T1, T2> kvp in dictionary)
            {
                T1 key = kvp.Key;
                T2 value = kvp.Value;

                if (!result.ContainsKey(key))
                {
                    result[key] = value;
                }
            }

            return result;
        }

        public static bool HasProperty(this NancyModule module, Type type)
        {
            bool result = NancyModulePropertiesLookup.ContainsKey(type);
            return result;
        }

        public static object GetProperty(this NancyModule module, Type type)
        {
            PropertyInfo propertyInfo = NancyModulePropertiesLookup[type];
            object property = propertyInfo.GetValue(module);
            return property;
        }

        public static object Bind(this NancyModule module, Type type)
        {
            MethodInfo bindMethod = BindMethod.MakeGenericMethod(type);
            return bindMethod.Invoke(null, new[] { module });
        }

        private static Dictionary<Type, PropertyInfo> ConstructNancyModulePropertiesLookup()
        {
            IEnumerable<PropertyInfo> properties = typeof(NancyModule).GetProperties().Where(p => p.CanRead);

            // avoid using LINQ here due to duplicated keys
            var lookup = new Dictionary<Type, PropertyInfo>();
            foreach (PropertyInfo propertyInfo in properties)
            {
                lookup[propertyInfo.PropertyType] = propertyInfo;
            }

            return lookup;
        }
    }
}
