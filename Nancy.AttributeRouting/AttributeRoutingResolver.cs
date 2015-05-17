namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Nancy.TinyIoc;

    /// <summary>
    /// The class to resolve routing attributes.
    /// </summary>
    public class AttributeRoutingResolver : NancyModule
    {
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
                .ForEach(method => RouteAttribute.AddToRoutings(Routings, method));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeRoutingResolver"/> class.
        /// </summary>
        /// <param name="container">The Nancy IoC container.</param>
        public AttributeRoutingResolver(TinyIoCContainer container)
        {
            Resolve(container, this, this.Delete, HttpMethod.Delete);
            Resolve(container, this, this.Get, HttpMethod.Get);
            Resolve(container, this, this.Options, HttpMethod.Options);
            Resolve(container, this, this.Patch, HttpMethod.Patch);
            Resolve(container, this, this.Post, HttpMethod.Post);
            Resolve(container, this, this.Put, HttpMethod.Put);
        }

        private static void Resolve(
            TinyIoCContainer container,
            NancyModule module,
            RouteBuilder builder,
            HttpMethod httpMethod)
        {
            foreach (KeyValuePair<string, MethodBase> routing in Routings[httpMethod])
            {
                string path = routing.Key;
                MethodBase member = routing.Value;

                builder[path] = dynamicParameters =>
                {
                    Dictionary<string, object> parameters = dynamicParameters.ToDictionary();
                    object instance = ConstructInstance(container, member, parameters);
                    object result = MethodInvoke(instance, member, parameters, module);

                    string viewPath = ViewAttribute.GetPath(member);
                    if (!string.IsNullOrEmpty(viewPath))
                    {
                        return module.Negotiate.WithModel(result).WithView(viewPath);
                    }

                    return result;
                };
            }
        }

        private static IEnumerable<MethodBase> GetMembersWithRouteAttribute(Type type)
        {
            IEnumerable<MethodBase> ctors = type
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(HasRouteAttribute);

            IEnumerable<MethodBase> methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(HasRouteAttribute);

            return ctors.Concat(methods);
        }

        private static bool HasRouteAttribute(MethodBase member)
        {
            return member.GetCustomAttributes<RouteAttribute>().Any();
        }

        private static object ConstructInstance(TinyIoCContainer container, MethodBase member, Dictionary<string, object> parameters)
        {
            var ctor = member as ConstructorInfo;
            if (ctor != null)
            {
                Dictionary<string, object> defaultParameters =
                    ctor.GetParameters().Where(p => p.HasDefaultValue).ToDictionary(p => p.Name, p => p.DefaultValue);

                parameters = parameters.Merge(defaultParameters);
            }

            return container.Resolve(member.DeclaringType, new NamedParameterOverloads(parameters));
        }

        private static object MethodInvoke(
            object instance,
            MethodBase method,
            Dictionary<string, object> queryParameters,
            NancyModule module)
        {
            if (method is ConstructorInfo)
            {
                // the associated member is a constructor, no need to invoke it.
                return instance;
            }

            IEnumerable<object> parameters =
                ResolveMethodParameters(method, queryParameters, module);

            return method.Invoke(instance, parameters.ToArray());
        }

        private static IEnumerable<object> ResolveMethodParameters(
            MethodBase method,
            Dictionary<string, object> queryParameters,
            NancyModule module)
        {
            IEnumerable<object> parameters =
                method.GetParameters()
                    .Select(parameterInfo => ResolveMethodParameter(parameterInfo, queryParameters, module));

            return parameters;
        }

        private static object ResolveMethodParameter(
            ParameterInfo parameterInfo,
            Dictionary<string, object> queryParameters,
            NancyModule module)
        {
            Type paramType = parameterInfo.ParameterType;
            string paramName = parameterInfo.Name;

            if (queryParameters.ContainsKey(paramName))
            {
                // 1. resolve using Nancy route parameters
                return queryParameters[paramName];
            }
            else if (!paramType.IsPrimitive() && module.HasProperty(paramType))
            {
                // 2. resolve using NancyModule property value
                return module.GetProperty(paramType);
            }
            else if (!paramType.IsPrimitive())
            {
                // 3. resolve using model binding from request data
                return module.Bind(paramType);
            }
            else
            {
                // 4. failing to missing type
                return Type.Missing;
            }
        }
    }
}
