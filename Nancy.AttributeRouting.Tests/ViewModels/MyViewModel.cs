namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class MyViewModel
    {
        private string property;

        public string Property
        {
            get { return this.property ?? "Value"; }
        }

        [Get("/")]
        public object Index()
        {
            return new { Result = "Index" };
        }

        [Get("/my-view-model")]
        public MyViewModel GetWithDefaultProperty()
        {
            return this;
        }

        [Get("/my-view-model/{property}")]
        public MyViewModel GetWithProperty(string property)
        {
            this.property = property;
            return this;
        }

        [Get("/my/result")]
        public object GetResult()
        {
            return new { Result = "MyResult" };
        }

        [Get("/my/result/{value}")]
        public object GetResult(string value)
        {
            return new { Result = value };
        }

        public string GetWithoutRoutings()
        {
            return string.Empty;
        }

        [Get("/my/1")]
        [Get("/my/2")]
        public object GetByTwoRoutings()
        {
            return new { Result = "TheSameResultFromTwoRoutings" };
        }

        public class NestedViewModel
        {
            public string NestedProperty
            {
                get { return "NestedValue"; }
            }

            [Get("/nested-view-model")]
            public NestedViewModel Get()
            {
                return this;
            }

            [Get("/nested/result")]
            public object GetResult()
            {
                return new { Result = "NestedResult" };
            }

            [Get("/nested/result/{value}")]
            public object GetResult(string value)
            {
                return new { Result = "nested-" + value };
            }
        }
    }
}
