namespace Nancy.AttributeRouting.Security
{
    using Nancy.Security;
    using Nancy.TinyIoc;

    /// <summary>
    /// The member decorated with <see cref="RequiresAuthenticationAttribute"/> indicates it
    /// requires authentication.
    /// </summary>
    public class RequiresAuthenticationAttribute : BeforeAttribute
    {
        /// <inheritdoc/>
        public override Response Process(TinyIoCContainer container, NancyContext context)
        {
            return SecurityHooks.RequiresAuthentication().Invoke(context);
        }
    }
}
