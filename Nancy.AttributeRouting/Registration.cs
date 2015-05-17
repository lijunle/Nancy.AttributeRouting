namespace Nancy.AttributeRouting
{
    using System.Collections.Generic;
    using Nancy.Bootstrapper;

    /// <summary>
    /// Register URL builder to Nancy IoC Container.
    /// </summary>
    public class Registration : IRegistrations
    {
        /// <summary>
        /// Gets the collection registrations to register into Nancy IoC container.
        /// </summary>
        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the instance registrations to register into Nancy IoC container.
        /// </summary>
        public IEnumerable<InstanceRegistration> InstanceRegistrations
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the types to register into Nancy IoC container.
        /// </summary>
        public IEnumerable<TypeRegistration> TypeRegistrations
        {
            get { yield return new TypeRegistration(typeof(IUrlBuilder), typeof(UrlBuilder)); }
        }
    }
}
