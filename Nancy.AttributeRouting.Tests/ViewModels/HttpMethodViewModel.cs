namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class HttpMethodViewModel
    {
        [Delete("/my")]
        public string Delete() => "MyDelete";

        [Get("/my")]
        public string Get() => "MyGet";

        [Options("/my")]
        public string Options() => "MyOptions";

        [Patch("/my")]
        public string Patch() => "MyPatch";

        [Post("/my")]
        public string Post() => "MyPost";

        [Put("/my")]
        public string Put() => "MyPut";
    }
}
