namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The ViewPrefix attribute. It decorates on class, indicates the View attribute works with
    /// this prefix to locate paths.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewPrefixAttribute : Attribute
    {
        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewPrefixAttribute"/> class.
        /// </summary>
        /// <param name="prefix">The path prefix.</param>
        public ViewPrefixAttribute(string prefix)
        {
            this.prefix = prefix.Trim('/');
        }

        internal static IEnumerable<string> GetPrefix(Type type)
        {
            if (type == typeof(object))
            {
                return new string[0];
            }

            IEnumerable<string> basePrefix = GetPrefix(type.BaseType);

            var attr = type.GetCustomAttribute<ViewPrefixAttribute>(false);
            if (attr == null)
            {
                return basePrefix;
            }

            return basePrefix.Concat(new string[] { attr.prefix });
        }
    }
}
