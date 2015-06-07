namespace Nancy.AttributeRouting.Exceptions
{
    using System;
    using System.Reflection;

    [Serializable]
    public sealed class MultipleBeforeAttributeException : Exception
    {
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
