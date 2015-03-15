namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class HtmlViewModel
    {
        [Get("/html")]
        [View("view")]
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
            [Get("/html/inner")]
            [View("inner")]
            public InnerViewModel()
            {
            }

            public string Message
            {
                get { return "Get inner message."; }
            }
        }
    }
}
