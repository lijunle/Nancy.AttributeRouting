namespace Nancy.AttributeRouting.Tests.ViewModels
{
    public class HttpMethodViewModel
    {
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
    }
}
