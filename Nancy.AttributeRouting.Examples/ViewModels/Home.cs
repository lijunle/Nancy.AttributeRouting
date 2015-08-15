namespace Nancy.AttributeRouting.Examples.ViewModels
{
    public class Home
    {
        private readonly IUrlBuilder builder;

        public Home(IUrlBuilder builder)
        {
            this.builder = builder;
        }

        public string TodoUrl => "/todo";

        public string AboutUrl => this.builder.GetUrl<About>(m => m.Get());

        [Get("/")]
        public Home Get() => this;
    }
}
