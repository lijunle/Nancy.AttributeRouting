namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

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
            member.GetCustomAttributes<RouteAttribute>().ToList()
                .ForEach(attr => AddToRoutings(routings, attr, member));
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
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [AttributeUsage(AttributeTargets.Class)]
    public class RoutePrefixAttribute : Attribute
    {
        private readonly string prefix;

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

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class DeleteAttribute : RouteAttribute
    {
        public DeleteAttribute(string path)
            : base(path, HttpMethod.Delete)
        {
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class GetAttribute : RouteAttribute
    {
        public GetAttribute(string path)
            : base(path, HttpMethod.Get)
        {
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class OptionsAttribute : RouteAttribute
    {
        public OptionsAttribute(string path)
            : base(path, HttpMethod.Options)
        {
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class PatchAttribute : RouteAttribute
    {
        public PatchAttribute(string path)
            : base(path, HttpMethod.Patch)
        {
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class PostAttribute : RouteAttribute
    {
        public PostAttribute(string path)
            : base(path, HttpMethod.Post)
        {
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class PutAttribute : RouteAttribute
    {
        public PutAttribute(string path)
            : base(path, HttpMethod.Put)
        {
        }
    }
}
