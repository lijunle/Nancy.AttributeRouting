namespace Nancy.AttributeRouting.Exceptions
{
    using System;
    using System.Reflection;

    /// <summary>
    /// <see cref="NoRouteAttributeException"/> indicates no route attribute is decorated on method.
    /// </summary>
    [Serializable]
    public sealed class NoRouteAttributeException : Exception
    {
        internal NoRouteAttributeException(MethodBase method)
            : base(GenerateMessage(method))
        {
        }

        private static string GenerateMessage(MethodBase method)
        {
            string message = string.Format(
                "No route attribute is decorated on method {0}.",
                method.GetFullName());

            return message;
        }
    }
}
