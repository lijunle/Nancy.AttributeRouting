namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Concurrent;
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
        // object may be instance of BeforeAttribute or MultipleBeforeAttributeException
        private static readonly ConcurrentDictionary<MemberInfo, object> Cache =
            new ConcurrentDictionary<MemberInfo, object>();

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
            object cachedAttribute = GetAttributeFromCache(method);
            if (cachedAttribute is Exception)
            {
                throw (Exception)cachedAttribute;
            }
            else if (cachedAttribute is BeforeAttribute)
            {
                return (BeforeAttribute)cachedAttribute;
            }
            else
            {
                return null;
            }
        }

        private static object GetAttributeFromCache(MemberInfo member)
        {
            if (member == null)
            {
                return null;
            }
            else
            {
                object cachedAttribute = Cache.GetOrAdd(
                    member,
                    m => GetAttributeFromCalculation(m));

                return cachedAttribute;
            }
        }

        private static object GetAttributeFromCalculation(MemberInfo member)
        {
            // get attribute from this member
            var attributes = member.GetCustomAttributes<BeforeAttribute>(inherit: false);
            if (attributes.Count() > 1)
            {
                // do not throw exception, the caller will cache it from returned value
                return new MultipleBeforeAttributeException(member);
            }
            else if (attributes.Count() == 1)
            {
                return attributes.Single();
            }

            // get attribute from method's declaring type if applicable
            var method = member as MethodBase;
            if (method != null)
            {
                return GetAttributeFromCache(method.DeclaringType);
            }

            // get attribute from type's route ancestor type if applicable
            var type = member as Type;
            if (type != null)
            {
                Type ancestorType = RouteInheritAttribute.GetAncestorType(type);
                return GetAttributeFromCache(ancestorType);
            }

            return null;
        }
    }
}
