namespace Nancy.AttributeRouting.Examples.ViewModels
{
    using Todo;

    public class About
    {
        private readonly IUrlBuilder builder;

        public About(IUrlBuilder builder)
        {
            this.builder = builder;
        }

        public string TodoUrl => this.builder.GetUrl<List>(m => m.Get());

        public string HomeUrl => this.builder.GetUrl<Home>(m => m.Get());

        [Get("about")]
        public About Get() => this;
    }
}
