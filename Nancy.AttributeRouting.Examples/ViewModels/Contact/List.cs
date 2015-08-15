namespace Nancy.AttributeRouting.Examples.ViewModels.Contact
{
    using System.Collections.Generic;
    using System.Linq;
    using Model;

    [RoutePrefix("contact")]
    [ViewPrefix(nameof(Contact))]
    public class List
    {
        private readonly IUrlBuilder builder;

        private readonly IContactDatabase database;

        public List(IUrlBuilder builder, IContactDatabase database)
        {
            this.builder = builder;
            this.database = database;
        }

        public string HomeUrl => this.builder.GetUrl<Home>(m => m.Get());

        public string AddUrl => this.builder.GetUrl<List>(m => m.Add(null));

        public string DeleteUrl => this.builder.GetUrl<List>(m => m.Delete(null));

        public IEnumerable<ContactItem> Contacts => this.database.Items;

        [Get("")]
        [View(nameof(List))]
        public List Get() => this;

        [Post("add")]
        public AjaxResponse Add(Form form)
        {
            int phone;
            if (!int.TryParse(form.Phone, out phone))
            {
                return new AjaxResponse
                {
                    ErrorMessage = "Phone must be number"
                };
            }

            var contact = new ContactItem
            {
                Name = form.Name,
                Phone = phone
            };

            this.database.Items.Insert(contact);

            return new AjaxResponse
            {
                ContactId = contact.Id
            };
        }

        [Post("delete")]
        public AjaxResponse Delete(Form form)
        {
            ContactItem contact = this.database.Items.SingleOrDefault(item => item.Id == form.ContactId);
            if (contact == null)
            {
                return new AjaxResponse
                {
                    ErrorMessage = $"Contact with ID {form.ContactId} is not existed"
                };
            }

            this.database.Items.Delete(contact);

            return new AjaxResponse
            {
                ContactId = form.ContactId
            };
        }

        public class Form
        {
            public int ContactId { get; set; }

            public string Name { get; set; }

            public string Phone { get; set; }
        }

        public class AjaxResponse
        {
            public string ErrorMessage { get; set; }

            public int ContactId { get; set; }
        }
    }
}
