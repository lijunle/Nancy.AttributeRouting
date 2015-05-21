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
            IEnumerable<MethodBase> methods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.SafeGetTypes())
                .Where(type => !type.IsAbstract && (type.IsPublic || type.IsNestedPublic))
                .SelectMany(GetMembersWithRouteAttribute);

            foreach (MethodBase method in methods)
            {
                RouteAttribute.AddToRoutings(Routings, method);
            }
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
            INancyModule module,
            RouteBuilder builder,
            HttpMethod httpMethod)
        {
            foreach (KeyValuePair<string, MethodBase> routing in Routings[httpMethod])
            {
                string path = routing.Key;
                MethodBase method = routing.Value;

                module.Before += (context) =>
                {
                    Response response = path == context.ResolvedRoute.Description.Path
                        ? BeforeAttribute.GetResponse(method, container, context)
                        : null;

                    return response;
                };

                builder[path] = dynamicParameters =>
                {
                    // dynamicParameter is an instance of Nancy.DynamicDictionary
                    IDictionary<string, object> parameters = dynamicParameters.ToDictionary();

                    object result = ConstructResult(container, method, module, parameters);
                    object response = ConstructResponse(method, module, result);

                    return response;
                };
            }
        }

        private static IEnumerable<MethodBase> GetMembersWithRouteAttribute(Type type)
        {
            IEnumerable<MethodBase> methods = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(HasRouteAttribute);

            return methods;
        }

        private static bool HasRouteAttribute(MethodBase method)
        {
            return method.GetCustomAttributes<RouteAttribute>().Any();
        }

        private static object ConstructResponse(
            MethodBase method,
            INancyModule module,
            object result)
        {
            string viewPath = ViewAttribute.GetPath(method);
            if (!string.IsNullOrEmpty(viewPath))
            {
                return module.Negotiate.WithModel(result).WithView(viewPath);
            }

            return result;
        }

        private static object ConstructResult(
            TinyIoCContainer container,
            MethodBase method,
            INancyModule module,
            IDictionary<string, object> parameters)
        {
            object instance = container.Resolve(method.DeclaringType, new NamedParameterOverloads(parameters));

            IEnumerable<object> parameterValues =
                method.GetParameters().Select(
                    parameterInfo => ResolveMethodParameter(parameterInfo, parameters, module));

            object result = method.Invoke(instance, parameterValues.ToArray());
            return result;
        }

        private static object ResolveMethodParameter(
            ParameterInfo parameterInfo,
            IDictionary<string, object> queryParameters,
            INancyModule module)
        {
            Type paramType = parameterInfo.ParameterType;
            string paramName = parameterInfo.Name;

            if (queryParameters.ContainsKey(paramName))
            {
                // 1. resolve using Nancy route parameters
                return queryParameters[paramName];
            }
            else if (!paramType.IsPrimitive())
            {
                // 2. resolve using model binding from request data
                return module.Bind(paramType);
            }
            else
            {
                // 3. failing to missing type
                return Type.Missing;
            }
        }
    }
}
