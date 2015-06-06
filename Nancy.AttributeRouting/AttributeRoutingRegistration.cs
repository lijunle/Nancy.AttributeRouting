namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Nancy.Bootstrapper;
    using Nancy.TinyIoc;

    /// <inheritdoc/>
    public class AttributeRoutingRegistration : IRegistrations
    {
        static AttributeRoutingRegistration()
        {
            IEnumerable<MethodBase> methods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.SafeGetTypes())
                .Where(type => !type.IsAbstract && (type.IsPublic || type.IsNestedPublic))
                .SelectMany(GetMethodsWithRouteAttribute);

            foreach (MethodBase method in methods)
            {
                AttributeRoutingResolver.Routings.Register(method);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations
        {
            get { return null; }
        }

        /// <inheritdoc/>
        public IEnumerable<InstanceRegistration> InstanceRegistrations
        {
            get { return null; }
        }

        /// <inheritdoc/>
        public IEnumerable<TypeRegistration> TypeRegistrations
        {
            get { yield return new TypeRegistration(typeof(IUrlBuilder), typeof(UrlBuilder)); }
        }

        private static IEnumerable<MethodBase> GetMethodsWithRouteAttribute(Type type)
        {
            IEnumerable<MethodBase> methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(HasRouteAttribute);

            return methods;
        }

        private static bool HasRouteAttribute(MethodBase method)
        {
            return method.GetCustomAttributes<RouteAttribute>().Any();
        }
    }
}
