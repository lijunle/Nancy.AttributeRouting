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
    }
}
