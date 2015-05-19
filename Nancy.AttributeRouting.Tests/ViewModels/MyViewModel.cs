namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class MyViewModel
    {
        private readonly string property;

        [Get("/my-view-model")]
        public MyViewModel()
        {
        }

        [Get("/my-view-model/{property}")]
        public MyViewModel(string property)
        {
            this.property = property;
        }

        [Get("/my-view-model/all")]
        public MyViewModel(bool all = true)
        {
            this.property = "all-properties";
        }

        public string Property
        {
            get { return this.property ?? "Value"; }
        }

        [Get("/")]
        public ResultViewModel Index()
        {
            return new ResultViewModel
            {
                Result = "Index"
            };
        }

        [Get("/my/result")]
        public ResultViewModel GetResult()
        {
            return new ResultViewModel
            {
                Result = "MyResult"
            };
        }

        [Get("/my/result/{value}")]
        public ResultViewModel GetResult(string value)
        {
            return new ResultViewModel
            {
                Result = value
            };
        }

        [Get("/my/{property}/result/{value}")]
        public ResultViewModel GetResultWithProperty(string value)
        {
            return new ResultViewModel
            {
                Result = this.property + "." + value
            };
        }

        public string GetWithoutRoutings()
        {
            return string.Empty;
        }

        [Get("/my/1")]
        [Get("/my/2")]
        public ResultViewModel GetByTwoRoutings()
        {
            return new ResultViewModel
            {
                Result = "TheSameResultFromTwoRoutings"
            };
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
            [Get("/nested-view-model")]
            public NestedViewModel()
            {
            }

            public string NestedProperty
            {
                get { return "NestedValue"; }
            }

            [Get("/nested/result")]
            public ResultViewModel GetResult()
            {
                return new ResultViewModel
                {
                    Result = "NestedResult"
                };
            }

            [Get("/nested/result/{value}")]
            public ResultViewModel GetResult(string value)
            {
                return new ResultViewModel
                {
                    Result = "nested-" + value
                };
            }
        }

        public class ResultViewModel
        {
            public string Result { get; set; }
        }
    }
}
