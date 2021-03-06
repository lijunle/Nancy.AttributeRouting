﻿namespace Nancy.AttributeRouting.Exceptions
{
    using System;
    using System.Reflection;

    /// <summary>
    /// <see cref="MultipleRouteAttributesException"/> indicates multiple route attributes are
    /// decorated on method.
    /// </summary>
    [Serializable]
    public sealed class MultipleRouteAttributesException : Exception
    {
        internal MultipleRouteAttributesException(MethodBase method)
            : base(GenerateMessage(method))
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
