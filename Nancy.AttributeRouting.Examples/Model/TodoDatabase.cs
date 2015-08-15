namespace Nancy.AttributeRouting.Examples.Model
{
    using System.Linq;
    using NMemory;
    using NMemory.Tables;

    public interface ITodoDatabase
    {
        ITable<TodoItem> Items { get; }
    }

    public class TodoDatabase : Database, ITodoDatabase
    {
        public TodoDatabase()
        {
            this.Items = this.Tables.Create(item => item.Id, new IdentitySpecification<TodoItem>(item => item.Id));

            foreach (int index in Enumerable.Range(1, 5))
            {
                this.Items.Insert(new TodoItem
                {
                    Description = $"No.{index} description",
                    Completed = index % 2 == 0
                });
            }
        }

        public ITable<TodoItem> Items { get; }
    }
}
