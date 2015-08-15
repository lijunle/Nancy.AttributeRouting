namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class MyViewModel
    {
        private string property;

        public string Property => this.property ?? "Value";

        [Get("/")]
        public object Index() => new { Result = "Index" };

        [Get("/my-view-model")]
        public MyViewModel GetWithDefaultProperty() => this;

        [Get("/my-view-model/{property}")]
        public MyViewModel GetWithProperty(string property)
        {
            this.property = property;
            return this;
        }

        [Get("/my/result")]
        public object GetResult() => new { Result = "MyResult" };

        [Get("/my/result/{value}")]
        public object GetResult(string value) => new { Result = value };

        public string GetWithoutRoutings() => string.Empty;

        [Get("/my/1")]
        [Get("/my/2")]
        public object GetByTwoRoutings() => new { Result = "TheSameResultFromTwoRoutings" };

        public class NestedViewModel
        {
            public string NestedProperty => "NestedValue";

            [Get("/nested-view-model")]
            public NestedViewModel Get() => this;

            [Get("/nested/result")]
            public object GetResult() => new { Result = "NestedResult" };

            [Get("/nested/result/{value}")]
            public object GetResult(string value) => new { Result = "nested-" + value };
        }
    }
}
