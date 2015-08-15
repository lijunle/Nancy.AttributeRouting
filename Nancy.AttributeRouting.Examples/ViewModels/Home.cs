namespace Nancy.AttributeRouting.Examples.ViewModels
{
    using Todo;

    public class Home
    {
        private readonly IUrlBuilder builder;

        public Home(IUrlBuilder builder)
        {
            this.builder = builder;
        }

        public string TodoUrl => this.builder.GetUrl<List>(m => m.Get());

        public string AboutUrl => this.builder.GetUrl<About>(m => m.Get());

        [Get("/")]
        public Home Get() => this;
    }
}
