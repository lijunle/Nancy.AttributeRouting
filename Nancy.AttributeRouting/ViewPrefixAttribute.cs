namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;

    /// <summary>
    /// The ViewPrefix attribute. It decorates on class, indicates the View attribute works with
    /// this prefix to locate paths.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ViewPrefixAttribute : Attribute
    {
        private static readonly ConcurrentDictionary<Type, string> Cache =
            new ConcurrentDictionary<Type, string>();

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

            var attr = type.GetCustomAttribute<ViewPrefixAttribute>(inherit: false);
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
