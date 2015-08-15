namespace Nancy.AttributeRouting.Tests.ViewModels
{
    using System;
    using Nancy.Responses;
    using Nancy.TinyIoc;

    [RoutePrefix("before")]
    public class BeforeAttributeViewModel
    {
        [Rejected("rejected-by-interface")]
        public interface IRejectedByInterfaceViewModel
        {
            [Get("before/rejected/by-interface")]
            object Get();
        }

        [RouteInherit(typeof(IRejectedByInterfaceViewModel))]
        public interface IRejectedByInterfaceAncestorViewModel
        {
            [Get("before/rejected/by-interface-ancestor")]
            object Get();
        }

        [MultipleBefore]
        [MultipleBefore]
        public interface IMultipleBeforeOnInterfaceViewModel
        {
            [Get("before/multiple-on-interface")]
            object Get();
        }

        [Get("/")]
        [Rejected("reject-root-path")]
        public object RejectRootPath() => new { Result = "should-not-be-here" };

        [Get("rejected")]
        [Rejected("before-rejected")]
        public object Reject() => new { Result = "should-not-be-here" };

        [Get("passed")]
        [Passed]
        public object Pass() => new { Result = "before-passed" };

        [Get("same-name")]
        [Rejected("rejected-get-same-name")]
        public object GetSameName() => new { Result = "should-not-be-here" };

        [Post("same-name")]
        [Rejected("rejected-post-same-name")]
        public object PostSameName() => new { Result = "should-not-be-here" };

        public class RejectedByInterfaceViewModel : IRejectedByInterfaceViewModel
        {
            // interface rejected attribute applies here
            public object Get() => new { Result = "should-not-be-here" };
        }

        public class RejectedByInterfaceAncestorViewModel : IRejectedByInterfaceAncestorViewModel
        {
            // interface ancestor rejected attribute applies here
            public object Get() => new { Result = "should-not-be-here" };
        }

        [Rejected("rejected-by-class")]
        public class RejectedByClassViewModel
        {
            // if method attribute is absent, the class level attribute applies.
            [Get("before/rejected/by-class")]
            public object RejectByClass() => new { Result = "should-not-be-here" };
        }

        [RouteInherit(typeof(RejectedByClassViewModel))]
        public class RejectByAncestorViewModel
        {
            // the route inherited attribute applies here
            [Get("before/rejected/by-ancestor")]
            public object Pass() => new { Result = "should-not-be-here" };
        }

        public class MultipleBeforeOnMethodViewModel
        {
            [MultipleBefore]
            [MultipleBefore]
            [Get("before/multiple-on-method")]
            public object Get() => new { Result = "should-not-be-here" };
        }

        [MultipleBefore]
        [MultipleBefore]
        public class MultipleBeforeOnClassViewModel
        {
            [Get("before/multiple-on-class")]
            public object Get() => new { Result = "should-not-be-here" };
        }

        public class MultipleBeforeOnInterfaceViewModel : IMultipleBeforeOnInterfaceViewModel
        {
            public object Get() => new { Result = "should-not-be-here" };
        }

        private class RejectedAttribute : BeforeAttribute
        {
            private readonly string message;

            public RejectedAttribute(string message)
            {
                this.message = message;
            }

            public override Response Process(TinyIoCContainer container, NancyContext context)
            {
                var result = new { Result = this.message };
                return new JsonResponse(result, new DefaultJsonSerializer());
            }
        }

        private class PassedAttribute : BeforeAttribute
        {
            public override Response Process(TinyIoCContainer container, NancyContext context) => null;
        }

        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
        private class MultipleBeforeAttribute : BeforeAttribute
        {
            public override Response Process(TinyIoCContainer container, NancyContext context) => null;
        }
    }
}
