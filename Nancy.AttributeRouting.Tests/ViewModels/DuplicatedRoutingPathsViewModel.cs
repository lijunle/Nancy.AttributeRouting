namespace Nancy.AttributeRouting.Tests.ViewModels
{
    using System;
    using System.Collections.Generic;

    [FalseRoute]
    public class DuplicatedRoutingPathsViewModel
    {
        static DuplicatedRoutingPathsViewModel()
        {
            TypeProvider = new DuplicatedRoutingPathsTypeProvider();
        }

        internal static ITypeProvider TypeProvider { get; }

        [Get("/false/same-route")]
        public object Get1() => null;

        [Get("/false/same-route")]
        public object Get2() => null;

        private class DuplicatedRoutingPathsTypeProvider : ITypeProvider
        {
            public IEnumerable<Type> Types =>
                new[]
                {
                    typeof(DuplicatedRoutingPathsViewModel)
                };
        }
    }
}
