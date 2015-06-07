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

        [ViewPrefix("Prefix")]
        public class ViewPrefixViewModel
        {
            [Get("/html/prefix")]
            [View("view-prefix")]
            public object GetInnerView()
            {
                return new { Message = "Get view prefix message." };
            }
        }

        [RouteInherit(typeof(ViewPrefixViewModel))]
        public class TypePrefixViewModel
        {
            [Get("/html/prefix/type")]
            [View("type-prefix")]
            public object GetView()
            {
                return new { Message = "Get type prefix message." };
            }
        }

        [RouteInherit(typeof(ViewPrefixViewModel))]
        [ViewPrefix("Deeper")]
        public class DeeperViewModel
        {
            [Get("/html/prefix/deeper")]
            [View("deeper-prefix")]
            public object GetDeeperView()
            {
                return new { Message = "Get deeper prefix message." };
            }
        }
    }
}
