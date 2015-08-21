namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bootstrapper;

    /// <inheritdoc/>
    public class AttributeRoutingRegistration : IRegistrations
    {
        private static TypeRegistration[] typeRegistrations = new[]
        {
            new TypeRegistration(typeof(IUrlBuilder), typeof(UrlBuilder))
        };

        private readonly AttributeRoutingTable attributeRoutingTable;

        private readonly IEnumerable<TypeRegistration> interfaceRegistrations;

        private readonly IEnumerable<Type> types;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeRoutingRegistration"/> class.
        /// </summary>
        /// <param name="typeProvider">The routing type provider.</param>
        public AttributeRoutingRegistration(ITypeProvider typeProvider)
        {
            this.types = typeProvider.Types;
            this.attributeRoutingTable = new AttributeRoutingTable(typeProvider);

            this.interfaceRegistrations = this.types
                .Where(IsRoutingInterface)
                .Select(this.ToInterfaceClassPair)
                .Where(x => x.Item2 != null)
                .Select(this.ToTypeRegistration);
        }

        /// <inheritdoc/>
        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations =>
            null;

        /// <inheritdoc/>
        public IEnumerable<InstanceRegistration> InstanceRegistrations =>
            new[]
            {
                new InstanceRegistration(typeof(AttributeRoutingTable), this.attributeRoutingTable)
            };

        /// <inheritdoc/>
        public IEnumerable<TypeRegistration> TypeRegistrations =>
            typeRegistrations.Concat(this.interfaceRegistrations);

        private static bool IsRoutingInterface(Type type) =>
            type.IsInterface && RouteAttribute.GetMethods(type).Any();

        private Tuple<Type, Type> ToInterfaceClassPair(Type @interface) =>
            Tuple.Create(
                @interface,
                this.types.FirstOrDefault(type => type.GetInterface(@interface.FullName) != null));

        private TypeRegistration ToTypeRegistration(Tuple<Type, Type> interfaceClassPair) =>
            new TypeRegistration(interfaceClassPair.Item1, interfaceClassPair.Item2, Lifetime.PerRequest);
    }
}
