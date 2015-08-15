namespace Nancy.AttributeRouting.Examples.ViewModels
{
    public class About
    {
        private readonly IUrlBuilder builder;

        public About(IUrlBuilder builder)
        {
            this.builder = builder;
        }

        public string TodoUrl => "/todo";

        public string HomeUrl => this.builder.GetUrl<Home>(m => m.Get());

        [Get("about")]
        public About Get() => this;
    }
}
