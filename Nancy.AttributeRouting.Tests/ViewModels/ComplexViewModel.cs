namespace Nancy.AttributeRouting.Tests.ViewModels
{
    using System;

    public class ComplexViewModel
    {
        private readonly IUrlBuilder urlBuilder;

        public ComplexViewModel(IUrlBuilder urlBuilder)
        {
            this.urlBuilder = urlBuilder;
        }

        public string Url => this.urlBuilder.GetUrl<ComplexViewModel>(m => m.GetWithInjection());

        [Get("/complex/with-injection")]
        public ComplexViewModel GetWithInjection() => this;

        [Get("/complex/guid/{id:guid}")]
        public string GetGuid(Guid id) => id.ToString();

        [Get("/complex/datetime/{time:datetime}")]
        public string GetDateTime(DateTime time) => time.ToString();

        [Get("/complex/int/{number:int}")]
        public string GetInt(int number) => number.ToString();

        [Get("/complex/boolean/{flag:bool}")]
        public string GetBoolean(bool flag) => flag.ToString();

        [Get("/complex/regex/(?<name>.+)")]
        public string GetRegex(string name) => name;

        [Post("/complex/post/form")]
        public string GetByInjectedRequestData(Form form) => string.Format("{0}={1}", form.User, form.Password);

        [Get("/complex/get/optional/{name?default}")]
        public object GetWithOptionalParameter(string name = null) => new { Name = name };

        [Get("/complex/get/missing/{name?}")]
        public object GetWithMissingParameter(string name = null) => new { Name = name ?? "default-name" };

        [Get("/complex/special/{str}")]
        public object GetWithSpecialCharacters(string str) => new { str };

        public class Form
        {
            public string User { get; set; }

            public string Password { get; set; }
        }
    }
}
