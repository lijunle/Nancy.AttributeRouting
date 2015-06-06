namespace Nancy.AttributeRouting.Exceptions
{
    using System;
    using System.Reflection;

    public class MultipleRouteAttributesException : Exception
    {
        internal MultipleRouteAttributesException(MethodBase method, Exception innerException)
            : base(GenerateMessage(method), innerException)
        {
        }

        private static string GenerateMessage(MethodBase method)
        {
            string message = string.Format(
                "Method {0} decorates with more than one route attributes.",
                method.Name);

            return message;
        }
    }
}
