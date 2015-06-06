namespace Nancy.AttributeRouting.Exceptions
{
    using System;
    using System.Reflection;

    public class NoRouteAttributeException : Exception
    {
        internal NoRouteAttributeException(MethodBase method)
            : base(GenerateMessage(method))
        {
        }

        private static string GenerateMessage(MethodBase method)
        {
            string message = string.Format(
                "Does not find any route attribute decorated on method {0}.",
                method.Name);

            return message;
        }
    }
}
