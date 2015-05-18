namespace Nancy.AttributeRouting.Tests.ViewModels
{
    using Nancy.Responses;
    using Nancy.TinyIoc;

    [RoutePrefix("before")]
    public class BeforeAttributeViewModel
    {
        [Get("rejected")]
        [Rejected]
        public object Get()
        {
            return new { Result = "should-not-be-here" };
        }

        [Get("passed")]
        [Passed]
        public object Passed()
        {
            return new { Result = "before-passed" };
        }

        private class RejectedAttribute : BeforeAttribute
        {
            public override Response Process(TinyIoCContainer container, NancyContext context)
            {
                var result = new { Result = "before-rejected" };
                return new JsonResponse(result, new DefaultJsonSerializer());
            }
        }

        private class PassedAttribute : BeforeAttribute
        {
            public override Response Process(TinyIoCContainer container, NancyContext context)
            {
                return null;
            }
        }
    }
}
