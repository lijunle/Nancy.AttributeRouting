namespace Nancy.AttributeRouting
{
    using System;
    using System.Reflection;

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
            : this(null, prefix)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewPrefixAttribute"/> class.
        /// </summary>
        /// <param name="prefixType">The prefix of this type leveraged as prefix of prefix.</param>
        /// <param name="prefix">The path prefix.</param>
        public ViewPrefixAttribute(Type prefixType, string prefix)
        {
            string typePrefix = GetPrefix(prefixType);
            string trimPrefix = prefix.Trim('/');
            this.prefix = string.Format("{0}/{1}", typePrefix, trimPrefix).Trim('/');
        }

        internal static string GetPrefix(Type type)
        {
            if (type == null || type == typeof(object))
            {
                return string.Empty;
            }

            var attr = type.GetCustomAttribute<ViewPrefixAttribute>(false);
            string prefix = attr == null ? string.Empty : attr.prefix;

            return prefix;
        }
    }
}
