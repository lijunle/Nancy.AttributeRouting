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
            get
            {
                return typeRegistrations.Concat(interfaceRegistrations);
            }
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

        private static bool DoesSupportType(Type type)
        {
            if (type.IsInterface)
            {
                return true;
            }
            else if (!type.IsAbstract && (type.IsPublic || type.IsNestedPublic))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsInterfaceHavingMethodsWithRouteAttribute(Type type)
        {
            return type.IsInterface && GetMethodsWithRouteAttribute(type).Any();
        }

        private static TypeRegistration GetTypeRegistration(IEnumerable<Type> allTypes, Type @interface)
        {
            IEnumerable<Type> implementations =
                allTypes.Where(type => type.GetInterface(@interface.FullName) != null);

            if (implementations.Count() > 1)
            {
                IEnumerable<string> implementationNames = implementations.Select(type => type.FullName);

                var message =
                    string.Format(
                        "More than one class implements interface {0}:{1}{2}",
                        @interface.FullName,
                        Environment.NewLine,
                        string.Join(Environment.NewLine, implementationNames));

                throw new AmbiguousMatchException(message);
            }
            else if (implementations.Count() == 1)
            {
                return new TypeRegistration(@interface, implementations.Single(), Lifetime.PerRequest);
            }
            else
            {
                return null;
            }
        }
    }
}
