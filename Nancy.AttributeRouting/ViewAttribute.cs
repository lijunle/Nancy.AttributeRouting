namespace Nancy.AttributeRouting
{
    using System;
    using System.Diagnostics.CodeAnalysis;
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

            string prefix = ViewPrefixAttribute.GetPrefix(method.DeclaringType);

            return string.Format("/{0}/{1}", prefix, attr.path);
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

        internal static string GetPrefix(Type type)
        {
            var attr = type.GetCustomAttribute<ViewPrefixAttribute>();

            return attr == null ? string.Empty : attr.prefix;
        }
    }
}
