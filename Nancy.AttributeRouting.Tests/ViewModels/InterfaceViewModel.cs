namespace Nancy.AttributeRouting.Tests.ViewModels
{
    [RoutePrefix("interface")]
    public interface IInterfaceViewModel
    {
        [Get("")]
        object Get();

        [Get("{value}")]
        object GetWithParamter(string value);
    }

    [RoutePrefix(typeof(IInterfaceViewModel), "child")]
    public interface IChildViewModel
    {
        [Get("")]
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
