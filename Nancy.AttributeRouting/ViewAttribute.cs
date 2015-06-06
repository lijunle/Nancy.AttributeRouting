namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
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
}
