namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class HtmlViewModel
    {
        //[Get("/html")]
        //[View("view")]
        public HtmlViewModel()
        {
        }

        public string Message
        {
            get { return "Hello world!"; }
        }

        [ViewPrefix("Inner")]
        public class InnerViewModel
        {
            //[Get("/html/inner")]
            //[View("inner")]
            public InnerViewModel()
            {
            }

            public virtual string Message
            {
                get { return "Get inner message."; }
            }
        }

        [ViewPrefix("Deeper")]
        public class DeeperViewModel : InnerViewModel
        {
            //[Get("/html/deeper")]
            //[View("deeper")]
            public DeeperViewModel()
            {
            }

            public override string Message
            {
                get { return "Get deeper message."; }
            }
        }

        public class InheritPrefixViewModel : InnerViewModel
        {
            //[Get("/html/inner/inherit")]
            //[View("inherit")]
            public InheritPrefixViewModel()
            {
            }

            public override string Message
            {
                get { return "Inherit prefix message."; }
            }
        }
    }
}
