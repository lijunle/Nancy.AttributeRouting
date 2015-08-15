namespace Nancy.AttributeRouting.Security
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Nancy.Security;
    using Nancy.TinyIoc;

    /// <summary>
    /// The member decorated with <see cref="RequiresValidatedClaimsAttribute"/> indicates it
    /// requires claims to be validated.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class RequiresValidatedClaimsAttribute : BeforeAttribute
    {
        /// <inheritdoc/>
        public override Response Process(TinyIoCContainer container, NancyContext context) =>
            SecurityHooks.RequiresAuthentication().Invoke(context) ??
            SecurityHooks.RequiresValidatedClaims(this.IsValid).Invoke(context);

        /// <summary>
        /// The implementation to validate claims.
        /// </summary>
        /// <param name="claims">The claims from request.</param>
        /// <returns>True if the claims is valid, otherwise false.</returns>
        protected abstract bool IsValid(IEnumerable<string> claims);
    }
}
