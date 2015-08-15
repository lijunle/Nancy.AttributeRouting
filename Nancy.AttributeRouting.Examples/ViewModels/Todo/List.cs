namespace Nancy.AttributeRouting.Examples.ViewModels.Todo
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;
    using Responses;

    [RoutePrefix("todo")]
    [ViewPrefix(nameof(Todo))]
    public class List
    {
        private readonly IUrlBuilder builder;

        private readonly ITodoDatabase database;

        public List(IUrlBuilder builder, ITodoDatabase database)
        {
            this.builder = builder;
            this.database = database;
        }

        public string HomeUrl => this.builder.GetUrl<Home>(m => m.Get());

        public string AddUrl => this.builder.GetUrl<Add>(m => m.Get());

        public IEnumerable<TodoItem> Items => this.database.Items;

        [Get("/")]
        [View(nameof(List))]
        public List Get() => this;

        [Post("")]
        public Response Complete(Form form)
        {
            foreach (var item in this.database.Items)
            {
                item.Completed = form.Ids.Contains(item.Id);
                this.database.Items.Update(item);
            }

            string listUrl = this.builder.GetUrl<List>(m => m.Get());
            return new RedirectResponse(listUrl);
        }

        public class Form
        {
            public IEnumerable<int> Ids { get; set; }
        }
    }
}
