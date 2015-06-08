namespace Nancy.AttributeRouting.Security
{
    using Nancy.Security;
    using Nancy.TinyIoc;

    /// <summary>
    /// The member decorated with <see cref="RequiresHttpsAttribute"/> indicates it requires HTTPS protocol.
    /// </summary>
    public class RequiresHttpsAttribute : BeforeAttribute
    {
        private readonly bool redirect;

        private readonly int? httpsPort;

        /// <inheritdoc/>
        public RequiresHttpsAttribute()
            : this(redirect: true)
        {
        }

        /// <inheritdoc/>
        public RequiresHttpsAttribute(bool redirect)
        {
            this.redirect = redirect;
            this.httpsPort = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresHttpsAttribute"/> class.
        /// </summary>
        /// <param name="redirect">
        /// True if the user should be redirected to HTTPS if the incoming request was made using
        /// HTTP, otherwise false if <see cref="HttpStatusCode.Forbidden"/> should be returned.
        /// </param>
        /// <param name="httpsPort">The HTTPS port number to use</param>
        public RequiresHttpsAttribute(bool redirect, int httpsPort)
        {
            this.redirect = redirect;
            this.httpsPort = httpsPort;
        }

        /// <inheritdoc/>
        public override Response Process(TinyIoCContainer container, NancyContext context)
        {
            return SecurityHooks.RequiresHttps(this.redirect, this.httpsPort).Invoke(context);
        }
    }
}
