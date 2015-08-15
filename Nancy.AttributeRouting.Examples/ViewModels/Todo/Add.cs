namespace Nancy.AttributeRouting.Examples.ViewModels.Todo
{
    using Model;
    using Responses;

    [RouteInherit(typeof(List))]
    [RoutePrefix("add")]
    public class Add
    {
        private readonly IUrlBuilder builder;

        private readonly ITodoDatabase database;

        public Add(IUrlBuilder builder, ITodoDatabase database)
        {
            this.builder = builder;
            this.database = database;
        }

        public string TodoUrl => this.builder.GetUrl<List>(m => m.Get());

        public string ErrorMessage { get; private set; }

        [Get("")]
        [View(nameof(Add))]
        public Add Get() => this;

        [Post("")]
        [View(nameof(Add))]
        public object Post(Form form)
        {
            if (string.IsNullOrWhiteSpace(form.Description))
            {
                this.ErrorMessage = "Description cannot be empty";
                return this;
            }

            this.database.Items.Insert(new TodoItem
            {
                Description = form.Description
            });

            return new RedirectResponse(this.TodoUrl);
        }

        public class Form
        {
            public string Description { get; set; }
        }
    }
}
