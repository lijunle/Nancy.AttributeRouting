namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Nancy.AttributeRouting.Exceptions;

    /// <summary>
    /// The Route attribute indicates the routing path to handle the request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class RouteAttribute : Attribute
    {
        private readonly HttpMethod method;
        private readonly string path;

        internal RouteAttribute(string path, HttpMethod method)
        {
            this.path = path.TrimStart('/');
            this.method = method;
        }

        internal static string GetPath(MethodBase method)
        {
            IEnumerable<RoutingPath> routingPaths = GetRoutingPaths(method);

            if (routingPaths.Count() == 0)
            {
                throw new NoRouteAttributeException(method);
            }
            else if (routingPaths.Count() > 1)
            {
                throw new MultipleRouteAttributesException(method);
            }
            else
            {
                return routingPaths.Single().Path;
            }
        }

        internal static void Register(
            Dictionary<HttpMethod, Dictionary<string, MethodBase>> routings,
            MethodBase method)
        {
            IEnumerable<RoutingPath> routingPaths = GetRoutingPaths(method);

            foreach (RoutingPath routingPath in routingPaths)
            {
                HttpMethod httpMethod = routingPath.HttpMethod;
                string path = routingPath.Path;

                if (routings[httpMethod].ContainsKey(path))
                {
                    throw new DuplicatedRoutingPathsException(routings, httpMethod, path, method);
                }

                routings[httpMethod].Add(path, method);
            }
        }

        private static IEnumerable<RoutingPath> GetRoutingPaths(MethodBase method)
        {
            string prefix = RoutePrefixAttribute.GetPrefix(method.DeclaringType);

            IEnumerable<RoutingPath> routingPaths =
                method.GetCustomAttributes<RouteAttribute>(inherit: false)
                    .Select(attribute => new RoutingPath(attribute.method, prefix, attribute.path));

            return routingPaths;
        }

        private class RoutingPath
        {
            public RoutingPath(HttpMethod httpMethod, string prefix, string path)
            {
                this.HttpMethod = httpMethod;
                this.Path = string.Format("{0}/{1}", prefix, path);
            }

            public HttpMethod HttpMethod { get; }

            public string Path { get; }
        }
    }

    /// <summary>
    /// The Delete attribute. It indicates that this method hit with HTTP DELETE method.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class DeleteAttribute : RouteAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAttribute"/> class.
        /// </summary>
        /// <param name="path">The path to register into routing table.</param>
        public DeleteAttribute(string path)
            : base(path, HttpMethod.Delete)
        {
        }
    }

    /// <summary>
    /// The Get attribute. It indicates that this method hit with HTTP GET method.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class GetAttribute : RouteAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAttribute"/> class.
        /// </summary>
        /// <param name="path">The path to register into routing table.</param>
        public GetAttribute(string path)
            : base(path, HttpMethod.Get)
        {
        }
    }

    /// <summary>
    /// The Options attribute. It indicates that this method hit with HTTP OPTIONS method.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class OptionsAttribute : RouteAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsAttribute"/> class.
        /// </summary>
        /// <param name="path">The path to register into routing table.</param>
        public OptionsAttribute(string path)
            : base(path, HttpMethod.Options)
        {
        }
    }

    /// <summary>
    /// The Patch attribute. It indicates that this method hit with HTTP PATCH method.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class PatchAttribute : RouteAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatchAttribute"/> class.
        /// </summary>
        /// <param name="path">The path to register into routing table.</param>
        public PatchAttribute(string path)
            : base(path, HttpMethod.Patch)
        {
        }
    }

    /// <summary>
    /// The Post attribute. It indicates that this method hit with HTTP POST method.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class PostAttribute : RouteAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostAttribute"/> class.
        /// </summary>
        /// <param name="path">The path to register into routing table.</param>
        public PostAttribute(string path)
            : base(path, HttpMethod.Post)
        {
        }
    }

    /// <summary>
    /// The Put attribute. It indicates that this method hit with HTTP PUT method.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class PutAttribute : RouteAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutAttribute"/> class.
        /// </summary>
        /// <param name="path">The path to register into routing table.</param>
        public PutAttribute(string path)
            : base(path, HttpMethod.Put)
        {
        }
    }
}
