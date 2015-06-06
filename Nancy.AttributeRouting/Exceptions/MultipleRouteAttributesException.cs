﻿namespace Nancy.AttributeRouting.Exceptions
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
                "Multiple route attributes are decorated on method {0}.",
                method.GetFullName());

            return message;
        }
    }
}
