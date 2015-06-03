namespace Nancy.AttributeRouting
{
    using System;
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
            string typePrefix = GetPrefix(prefixType);
            this.prefix = string.Format("{0}/{1}", typePrefix, prefix).Trim('/');
        }

        internal static string GetPrefix(Type type)
        {
            if (type == null || type == typeof(object))
            {
                return string.Empty;
            }

            var attr = type.GetCustomAttribute<RoutePrefixAttribute>(inherit: false);
            string prefix = attr == null ? string.Empty : attr.prefix;

            return prefix;
        }
    }
}
