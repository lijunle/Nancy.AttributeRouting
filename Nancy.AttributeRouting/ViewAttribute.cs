namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The View attribute indicates the view path to render from request.
    /// </summary>
    /// <example>
    /// The following code will render <c>View/index.html</c> with routing instance.
    /// <code>
    /// View('View/index.html')
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method)]
    public class ViewAttribute : Attribute
    {
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewAttribute"/> class.
        /// </summary>
        /// <param name="path">The view path for rendering.</param>
        public ViewAttribute(string path)
        {
            this.path = path.TrimStart('/');
        }

        internal static string GetPath(MethodBase method)
        {
            var attr = method.GetCustomAttribute<ViewAttribute>(false);
            if (attr == null)
            {
                return string.Empty;
            }

            IEnumerable<string> prefix = ViewPrefixAttribute.GetPrefix(method.DeclaringType);
            string path = string.Join("/", prefix.Concat(new string[] { attr.path }));

            return path;
        }
    }

    /// <summary>
    /// The ViewPrefix attribute. It decorates on class, indicates the View attribute works with
    /// this prefix to locate paths.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
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
