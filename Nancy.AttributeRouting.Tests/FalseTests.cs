namespace Nancy.AttributeRouting.Tests
{
    using System;
    using Exceptions;
    using Testing;
    using TinyIoc;
    using ViewModels;
    using Xunit;

    public class FalseTests
    {
        [Fact]
        public void Two_same_routing_paths_should_throw_exception()
        {
            Exception exception = null;

            try
            {
                new Browser(
                    new TestBootstrapper(DuplicatedRoutingPathsViewModel.TypeProvider));
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                Assert.NotNull(exception);
                Assert.IsType<TinyIoCResolutionException>(exception);

                Assert.NotNull(exception.InnerException);
                Assert.IsType<DuplicatedRoutingPathsException>(exception.InnerException);
            }
        }
    }
}
