namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    public class ViewAttribute : Attribute
    {
        private readonly string path;

        public ViewAttribute(string path)
        {
            this.path = path.TrimStart('/');
        }

        internal static string GetPath(MethodBase method)
        {
            var attr = method.GetCustomAttribute<ViewAttribute>();
            if (attr == null)
            {
                return string.Empty;
            }

            IEnumerable<string> prefix = ViewPrefixAttribute.GetPrefix(method.DeclaringType);

            return string.Join("/", prefix.Concat(new string[] { attr.path }));
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewPrefixAttribute : Attribute
    {
        private readonly string prefix;

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
