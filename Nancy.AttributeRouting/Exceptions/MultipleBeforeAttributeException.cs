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
            : base(GenerateMessage(member))
        {
        }

        private static string GenerateMessage(MemberInfo member)
        {
            string message = string.Format(
                "Multiple before attributes are decorated on member {0}.",
                member.GetFullName());

            return message;
        }
    }
}
