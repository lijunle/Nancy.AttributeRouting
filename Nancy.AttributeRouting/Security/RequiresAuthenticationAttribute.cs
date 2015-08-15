namespace Nancy.AttributeRouting.Security
{
    using System.Diagnostics.CodeAnalysis;
    using Nancy.Security;
    using Nancy.TinyIoc;

    /// <summary>
    /// The member decorated with <see cref="RequiresAuthenticationAttribute"/> indicates it
    /// requires authentication.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RequiresAuthenticationAttribute : BeforeAttribute
    {
        /// <inheritdoc/>
        public override Response Process(TinyIoCContainer container, NancyContext context) =>
            SecurityHooks.RequiresAuthentication().Invoke(context);
    }
}
