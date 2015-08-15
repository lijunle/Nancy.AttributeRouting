namespace Nancy.AttributeRouting.Examples.Model
{
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
        }

        public ITable<TodoItem> Items { get; }
    }
}
