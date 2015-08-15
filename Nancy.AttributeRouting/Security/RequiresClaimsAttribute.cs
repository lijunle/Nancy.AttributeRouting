namespace Nancy.AttributeRouting.Security
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Nancy.Security;
    using Nancy.TinyIoc;

    /// <summary>
    /// The member decorated with <see cref="RequiresClaimsAttribute"/> indicates it requires
    /// authentication and certain claims to be present.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RequiresClaimsAttribute : BeforeAttribute
    {
        private readonly IEnumerable<string> claims;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresClaimsAttribute"/> class.
        /// </summary>
        /// <param name="claims">The claims to be present for authentication.</param>
        public RequiresClaimsAttribute(IEnumerable<string> claims)
        {
            this.claims = claims;
        }

        /// <inheritdoc/>
        public RequiresClaimsAttribute(params string[] claims)
            : this(claims.AsEnumerable())
        {
        }

        /// <inheritdoc/>
        public override Response Process(TinyIoCContainer container, NancyContext context) =>
            SecurityHooks.RequiresAuthentication().Invoke(context) ??
            SecurityHooks.RequiresClaims(this.claims).Invoke(context);
    }
}
