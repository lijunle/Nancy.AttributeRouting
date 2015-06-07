namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;

    /// <summary>
    /// The RoutePrefix attribute. It decorates on class, indicates the path from route attribute on
    /// the class and child class will be prefixed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class RoutePrefixAttribute : Attribute
    {
        private static readonly ConcurrentDictionary<Type, string> Cache =
            new ConcurrentDictionary<Type, string>();

        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutePrefixAttribute"/> class.
        /// </summary>
        /// <param name="prefix">The prefix string for the route attribute path.</param>
        public RoutePrefixAttribute(string prefix)
        {
            this.prefix = prefix;
        }

        internal static string GetPrefix(Type type)
        {
            string prefix = GetPrefixFromCache(type);
            return prefix;
        }

        private static string GetPrefixFromCache(Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }
            else
            {
                string prefix = Cache.GetOrAdd(type, t => GetPrefixFromCalculation(t));
                return prefix;
            }
        }

        private static string GetPrefixFromCalculation(Type type)
        {
            Type ancestorType = RouteInheritAttribute.GetAncestorType(type);
            string prefix = GetPrefixFromCache(ancestorType);

            var attr = type.GetCustomAttribute<RoutePrefixAttribute>(inherit: false);
            if (attr != null)
            {
                return string.Format("{0}/{1}", prefix, attr.prefix).Trim('/');
            }
            else
            {
                return prefix;
            }
        }
    }
}
