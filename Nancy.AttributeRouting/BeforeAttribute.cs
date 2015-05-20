namespace Nancy.AttributeRouting
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Nancy.TinyIoc;

    /// <summary>
    /// Before attribute provides a hook to execute before enter the view model execution.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public abstract class BeforeAttribute : Attribute
    {
        /// <summary>
        /// Process the custom code and determine whether continue on view model execution.
        /// </summary>
        /// <param name="container">
        /// The Tiny IoC container. It provides <see cref="IUrlBuilder"/> and others to construct
        /// the response.
        /// </param>
        /// <param name="context">
        /// The Nancy context. It provides user information and others to determine whether continue
        /// view model execution.
        /// </param>
        /// <returns>
        /// The response. If this is <c>null</c>, it will continue on view model execution,
        /// otherwise it returns the this value directly.
        /// </returns>
        public abstract Response Process(TinyIoCContainer container, NancyContext context);

        internal static Response GetResponse(MethodBase method, TinyIoCContainer container, NancyContext context)
        {
            var attr = GetAttribute(method);
            if (attr == null)
            {
                return null;
            }

            Response response = attr.Process(container, context);
            return response;
        }

        private static BeforeAttribute GetAttribute(MethodBase method)
        {
            // 1. check the method itself
            var attr = method.GetCustomAttribute<BeforeAttribute>(inherit: false);
            if (attr != null)
            {
                return attr;
            }

            // 2. check the class which declares the method and its ancestors.
            var attrs = method.DeclaringType.GetCustomAttributes<BeforeAttribute>(inherit: true);
            if (attrs.Any())
            {
                return attrs.First();
            }

            return null;
        }
    }
}
