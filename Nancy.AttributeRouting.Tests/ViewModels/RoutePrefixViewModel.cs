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

        [RoutePrefix(typeof(RoutePrefixViewModel), "/inner")]
        public class InnerPrefixViewModel : RoutePrefixViewModel
        {
            [Get("/")]
            public object GetInnerPrefix()
            {
                return new { Result = "inner value" };
            }
        }

        [RoutePrefix(typeof(RoutePrefixViewModel), "")]
        public class InheritPrefixViewModel : RoutePrefixViewModel
        {
            [Get("/inherit")]
            public object GetInheritPrefix()
            {
                return new { Result = "inherit value" };
            }
        }

        [RoutePrefix(typeof(InnerPrefixViewModel), "inherit")]
        public class InheritInnerViewModel : InnerPrefixViewModel
        {
            [Get("/")]
            public object GetInheritInner()
            {
                return new { Result = "inherit inner value" };
            }
        }

        [RoutePrefix(typeof(RoutePrefixViewModel), "{prefix}")]
        public class PlaceholderViewModel : RoutePrefixViewModel
        {
            private readonly string prefix;

            public PlaceholderViewModel(string prefix)
            {
                this.prefix = prefix;
            }

            [Get("{value}")]
            public object GetResultWithProperty(string value)
            {
                return new { Result = this.prefix + "." + value };
            }
        }
    }
}
