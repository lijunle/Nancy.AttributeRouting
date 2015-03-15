namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Nancy.ModelBinding;
    using Nancy.Responses.Negotiation;
    using Nancy.TinyIoc;

    public class AttributeRoutingResolver : NancyModule
    {
        private static readonly PropertyInfo[] NancyModuleProperties =
            typeof(NancyModule).GetProperties();

        private static readonly MethodInfo BindMethod =
            typeof(ModuleExtensions).GetMethod("Bind", new Type[] { typeof(INancyModule) });

        private static readonly Dictionary<HttpMethod, Dictionary<string, MethodBase>> Routings =
            new Dictionary<HttpMethod, Dictionary<string, MethodBase>>
            {
                { HttpMethod.Delete, new Dictionary<string, MethodBase>() },
                { HttpMethod.Get, new Dictionary<string, MethodBase>() },
                { HttpMethod.Options, new Dictionary<string, MethodBase>() },
                { HttpMethod.Patch, new Dictionary<string, MethodBase>() },
                { HttpMethod.Post, new Dictionary<string, MethodBase>() },
                { HttpMethod.Put, new Dictionary<string, MethodBase>() },
            };

        static AttributeRoutingResolver()
        {
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.SafeGetTypes())
                .Where(type => !type.IsAbstract && (type.IsPublic || type.IsNestedPublic))
                .SelectMany(GetMembersWithRouteAttribute)
                .ToList()
                .ForEach(AddToRouting);
        }

        public AttributeRoutingResolver(TinyIoCContainer container)
        {
            var negotiator = new Lazy<Negotiator>(() => this.Negotiate);
            Resolve(container, negotiator, this, this.Delete, HttpMethod.Delete);
            Resolve(container, negotiator, this, this.Get, HttpMethod.Get);
            Resolve(container, negotiator, this, this.Options, HttpMethod.Options);
            Resolve(container, negotiator, this, this.Patch, HttpMethod.Patch);
            Resolve(container, negotiator, this, this.Post, HttpMethod.Post);
            Resolve(container, negotiator, this, this.Put, HttpMethod.Put);
        }

        private static void Resolve(
            TinyIoCContainer container,
            Lazy<Negotiator> negotiator,
            NancyModule module,
            RouteBuilder builder,
            HttpMethod httpMethod)
        {
            foreach (KeyValuePair<string, MethodBase> routing in Routings[httpMethod])
            {
                string path = routing.Key;
                MethodBase member = routing.Value;
                Type type = member.DeclaringType;
                string viewPath = ViewAttribute.GetPath(member);

                if (member is ConstructorInfo || member is MethodInfo)
                {
                    builder[path] = parameters =>
                    {
                        object instance = ContainerResolve(container, type, parameters);
                        object result = member is MethodInfo
                            ? MethodInvoke(instance, member, parameters, container, module)
                            : instance;

                        if (!string.IsNullOrEmpty(viewPath))
                        {
                            return negotiator.Value.WithModel(result).WithView(viewPath);
                        }

                        return result;
                    };
                }
                else
                {
                    string errorMessage = string.Format(
                        "Routing attribute is not allowed to decorated on {0} of type {1}.",
                        member.Name,
                        type.FullName);

                    throw new InvalidOperationException(errorMessage);
                }
            }
        }

        private static void AddToRouting(MethodBase member)
        {
            member.GetCustomAttributes<RouteAttribute>().ToList()
                .ForEach(attr => AddToRouting(attr.Method, attr.Path, member));
        }

        private static void AddToRouting(HttpMethod method, string path, MethodBase member)
        {
            if (Routings[method].ContainsKey(path))
            {
                string message = string.Format(
                    "Attribute path {0} registered on two members, {1}.{2} and {3}.{4}.",
                    path,
                    Routings[method][path].DeclaringType.FullName,
                    Routings[method][path].Name,
                    member.DeclaringType.FullName,
                    member.Name);

                throw new Exception(message);
            }

            Routings[method].Add(path, member);
        }

        private static IEnumerable<MethodBase> GetMembersWithRouteAttribute(Type type)
        {
            IEnumerable<MethodBase> ctors = type
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(HasRouteAttribute);

            IEnumerable<MethodBase> methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(HasRouteAttribute);

            return ctors.Union(methods);
        }

        private static bool HasRouteAttribute(MethodBase member)
        {
            return member.GetCustomAttributes<RouteAttribute>().Count() != 0;
        }

        private static object ContainerResolve(TinyIoCContainer container, Type type, DynamicDictionary nancyDictionary)
        {
            var data = new Dictionary<string, object>();

            foreach (string key in nancyDictionary.Keys)
            {
                data.Add(key, nancyDictionary[key].Value);
            }

            return container.Resolve(type, new NamedParameterOverloads(data));
        }

        private static object MethodInvoke(
            object instance,
            MethodBase method,
            DynamicDictionary nancyDictionary,
            TinyIoCContainer container,
            NancyModule module)
        {
            IEnumerable<object> parameters =
                ResolveMethodParameters(method, nancyDictionary, container, module);

            return method.Invoke(instance, parameters.ToArray());
        }

        private static IEnumerable<object> ResolveMethodParameters(
            MethodBase method,
            DynamicDictionary nancyDictionary,
            TinyIoCContainer container,
            NancyModule module)
        {
            IEnumerable<object> parameters = method.GetParameters().Select(parameterInfo =>
            {
                Type paramType = parameterInfo.ParameterType;
                string paramName = parameterInfo.Name;

                var isPrimitive = new Lazy<bool>(() => IsPrimitive(paramType));

                var moduleProperty = new Lazy<PropertyInfo>(
                    () => NancyModuleProperties.FirstOrDefault(
                        p => p.PropertyType == paramType && p.CanRead));

                if (nancyDictionary.ContainsKey(paramName))
                {
                    // 1. resolve using Nancy route parameters
                    return nancyDictionary[paramName].Value;
                }
                else if (!isPrimitive.Value && moduleProperty.Value != null)
                {
                    // 2. resolve using NancyModule property value
                    return moduleProperty.Value.GetValue(module);
                }
                else if (!isPrimitive.Value)
                {
                    // 3. resolve using model binding from request data
                    MethodInfo bindMethod = BindMethod.MakeGenericMethod(paramType);
                    return bindMethod.Invoke(null, new[] { module });
                }
                else
                {
                    // 4. failing to missing type
                    return Type.Missing;
                }
            });

            return parameters;
        }

        private static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive ||
                type == typeof(string) ||
                type == typeof(Guid) ||
                type == typeof(DateTime) ||
                type == typeof(TimeSpan);
        }
    }
}
