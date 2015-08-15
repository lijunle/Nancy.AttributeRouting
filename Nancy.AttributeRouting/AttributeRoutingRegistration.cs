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
        private static TypeRegistration[] typeRegistrations = new[]
        {
            new TypeRegistration(typeof(IUrlBuilder), typeof(UrlBuilder))
        };

        private static IEnumerable<TypeRegistration> interfaceRegistrations;

        static AttributeRoutingRegistration()
        {
            IEnumerable<Type> allTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.SafeGetTypes());

            IEnumerable<MethodBase> methods =
                allTypes.Where(DoesSupportType).SelectMany(GetMethodsWithRouteAttribute);

            foreach (MethodBase method in methods)
            {
                AttributeRoutingResolver.Routings.Register(method);
            }

            // Prepare the interface to implementation registration mapping for IoC
            interfaceRegistrations =
                allTypes.Where(IsInterfaceHavingMethodsWithRouteAttribute)
                    .Select(@interface => GetTypeRegistration(allTypes, @interface))
                    .Where(typeRegistration => typeRegistration != null);
        }

        /// <inheritdoc/>
        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations => null;

        /// <inheritdoc/>
        public IEnumerable<InstanceRegistration> InstanceRegistrations => null;

        /// <inheritdoc/>
        public IEnumerable<TypeRegistration> TypeRegistrations => typeRegistrations.Concat(interfaceRegistrations);

        private static IEnumerable<MethodBase> GetMethodsWithRouteAttribute(Type type) =>
            type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(HasRouteAttribute);

        private static bool HasRouteAttribute(MethodBase method) =>
            method.GetCustomAttributes<RouteAttribute>().Any();

        private static bool DoesSupportType(Type type) =>
            type.IsInterface || (!type.IsAbstract && (type.IsPublic || type.IsNestedPublic));

        private static bool IsInterfaceHavingMethodsWithRouteAttribute(Type type) =>
            type.IsInterface && GetMethodsWithRouteAttribute(type).Any();

        private static TypeRegistration GetTypeRegistration(IEnumerable<Type> allTypes, Type @interface)
        {
            Type implementation =
                allTypes.FirstOrDefault(type => type.GetInterface(@interface.FullName) != null);

            if (implementation != null)
            {
                return new TypeRegistration(@interface, implementation, Lifetime.PerRequest);
            }
            else
            {
                return null;
            }
        }
    }
}
