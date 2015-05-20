namespace Nancy.AttributeRouting.Tests.ViewModels
{
    using System;

    public class ComplexViewModel
    {
        private readonly IUrlBuilder urlBuilder;

        //[Get("/complex/with-injection")]
        public ComplexViewModel(IUrlBuilder urlBuilder)
        {
            this.urlBuilder = urlBuilder;
        }

        public string Url
        {
            get
            {
                return this.urlBuilder.GetUrl<ComplexViewModel>(() => new ComplexViewModel(null));
            }
        }

        [Get("/complex/non-string/{age:int}/{graduated:bool}/{id:guid}/{birth:datetime}")]
        public object GetComplexRoute(Guid id, DateTime birth, int age, bool graduated)
        {
            return new { id, birth, age, graduated };
        }

        [Post("/complex/post/form")]
        public string GetByInjectedRequestData(Form form)
        {
            return string.Format("{0}={1}", form.User, form.Password);
        }

        [Get("/complex/get/optional/{name?}")]
        public string GetWithOptionalParameter(string name = "OptionalName")
        {
            return name;
        }

        [Get("/complex/special/{str}")]
        public object GetWithSpecialCharacters(string str)
        {
            return new { str };
        }

        public class Form
        {
            public string User { get; set; }

            public string Password { get; set; }
        }
    }
}
