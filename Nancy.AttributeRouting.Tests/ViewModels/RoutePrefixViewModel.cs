namespace Nancy.AttributeRouting.Tests.ViewModels
{
    [RoutePrefix("/route-prefix")]
    public class RoutePrefixViewModel
    {
        [Get("/")]
        public RoutePrefixViewModel()
        {
        }

        public virtual string Value
        {
            get { return "value"; }
        }

        [RoutePrefix("/inner")]
        public class InnerPrefixViewModel : RoutePrefixViewModel
        {
            [Get("/")]
            public InnerPrefixViewModel()
            {
            }

            public override string Value
            {
                get { return "inner value"; }
            }
        }

        public class InheritPrefixViewModel : RoutePrefixViewModel
        {
            [Get("/inherit")]
            public InheritPrefixViewModel()
            {
            }

            public override string Value
            {
                get { return "inherit value"; }
            }
        }

        [RoutePrefix("inherit")]
        public class InheritInnerViewModel : InnerPrefixViewModel
        {
            [Get("/")]
            public InheritInnerViewModel()
            {
            }

            public override string Value
            {
                get { return "inherit inner value"; }
            }
        }
    }
}
