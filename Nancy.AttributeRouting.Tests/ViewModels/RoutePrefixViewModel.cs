namespace Nancy.AttributeRouting.Tests.ViewModels
{
    [RoutePrefix("/route-prefix")]
    public class RoutePrefixViewModel
    {
        [Get("/")]
        public object Get()
        {
            return new { Result = "value" };
        }

        [RoutePrefix(typeof(RoutePrefixViewModel), "{prefix}")]
        public class PlaceholderViewModel
        {
            private readonly string prefix;

            public PlaceholderViewModel(string prefix)
            {
                this.prefix = prefix;
            }

            [Get("{value}")]
            public object GetResultWithProperty(string value)
            {
                return new { Result = this.prefix + "." + value };
            }
        }
    }
}
