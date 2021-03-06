﻿namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public interface IInterfaceViewModel
    {
        [Get("interface")]
        object Get();

        [Get("interface/{value}")]
        object GetWithParamter(string value);
    }

    [RouteInherit(typeof(IInterfaceViewModel))]
    [RoutePrefix("interface")]
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

    [ViewPrefix("Prefix")]
    public interface IHtmlChildViewModel
    {
        [Get("interface/html/child")]
        [View("view-prefix")]
        object Get();
    }

    public interface INumberOfInstanceViewModel
    {
        [Get("interface/number-of-instance")]
        string GetInstanceNumber();
    }

    public interface IChildOfClassViewModel
    {
        [Get("interface/child-of-class")]
        object GetChildOfClass();
    }

    public interface IInterfaceWithoutImplementation
    {
        // In such case, request should return internal server error (500) because the interface
        // cannot be resolved from IoC, for URL builder should still work fine because it generates
        // URL from lambda expression.
        [Get("interface/without-implementation")]
        object Get();
    }

    public class InterfaceViewModel : IInterfaceViewModel
    {
        public object Get() => new { Result = "query-from-interface" };

        public object GetWithParamter(string value) => new { Result = value };

        public class ChildViewModel : IChildViewModel
        {
            public object Get() => new { Result = "from-child-interface" };
        }

        public class HtmlViewModel : IHtmlViewModel
        {
            public object Get() => new { Message = "Get HTML from interface." };
        }

        public class HtmlChildViewModel : IHtmlChildViewModel
        {
            public object Get() => new { Message = "Get HTML with view prefix from interface." };
        }

        public class NumberOfInstanceViewModel : INumberOfInstanceViewModel
        {
            private static int number = 0;

            public NumberOfInstanceViewModel()
            {
                number++;
            }

            public string GetInstanceNumber() => number.ToString();
        }

        public class ChildOfClassViewModel : InterfaceViewModel, IChildOfClassViewModel
        {
            public object GetChildOfClass() => new { Result = "from-child-of-class" };
        }
    }
}
