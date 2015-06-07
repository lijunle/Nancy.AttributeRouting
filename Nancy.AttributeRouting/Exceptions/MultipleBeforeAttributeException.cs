namespace Nancy.AttributeRouting.Exceptions
{
    using System;
    using System.Reflection;

    /// <summary>
    /// <see cref="MultipleBeforeAttributeException"/> indicates multiple before attributes are
    /// decorated on method or type.
    /// </summary>
    [Serializable]
    public sealed class MultipleBeforeAttributeException : Exception
    {
        internal MultipleBeforeAttributeException(MemberInfo member)
            : base()
        {
            // TODO generate the exception message
        }

        internal MultipleBeforeAttributeException(MethodBase method)
            : base(GenerateMessage(method))
        {
        }

        internal MultipleBeforeAttributeException(Type type)
            : base(GenerateMessage(type))
        {
        }

        private static string GenerateMessage(MethodBase method)
        {
            string message = string.Format(
                "Multiple before attributes are decorated on method {0}.",
                method.GetFullName());

            return message;
        }

        private static string GenerateMessage(Type type)
        {
            string message = string.Format(
                "Multiple before attributes are decorated on type {0}.",
                type.FullName);

            return message;
        }
    }
}
