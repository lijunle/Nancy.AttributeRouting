namespace Nancy.AttributeRouting.Tests.ViewModels
{
    [RoutePrefix("/route-prefix")]
    public class RoutePrefixViewModel
    {
        [Get("/")]
        public RoutePrefixViewModel()
        {
        }

        public string Value
        {
            get { return "value"; }
        }
    }
}
