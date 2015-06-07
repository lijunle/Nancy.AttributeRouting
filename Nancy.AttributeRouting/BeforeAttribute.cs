namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Nancy.AttributeRouting.Exceptions;
    using Nancy.TinyIoc;

    /// <summary>
    /// Before attribute provides a hook to execute before enter the view model execution.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public abstract class BeforeAttribute : Attribute
    {
        private static readonly Dictionary<Type, BeforeAttribute> Cache =
            new Dictionary<Type, BeforeAttribute>();

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
            var methodAttributes = method.GetCustomAttributes<BeforeAttribute>(inherit: false);
            if (methodAttributes.Count() > 1)
            {
                throw new MultipleBeforeAttributeException(method);
            }

            var attribute = methodAttributes.SingleOrDefault() ?? GetAttribute(method.DeclaringType);
            return attribute;
        }

        private static BeforeAttribute GetAttribute(Type type)
        {
            if (type == null || type == typeof(object))
            {
                return null;
            }

            BeforeAttribute cachedAttribute;
            if (Cache.TryGetValue(type, out cachedAttribute))
            {
                return cachedAttribute;
            }

            var typeAttributes = type.GetCustomAttributes<BeforeAttribute>(inherit: false);
            if (typeAttributes.Count() > 1)
            {
                throw new MultipleBeforeAttributeException(type);
            }

            var attribute = typeAttributes.SingleOrDefault() ?? GetAttribute(RouteInheritAttribute.GetAncestorType(type));

            Cache.Add(type, attribute);

            return attribute;
        }
    }
}
