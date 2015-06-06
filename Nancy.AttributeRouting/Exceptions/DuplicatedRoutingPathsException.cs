namespace Nancy.AttributeRouting.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// <see cref="DuplicatedRoutingPathsException"/> indicates two or more methods are decorated
    /// with a same routing path on a same HTTP method.
    /// </summary>
    [Serializable]
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
