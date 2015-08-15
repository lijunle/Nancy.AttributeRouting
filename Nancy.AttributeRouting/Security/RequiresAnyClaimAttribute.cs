namespace Nancy.AttributeRouting.Security
{
    using System.Collections.Generic;
    using System.Linq;
    using Nancy.Security;
    using Nancy.TinyIoc;

    /// <summary>
    /// The member decorated with <see cref="RequiresAnyClaimAttribute"/> indicates it requires
    /// authentication and any one of certain claims to be present.
    /// </summary>
    public class RequiresAnyClaimAttribute : BeforeAttribute
    {
        private readonly IEnumerable<string> requiredClaims;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiresAnyClaimAttribute"/> class.
        /// </summary>
        /// <param name="requiredClaims">The claims to be present for authentication.</param>
        public RequiresAnyClaimAttribute(IEnumerable<string> requiredClaims)
        {
            this.requiredClaims = requiredClaims;
        }

        /// <inheritdoc/>
        public RequiresAnyClaimAttribute(params string[] requiredClaims)
            : this(requiredClaims.AsEnumerable())
        {
        }

        /// <inheritdoc/>
        public override Response Process(TinyIoCContainer container, NancyContext context) =>
            SecurityHooks.RequiresAuthentication().Invoke(context) ??
            SecurityHooks.RequiresAnyClaim(this.requiredClaims).Invoke(context);
    }
}
