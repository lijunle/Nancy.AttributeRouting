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

    public interface IHtmlViewModel
    {
        [Get("interface/html")]
        [View("view")]
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

        public class HtmlViewModel : IHtmlViewModel
        {
            public object Get()
            {
                return new { Message = "Get HTML from interface." };
            }
        }
    }
}
