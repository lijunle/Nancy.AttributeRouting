namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public interface IInterfaceViewModel
    {
        [Get("interface")]
        object Get();

        [Get("interface/{value}")]
        object GetWithParamter(string value);
    }

    [RoutePrefix(typeof(IInterfaceViewModel), "interface")]
    public interface IChildViewModel
    {
        [Get("child")]
        object Get();
    }

    public class InterfaceViewModel : IInterfaceViewModel
    {
        public object Get()
        {
            return new { Result = "query-from-interface" };
        }

        public object GetWithParamter(string value)
        {
            return new { Result = value };
        }

        public class ChildViewModel : IChildViewModel
        {
            public object Get()
            {
                return new { Result = "from-child-interface" };
            }
        }
    }
}
