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

        [Get("/")]
        [Rejected("reject-root-path")]
        public object RejectRootPath()
        {
            return new { Result = "should-not-be-here" };
        }

        [Get("rejected")]
        [Rejected("before-rejected")]
        public object Reject()
        {
            return new { Result = "should-not-be-here" };
        }

        [Get("passed")]
        [Passed]
        public object Pass()
        {
            return new { Result = "before-passed" };
        }

        [Get("same-name")]
        [Rejected("rejected-get-same-name")]
        public object GetSameName()
        {
            return new { Result = "should-not-be-here" };
        }

        [Post("same-name")]
        [Rejected("rejected-post-same-name")]
        public object PostSameName()
        {
            return new { Result = "should-not-be-here" };
        }

        public class RejectedByInterfaceViewModel : IRejectedByInterfaceViewModel
        {
            public object Get()
            {
                // interface rejected attribute applies here
                return new { Result = "should-not-be-here" };
            }
        }

        public class RejectedByInterfaceAncestorViewModel : IRejectedByInterfaceAncestorViewModel
        {
            public object Get()
            {
                // interface ancestor rejected attribute applies here
                return new { Result = "should-not-be-here" };
            }
        }

        [Rejected("rejected-by-class")]
        public class RejectedByClassViewModel
        {
            [Get("before/rejected/by-class")]
            public object RejectByClass()
            {
                // if method attribute is absent, the class level attribute applies.
                return new { Result = "should-not-be-here" };
            }
        }

        [RouteInherit(typeof(RejectedByClassViewModel))]
        public class RejectByAncestorViewModel
        {
            [Get("before/rejected/by-ancestor")]
            public object Pass()
            {
                // the route inherited attribute applies here
                return new { Result = "should-not-be-here" };
            }
        }

        public class MultipleBeforeOnMethodViewModel
        {
            [MultipleBefore]
            [MultipleBefore]
            [Get("before/multiple-on-method")]
            public object Get()
            {
                return new { Result = "should-not-be-here" };
            }
        }

        [MultipleBefore]
        [MultipleBefore]
        public class MultipleBeforeOnClassViewModel
        {
            [Get("before/multiple-on-class")]
            public object Get()
            {
                return new { Result = "should-not-be-here" };
            }
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
            public override Response Process(TinyIoCContainer container, NancyContext context)
            {
                return null;
            }
        }

        [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
        private class MultipleBeforeAttribute : BeforeAttribute
        {
            public override Response Process(TinyIoCContainer container, NancyContext context)
            {
                return null;
            }
        }
    }
}
