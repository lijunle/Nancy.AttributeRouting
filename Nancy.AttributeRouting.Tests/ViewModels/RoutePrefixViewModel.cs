namespace Nancy.AttributeRouting.Tests.ViewModels
{
    [RoutePrefix("/route-prefix")]
    public class RoutePrefixViewModel
    {
        public virtual string Value
        {
            get { return "value"; }
        }

        [Get("/")]
        public RoutePrefixViewModel Get()
        {
            return this;
        }

        [RoutePrefix("/inner")]
        public class InnerPrefixViewModel : RoutePrefixViewModel
        {
            public override string Value
            {
                get { return "inner value"; }
            }

            [Get("/")]
            public InnerPrefixViewModel GetInnerPrefix()
            {
                return this;
            }
        }

        public class InheritPrefixViewModel : RoutePrefixViewModel
        {
            public override string Value
            {
                get { return "inherit value"; }
            }

            [Get("/inherit")]
            public InheritPrefixViewModel GetInheritPrefix()
            {
                return this;
            }
        }

        [RoutePrefix("inherit")]
        public class InheritInnerViewModel : InnerPrefixViewModel
        {
            public override string Value
            {
                get { return "inherit inner value"; }
            }

            [Get("/")]
            public InheritInnerViewModel GetInheritInner()
            {
                return this;
            }
        }
    }
}
