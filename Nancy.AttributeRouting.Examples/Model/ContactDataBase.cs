namespace Nancy.AttributeRouting.Examples.Model
{
    using System.Linq;
    using NMemory;
    using NMemory.Tables;

    public interface IContactDatabase
    {
        ITable<ContactItem> Items { get; }
    }

    public class ContactDatabase : Database, IContactDatabase
    {
        public ContactDatabase()
        {
            this.Items = this.Tables.Create(item => item.Id, new IdentitySpecification<ContactItem>(item => item.Id));

            foreach (int index in Enumerable.Range(1, 5))
            {
                this.Items.Insert(new ContactItem
                {
                    Name = $"Name_{index}",
                    Phone = index * 12345678
                });
            }
        }

        public ITable<ContactItem> Items { get; }
    }
}
