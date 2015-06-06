namespace Nancy.AttributeRouting
{
    using System.Collections.Generic;
    using Nancy.Bootstrapper;

    /// <inheritdoc/>
    public class Registration : IRegistrations
    {
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
    }
}
