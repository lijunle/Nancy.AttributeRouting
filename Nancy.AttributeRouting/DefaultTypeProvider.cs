namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TinyIoc;

    /// <summary>
    /// Type provider to provide types for routing register.
    /// </summary>
    public interface ITypeProvider
    {
        /// <summary>
        /// Gets the type list for routing register.
        /// Their methods decorated with <see cref="RouteAttribute"/> will be registed to routing table.
        /// By default, all types will be registed.
        /// </summary>
        /// <value>The type list.</value>
        IEnumerable<Type> Types { get; }
    }

    /// <summary>
    /// The default type provider for routing register.
    /// </summary>
    public class DefaultTypeProvider : ITypeProvider
    {
        /// <inheritdoc/>
        public IEnumerable<Type> Types =>
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.SafeGetTypes());
    }
}
