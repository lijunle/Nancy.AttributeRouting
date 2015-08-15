namespace Nancy.AttributeRouting.Examples.ViewModels.Todo
{
    [RoutePrefix("todo")]
    [ViewPrefix(nameof(Todo))]
    public class List
    {
        private readonly IUrlBuilder builder;

        public List(IUrlBuilder builder)
        {
            this.builder = builder;
        }

        public string HomeUrl => this.builder.GetUrl<Home>(m => m.Get());

        public string AddUrl => this.builder.GetUrl<Add>(m => m.Get());

        [Get("/")]
        [View(nameof(List))]
        public List Get() => this;
    }
}
