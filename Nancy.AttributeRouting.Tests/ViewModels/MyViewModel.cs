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
        public ResultViewModel Index()
        {
            return new ResultViewModel("Index");
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

        [Get("/my-view-model/all")]
        public MyViewModel GetWithAllProperties(bool all = true)
        {
            this.property = "all-properties";
            return this;
        }

        [Get("/my/result")]
        public ResultViewModel GetResult()
        {
            return new ResultViewModel("MyResult");
        }

        [Get("/my/result/{value}")]
        public ResultViewModel GetResult(string value)
        {
            return new ResultViewModel(value);
        }

        [Get("/my/{property}/result/{value}")]
        public ResultViewModel GetResultWithProperty(string value)
        {
            return new ResultViewModel(this.property + "." + value);
        }

        public string GetWithoutRoutings()
        {
            return string.Empty;
        }

        [Get("/my/1")]
        [Get("/my/2")]
        public ResultViewModel GetByTwoRoutings()
        {
            return new ResultViewModel("TheSameResultFromTwoRoutings");
        }

        [Delete("/my")]
        public string Delete()
        {
            return "MyDelete";
        }

        [Get("/my")]
        public string Get()
        {
            return "MyGet";
        }

        [Options("/my")]
        public string Options()
        {
            return "MyOptions";
        }

        [Patch("/my")]
        public string Patch()
        {
            return "MyPatch";
        }

        [Post("/my")]
        public string Post()
        {
            return "MyPost";
        }

        [Put("/my")]
        public string Put()
        {
            return "MyPut";
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
            public ResultViewModel GetResult()
            {
                return new ResultViewModel("NestedResult");
            }

            [Get("/nested/result/{value}")]
            public ResultViewModel GetResult(string value)
            {
                return new ResultViewModel("nested-" + value);
            }
        }

        public class ResultViewModel
        {
            public ResultViewModel(string result)
            {
                this.Result = result;
            }

            public string Result { get; private set; }
        }
    }
}
