namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

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
            RouteAttribute attr;

            try
            {
                attr = method.GetCustomAttribute<RouteAttribute>(inherit: false);
            }
            catch (AmbiguousMatchException e)
            {
                string message = string.Format(
                    "Method {0} associates with more than one route attributes.",
                    method.Name);

                throw new Exception(message, e);
            }

            if (attr == null)
            {
                string message = string.Format(
                    "Does not find any route attribute with method {0}.",
                    method.Name);

                throw new Exception(message);
            }

            string prefix = RoutePrefixAttribute.GetPrefix(method.DeclaringType);
            string path = string.Format("/{0}/{1}", prefix, attr.path);

            return path;
        }

        internal static void Register(
            Dictionary<HttpMethod, Dictionary<string, MethodBase>> routings,
            RouteAttribute attribute,
            MethodBase method)
        {
            HttpMethod httpMethod = attribute.method;
            string prefix = RoutePrefixAttribute.GetPrefix(method.DeclaringType);
            string path = string.Format("/{0}/{1}", prefix, attribute.path);

            if (routings[httpMethod].ContainsKey(path))
            {
                string message = string.Format(
                    "Attribute path {0} registered on two members, {1}.{2} and {3}.{4}.",
                    path,
                    routings[httpMethod][path].DeclaringType.FullName,
                    routings[httpMethod][path].Name,
                    method.DeclaringType.FullName,
                    method.Name);

                throw new Exception(message);
            }

            routings[httpMethod].Add(path, method);
        }

        private static string JoinPrefixAndPath(IEnumerable<string> prefix, string path)
        {
            return string.Join("/", prefix.Concat(new string[] { path }));
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
