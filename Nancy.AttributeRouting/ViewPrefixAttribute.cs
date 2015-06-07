namespace Nancy.AttributeRouting
{
    using System;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// The ViewPrefix attribute. It decorates on class, indicates the View attribute works with
    /// this prefix to locate paths.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ViewPrefixAttribute : Attribute
    {
        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewPrefixAttribute"/> class.
        /// </summary>
        /// <param name="prefix">The path prefix.</param>
        public ViewPrefixAttribute(string prefix)
        {
            this.prefix = prefix;
        }

        internal static StringBuilder GetPrefix(Type type)
        {
            if (type == null || type == typeof(object))
            {
                return new StringBuilder();
            }

            Type ancestorType = RouteInheritAttribute.GetAncestorType(type);
            StringBuilder builder = GetPrefix(ancestorType);

            var attr = type.GetCustomAttribute<ViewPrefixAttribute>(inherit: false);
            if (attr != null)
            {
                builder.Append('/').Append(attr.prefix);
            }

            return builder;
        }
    }
}
