namespace Nancy.AttributeRouting
{
    using System;
    using Nancy.TinyIoc;

    /// <summary>
    /// Before attribute provides a hook to execute before enter the view model execution.
    /// </summary>
    public abstract class BeforeAttribute : Attribute
    {
        /// <summary>
        /// Process the custom code and determine whether continue on view model execution.
        /// </summary>
        /// <param name="container">
        /// The Tiny IoC container. It provides <see cref="IUrlBuilder"/> and others to construct
        /// the response.
        /// </param>
        /// <param name="context">
        /// The Nancy context. It provides user information and others to determine whether continue
        /// view model execution.
        /// </param>
        /// <returns>
        /// The response. If this is <c>null</c>, it will continue on view model execution,
        /// otherwise it returns the this value directly.
        /// </returns>
        public abstract Response Process(TinyIoCContainer container, NancyContext context);
    }
}
