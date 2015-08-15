namespace Nancy.AttributeRouting.Examples.ViewModels.Todo
{
    using Responses;

    [RouteInherit(typeof(List))]
    [RoutePrefix("add")]
    public class Add
    {
        private readonly IUrlBuilder builder;

        public Add(IUrlBuilder builder)
        {
            this.builder = builder;
        }

        public string TodoUrl => this.builder.GetUrl<List>(m => m.Get());

        [Get("")]
        [View(nameof(Add))]
        public Add Get() => this;

        [Post("")]
        public Response Post(Form form)
        {
            return new RedirectResponse(this.TodoUrl);
        }

        public class Form
        {
            public string Description { get; set; }
        }
    }
}
