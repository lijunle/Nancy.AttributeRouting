namespace Nancy.AttributeRouting.Tests.ViewModels
{
    [RoutePrefix("/route-prefix")]
    public class RoutePrefixViewModel
    {
        [Get("/")]
        public object Get()
        {
            return new { Result = "value" };
        }

        [RoutePrefix(typeof(RoutePrefixViewModel), "")]
        public class TypePrefixViewModel
        {
            [Get("/type-prefix")]
            public object GetTypePrefix()
            {
                return new { Result = "from-type-prefix" };
            }
        }

        [RoutePrefix(typeof(RoutePrefixViewModel), "child-prefix")]
        public class ChildPrefixViewModel
        {
            [Get("/")]
            public object GetChildPrefix()
            {
                return new { Result = "from-child-prefix" };
            }
        }

        [RoutePrefix(typeof(ChildPrefixViewModel), "grandchild")]
        public class GrandchildViewModel
        {
            [Get("/")]
            public object GetGrandchild()
            {
                return new { Result = "from-grandchild" };
            }
        }

        public class InheritViewModel : RoutePrefixViewModel
        {
            [Get("/route-prefix/inherit")]
            public object GetInherit()
            {
                return new { Result = "inherit-not-affect-routing" };
            }
        }

        [RoutePrefix(typeof(RoutePrefixViewModel), "{prefix}")]
        public class ParameterizedPrefixViewModel
        {
            [Get("{value}")]
            public object GetResultWithProperty(string prefix, string value)
            {
                return new { Result = prefix + "." + value };
            }
        }
    }
}
