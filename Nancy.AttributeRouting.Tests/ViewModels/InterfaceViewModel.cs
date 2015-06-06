namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public interface IInterfaceViewModel
    {
        object Get();

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
