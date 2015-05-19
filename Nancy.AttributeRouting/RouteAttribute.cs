﻿namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The Route attribute indicates the routing path to handle the request.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true)]
    public abstract class RouteAttribute : Attribute
    {
        private readonly HttpMethod method;
        private readonly string path;

        internal RouteAttribute(string path, HttpMethod method)
        {
            this.path = path.TrimStart('/');
            this.method = method;
        }

        internal static void AddToRoutings(
            Dictionary<HttpMethod, Dictionary<string, MethodBase>> routings,
            MethodBase member)
        {
            IEnumerable<RouteAttribute> attrs = member.GetCustomAttributes<RouteAttribute>();
            foreach (RouteAttribute attr in attrs)
            {
                AddToRoutings(routings, attr, member);
            }
        }

        internal static string GetPath(MethodBase method)
        {
            RouteAttribute attr;

            try
            {
                attr = method.GetCustomAttribute<RouteAttribute>(false);
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

            IEnumerable<string> prefix = RoutePrefixAttribute.GetPrefix(method.DeclaringType);
            string path = string.Join("/", prefix.Concat(new string[] { attr.path }));

            return path;
        }

        private static void AddToRoutings(
            Dictionary<HttpMethod, Dictionary<string, MethodBase>> routings,
            RouteAttribute attribute,
            MethodBase member)
        {
            HttpMethod method = attribute.method;
            IEnumerable<string> prefix = RoutePrefixAttribute.GetPrefix(member.DeclaringType);
            string path = string.Join("/", prefix.Concat(new string[] { attribute.path }));

            if (routings[method].ContainsKey(path))
            {
                string message = string.Format(
                    "Attribute path {0} registered on two members, {1}.{2} and {3}.{4}.",
                    path,
                    routings[method][path].DeclaringType.FullName,
                    routings[method][path].Name,
                    member.DeclaringType.FullName,
                    member.Name);

                throw new Exception(message);
            }

            routings[method].Add(path, member);
        }

        private static string JoinPrefixAndPath(IEnumerable<string> prefix, string path)
        {
            return string.Join("/", prefix.Concat(new string[] { path }));
        }
    }

    /// <summary>
    /// The RoutePrefix attribute. It decorates on class, indicates the path from route attribute on
    /// the class and child class will be prefixed.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [AttributeUsage(AttributeTargets.Class)]
    public class RoutePrefixAttribute : Attribute
    {
        private readonly string prefix;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutePrefixAttribute"/> class.
        /// </summary>
        /// <param name="prefix">The string to prefix to the route attribute path.</param>
        public RoutePrefixAttribute(string prefix)
        {
            this.prefix = prefix.Trim('/');
        }

        internal static IEnumerable<string> GetPrefix(Type type)
        {
            if (type == typeof(object))
            {
                return new string[] { string.Empty };
            }

            IEnumerable<string> prefix = GetPrefix(type.BaseType);
            var attr = type.GetCustomAttribute<RoutePrefixAttribute>(false);
            if (attr == null)
            {
                return prefix;
            }

            return prefix.Concat(new string[] { attr.prefix });
        }
    }

    /// <summary>
    /// The Delete attribute. It indicates that this member hit with HTTP DELETE method.
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
    /// The Get attribute. It indicates that this member hit with HTTP GET method.
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
    /// The Options attribute. It indicates that this member hit with HTTP OPTIONS method.
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
    /// The Patch attribute. It indicates that this member hit with HTTP PATCH method.
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
    /// The Post attribute. It indicates that this member hit with HTTP POST method.
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
    /// The Put attribute. It indicates that this member hit with HTTP PUT method.
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
