namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class HtmlViewModel
    {
        public string Message
        {
            get { return "Hello world!"; }
        }

        [Get("/html")]
        [View("view")]
        public HtmlViewModel GetView()
        {
            return this;
        }

        [ViewPrefix("Inner")]
        public class InnerViewModel
        {
            public virtual string Message
            {
                get { return "Get inner message."; }
            }

            [Get("/html/inner")]
            [View("inner")]
            public InnerViewModel GetInnerView()
            {
                return this;
            }
        }

        [ViewPrefix("Deeper")]
        public class DeeperViewModel : InnerViewModel
        {
            public override string Message
            {
                get { return "Get deeper message."; }
            }

            [Get("/html/deeper")]
            [View("deeper")]
            public DeeperViewModel GetDeeperView()
            {
                return this;
            }
        }

        public class InheritPrefixViewModel : InnerViewModel
        {
            public override string Message
            {
                get { return "Inherit prefix message."; }
            }

            [Get("/html/inner/inherit")]
            [View("inherit")]
            public InheritPrefixViewModel GetView()
            {
                return this;
            }
        }
    }
}
