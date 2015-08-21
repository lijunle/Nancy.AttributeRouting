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
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeRoutingResolver"/> class.
        /// </summary>
        /// <param name="container">The Nancy IoC container.</param>
        public AttributeRoutingResolver(TinyIoCContainer container)
        {
            var table = container.Resolve<AttributeRoutingTable>();
            Resolve(container, this, table.Routings, this.Delete, HttpMethod.Delete);
            Resolve(container, this, table.Routings, this.Get, HttpMethod.Get);
            Resolve(container, this, table.Routings, this.Options, HttpMethod.Options);
            Resolve(container, this, table.Routings, this.Patch, HttpMethod.Patch);
            Resolve(container, this, table.Routings, this.Post, HttpMethod.Post);
            Resolve(container, this, table.Routings, this.Put, HttpMethod.Put);
        }

        private static void Resolve(
            TinyIoCContainer container,
            INancyModule module,
            Dictionary<HttpMethod, Dictionary<string, MethodBase>> routings,
            RouteBuilder builder,
            HttpMethod httpMethod)
        {
            foreach (KeyValuePair<string, MethodBase> routing in routings[httpMethod])
            {
                string path = routing.Key;
                string name = string.Format("{0} {1}", httpMethod, path);
                MethodBase method = routing.Value;

                module.Before += context =>
                {
                    Response response = context.ResolvedRoute.Description.Name == name
                        ? BeforeAttribute.GetResponse(method, container, context)
                        : null;

                    return response;
                };

                builder[name, path] = dynamicParameters =>
                {
                    // dynamicParameter is an instance of Nancy.DynamicDictionary
                    IDictionary<string, object> parameters = dynamicParameters.ToDictionary();

                    object result = ConstructResult(container, method, module, parameters);
                    object response = ConstructResponse(method, module, result);

                    return response;
                };
            }
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
            object instance = container.Resolve(method.DeclaringType);

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
