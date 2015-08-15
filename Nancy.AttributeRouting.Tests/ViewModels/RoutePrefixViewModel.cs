namespace Nancy.AttributeRouting.Tests.ViewModels
{
    [RoutePrefix("/route-prefix")]
    public class RoutePrefixViewModel
    {
        [Get("/")]
        public object Get() => new { Result = "value" };

        [RouteInherit(typeof(RoutePrefixViewModel))]
        public class TypePrefixViewModel
        {
            [Get("/type-prefix")]
            public object GetTypePrefix() => new { Result = "from-type-prefix" };
        }

        [RouteInherit(typeof(RoutePrefixViewModel))]
        [RoutePrefix("child-prefix")]
        public class ChildPrefixViewModel
        {
            [Get("/")]
            public object GetChildPrefix() => new { Result = "from-child-prefix" };
        }

        [RouteInherit(typeof(ChildPrefixViewModel))]
        [RoutePrefix("grandchild")]
        public class GrandchildViewModel
        {
            [Get("/")]
            public object GetGrandchild() => new { Result = "from-grandchild" };
        }

        public class InheritViewModel : RoutePrefixViewModel
        {
            [Get("/route-prefix/inherit")]
            public object GetInherit() => new { Result = "inherit-not-affect-routing" };
        }

        [RouteInherit(typeof(RoutePrefixViewModel))]
        [RoutePrefix("{prefix}")]
        public class ParameterizedPrefixViewModel
        {
            [Get("{value}")]
            public object GetResultWithProperty(string prefix, string value) => new { Result = prefix + "." + value };
        }

        [RouteInherit(typeof(RoutePrefixViewModel))]
        [RoutePrefix("{prefix}")]
        public class ConstructorPrefixViewModel
        {
            private readonly string prefix;

            public ConstructorPrefixViewModel()
            {
                this.prefix = "parameters-not-pass-to-constructor";
            }

            public ConstructorPrefixViewModel(string prefix)
            {
                this.prefix = prefix;
            }

            [Get("/")]
            public object Get() => new { Result = this.prefix };
        }
    }
}
