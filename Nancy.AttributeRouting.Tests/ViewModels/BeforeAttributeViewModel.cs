﻿namespace Nancy.AttributeRouting.Tests.ViewModels
{
    using Nancy.Responses;
    using Nancy.TinyIoc;

    [RoutePrefix("before")]
    public class BeforeAttributeViewModel
    {
        [Get("rejected")]
        [Rejected]
        public virtual object Reject()
        {
            return new { Result = "should-not-be-here" };
        }

        [Get("passed")]
        [Passed]
        public virtual object Pass()
        {
            return new { Result = "before-passed" };
        }

        public class PassedChildViewModel : BeforeAttributeViewModel
        {
            [Get("child/passed")]
            [Passed]
            public override object Reject()
            {
                // the parent rejected attribute does not affect child.
                return new { Result = "passed-from-child" };
            }
        }

        [Rejected("rejected-from-child")]
        public class RejectedChildViewModel : BeforeAttributeViewModel
        {
            [Get("child/rejected")]
            public override object Pass()
            {
                // if method hook is absent, the class level hook applies.
                return base.Pass();
            }
        }

        [Rejected("rejected-from-nearest")]
        public class NearestChildViewModel : RejectedChildViewModel
        {
        }

        public class LeafChildViewModel : NearestChildViewModel
        {
            [Get("child/nearest")]
            public override object Pass()
            {
                // the nearest ancestor class level hook applies
                return base.Pass();
            }
        }

        private class RejectedAttribute : BeforeAttribute
        {
            private readonly string message;

            public RejectedAttribute()
                : this("before-rejected")
            {
            }

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
    }
}
