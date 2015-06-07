namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;
    using System.Text;

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
            string prefix = Cache.GetOrAdd(
                type,
                t => GetPrefix(t, recursive: true).ToString().Trim('/'));

            return prefix;
        }

        private static StringBuilder GetPrefix(Type type, bool recursive)
        {
            if (type == null || type == typeof(object))
            {
                return new StringBuilder();
            }

            Type ancestorType = RouteInheritAttribute.GetAncestorType(type);
            StringBuilder builder = GetPrefix(ancestorType, recursive);

            var attr = type.GetCustomAttribute<RoutePrefixAttribute>(inherit: false);
            if (attr != null)
            {
                builder.Append('/').Append(attr.prefix);
            }

            return builder;
        }
    }
}
