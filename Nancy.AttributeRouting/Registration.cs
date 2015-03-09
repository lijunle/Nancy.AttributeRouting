namespace Nancy.AttributeRouting
{
    using System.Collections.Generic;
    using Nancy.Bootstrapper;

    public class Registration : IRegistrations
    {
        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations
        {
            get { return null; }
        }

        public IEnumerable<InstanceRegistration> InstanceRegistrations
        {
            get { return null; }
        }

        public IEnumerable<TypeRegistration> TypeRegistrations
        {
            get { yield return new TypeRegistration(typeof(IUrlBuilder), typeof(UrlBuilder)); }
        }
    }
}
