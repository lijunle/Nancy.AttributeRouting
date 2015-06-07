namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// The ViewPrefix attribute. It decorates on class, indicates the View attribute works with
    /// this prefix to locate paths.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ViewPrefixAttribute : Attribute
    {
        private static readonly Dictionary<Type, string> Cache = new Dictionary<Type, string>();

        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewPrefixAttribute"/> class.
        /// </summary>
        /// <param name="prefix">The path prefix.</param>
        public ViewPrefixAttribute(string prefix)
        {
            this.prefix = prefix;
        }

        internal static string GetPrefix(Type type)
        {
            string cachedPrefix;
            if (Cache.TryGetValue(type, out cachedPrefix))
            {
                return cachedPrefix;
            }

            string prefix = GetPrefix(type, recursive: true).ToString().Trim('/');

            Cache.Add(type, prefix);

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

            var attr = type.GetCustomAttribute<ViewPrefixAttribute>(inherit: false);
            if (attr != null)
            {
                builder.Append('/').Append(attr.prefix);
            }

            return builder;
        }
    }
}
