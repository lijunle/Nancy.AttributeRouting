namespace Nancy.AttributeRouting.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class DuplicatedRoutingPathsException : Exception
    {
        internal DuplicatedRoutingPathsException(
            Dictionary<HttpMethod, Dictionary<string, MethodBase>> routings,
            HttpMethod httpMethod,
            string path,
            MethodBase method)
            : base(GenerateMessage(routings, httpMethod, path, method))
        {
        }

        private static string GenerateMessage(
            Dictionary<HttpMethod, Dictionary<string, MethodBase>> routings,
            HttpMethod httpMethod,
            string path,
            MethodBase method)
        {
            string registeredMethodName = routings[httpMethod][path].GetFullName();
            string pendingMethodName = method.GetFullName();

            string message = string.Format(
                "Routing path {0} has already been decorated on method {1}, but also decorated on method {2}.",
                path,
                registeredMethodName,
                pendingMethodName);

            return message;
        }
    }
}
