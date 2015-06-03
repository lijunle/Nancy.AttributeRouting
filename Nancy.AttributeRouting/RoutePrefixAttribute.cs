namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The RoutePrefix attribute. It decorates on class, indicates the path from route attribute on
    /// the class and child class will be prefixed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RoutePrefixAttribute : Attribute
    {
        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutePrefixAttribute"/> class.
        /// </summary>
        /// <param name="prefix">The prefix string for the route attribute path.</param>
        public RoutePrefixAttribute(string prefix)
            : this(null, prefix)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutePrefixAttribute"/> class.
        /// </summary>
        /// <param name="prefixType">The prefix of this type leveraged as prefix of prefix.</param>
        /// <param name="prefix">The prefix string for the route attribute path.</param>
        public RoutePrefixAttribute(Type prefixType, string prefix)
        {
            IEnumerable<string> typePrefix = GetPrefix(prefixType);
            IEnumerable<string> prefixes = typePrefix.Concat(new string[] { prefix.Trim('/') });
            this.prefix = string.Join("/", prefixes);
        }

        internal static IEnumerable<string> GetPrefix(Type type)
        {
            if (type == null || type == typeof(object))
            {
                return new string[] { string.Empty };
            }

            IEnumerable<string> prefix = GetPrefix(type.BaseType);
            var attr = type.GetCustomAttribute<RoutePrefixAttribute>(inherit: false);
            if (attr == null)
            {
                return prefix;
            }

            return prefix.Concat(new string[] { attr.prefix });
        }
    }
}
