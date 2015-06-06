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
            string message = string.Format(
                "Attribute path {0} registered on two members, {1}.{2} and {3}.{4}.",
                path,
                routings[httpMethod][path].DeclaringType.FullName,
                routings[httpMethod][path].Name,
                method.DeclaringType.FullName,
                method.Name);

            return message;
        }
    }
}
