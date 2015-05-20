namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class HtmlViewModel
    {
        [Get("/html")]
        [View("view")]
        public object GetView()
        {
            return new { Message = "Hello world!" };
        }

        [ViewPrefix("Inner")]
        public class InnerViewModel
        {
            [Get("/html/inner")]
            [View("inner")]
            public object GetInnerView()
            {
                return new { Message = "Get inner message." };
            }
        }

        [ViewPrefix("Deeper")]
        public class DeeperViewModel : InnerViewModel
        {
            [Get("/html/deeper")]
            [View("deeper")]
            public object GetDeeperView()
            {
                return new { Message = "Get deeper message." };
            }
        }

        public class InheritPrefixViewModel : InnerViewModel
        {
            [Get("/html/inner/inherit")]
            [View("inherit")]
            public object GetView()
            {
                return new { Message = "Inherit prefix message." };
            }
        }
    }
}
