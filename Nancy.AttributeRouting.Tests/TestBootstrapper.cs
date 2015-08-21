namespace Nancy.AttributeRouting.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TinyIoc;

    internal class TestBootstrapper : DefaultNancyBootstrapper
    {
        private readonly ITypeProvider typeProvider;

        internal TestBootstrapper()
            : this(NonFalseRouteTypeProvider.Instance)
        {
        }

        internal TestBootstrapper(ITypeProvider typeProvider)
        {
            this.typeProvider = typeProvider;
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register(this.typeProvider);
        }

        private class NonFalseRouteTypeProvider : ITypeProvider
        {
            private readonly ITypeProvider defaultTypeProvider;

            static NonFalseRouteTypeProvider()
            {
                Instance = new NonFalseRouteTypeProvider();
            }

            internal NonFalseRouteTypeProvider()
            {
                this.defaultTypeProvider = new DefaultTypeProvider();
            }

            /// <inheritdoc/>
            public IEnumerable<Type> Types =>
                this.defaultTypeProvider.Types.Where(IsNotFalseRoute);

            internal static NonFalseRouteTypeProvider Instance { get; }

            private static bool IsNotFalseRoute(Type type) =>
                !type.GetCustomAttributes<FalseRouteAttribute>().Any();
        }
    }
}
