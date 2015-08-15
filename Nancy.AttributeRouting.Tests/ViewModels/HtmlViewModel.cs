namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class HtmlViewModel
    {
        [Get("/html")]
        [View("view")]
        public object GetView() => new { Message = "Hello world!" };

        [Get("/html/subfolder")]
        [View("Prefix/view-prefix")]
        public object GetSubfolder() => new { Message = "Get subfolder file from root." };

        [ViewPrefix("Prefix")]
        public class ViewPrefixViewModel
        {
            [Get("/html/prefix")]
            [View("view-prefix")]
            public object GetInnerView() => new { Message = "Get view prefix message." };
        }

        [RouteInherit(typeof(ViewPrefixViewModel))]
        public class TypePrefixViewModel
        {
            [Get("/html/prefix/type")]
            [View("type-prefix")]
            public object GetView() => new { Message = "Get type prefix message." };
        }

        [RouteInherit(typeof(ViewPrefixViewModel))]
        [ViewPrefix("Deeper")]
        public class DeeperViewModel
        {
            [Get("/html/prefix/deeper")]
            [View("deeper-prefix")]
            public object GetDeeperView() => new { Message = "Get deeper prefix message." };
        }
    }
}
