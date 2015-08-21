namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class AttributeRoutingTable
    {
        internal AttributeRoutingTable(ITypeProvider typeProvider)
        {
            this.Routings = new Dictionary<HttpMethod, Dictionary<string, MethodBase>>
            {
                { HttpMethod.Delete, new Dictionary<string, MethodBase>() },
                { HttpMethod.Get, new Dictionary<string, MethodBase>() },
                { HttpMethod.Options, new Dictionary<string, MethodBase>() },
                { HttpMethod.Patch, new Dictionary<string, MethodBase>() },
                { HttpMethod.Post, new Dictionary<string, MethodBase>() },
                { HttpMethod.Put, new Dictionary<string, MethodBase>() },
            };

            IEnumerable<MethodBase> methods = typeProvider.Types
                .Where(IsSupported).SelectMany(RouteAttribute.GetMethods);

            foreach (MethodBase method in methods)
            {
                this.Routings.Register(method);
            }
        }

        internal Dictionary<HttpMethod, Dictionary<string, MethodBase>> Routings { get; }

        private static bool IsSupported(Type type) =>
            type.IsInterface || (!type.IsAbstract && (type.IsPublic || type.IsNestedPublic));
    }
}
