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

        public static IDictionary<T1, T2> Merge<T1, T2>(this IDictionary<T1, T2> origin, IDictionary<T1, T2> dictionary)
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

        public static object Bind(this INancyModule module, Type type)
        {
            MethodInfo bindMethod = BindMethod.MakeGenericMethod(type);
            return bindMethod.Invoke(null, new[] { module });
        }
    }
}
