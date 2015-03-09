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
    }
}
