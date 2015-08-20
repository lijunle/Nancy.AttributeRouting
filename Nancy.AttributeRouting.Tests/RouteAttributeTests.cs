namespace Nancy.AttributeRouting.Tests
{
    using System;
    using System.Collections.Generic;
    using Nancy.AttributeRouting.Exceptions;
    using Nancy.AttributeRouting.Tests.ViewModels;
    using Nancy.Extensions;
    using Nancy.Testing;
    using Nancy.TinyIoc;
    using Xunit;

    public class RouteAttributeTests
    {
        private static readonly Browser Browser = new Browser(new DefaultNancyBootstrapper());

        [Theory]
        [InlineData("/", "Result", "Index")]
        [InlineData("/my-view-model", "Property", "Value")]
        [InlineData("/my-view-model/my-property", "Property", "my-property")]
        [InlineData("/nested-view-model", "NestedProperty", "NestedValue")]
        [InlineData("/my/result", "Result", "MyResult")]
        [InlineData("/nested/result", "Result", "NestedResult")]
        [InlineData("/my/result/custom-value", "Result", "custom-value")]
        [InlineData("/nested/result/custom-value", "Result", "nested-custom-value")]
        [InlineData("/my/1", "Result", "TheSameResultFromTwoRoutings")]
        [InlineData("/my/2", "Result", "TheSameResultFromTwoRoutings")]
        [InlineData("/complex/with-injection", "Url", "/complex/with-injection")]
        [InlineData("/complex/get/optional", "Name", "default")]
        [InlineData("/complex/get/optional/override-name", "Name", "override-name")]
        [InlineData("/complex/get/missing", "Name", "default-name")]
        [InlineData("/complex/get/missing/provided-name", "Name", "provided-name")]
        [InlineData("/complex/special/Space%20Here", "Str", "Space Here")]
        [InlineData("/complex/special/%E4%B8%AD%E6%96%87", "Str", "中文")]
        [InlineData("/route-prefix", "Result", "value")]
        [InlineData("/route-prefix/type-prefix", "Result", "from-type-prefix")]
        [InlineData("/route-prefix/child-prefix", "Result", "from-child-prefix")]
        [InlineData("/route-prefix/child-prefix/grandchild", "Result", "from-grandchild")]
        [InlineData("/route-prefix/inherit", "Result", "inherit-not-affect-routing")]
        [InlineData("/route-prefix/custom-prefix/custom-value", "Result", "custom-prefix.custom-value")]
        [InlineData("/route-prefix/constructor-prefix", "Result", "parameters-not-pass-to-constructor")]
        [InlineData("/before", "Result", "reject-root-path")]
        [InlineData("/before/passed", "Result", "before-passed")]
        [InlineData("/before/rejected", "Result", "before-rejected")]
        [InlineData("/before/rejected/by-interface", "Result", "rejected-by-interface")]
        [InlineData("/before/rejected/by-interface-ancestor", "Result", "rejected-by-interface")]
        [InlineData("/before/rejected/by-class", "Result", "rejected-by-class")]
        [InlineData("/before/rejected/by-ancestor", "Result", "rejected-by-class")]
        [InlineData("/interface", "Result", "query-from-interface")]
        [InlineData("/interface/passed-to-interface", "Result", "passed-to-interface")]
        [InlineData("/interface/child", "Result", "from-child-interface")]
        [InlineData("/interface/child-of-class", "Result", "from-child-of-class")]
        public void Attribute_routing_should_accept_get_request(
            string path,
            string expectedKey,
            string expectedValue)
        {
            // Act
            BrowserResponse response = Browser.Get(path, with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = (IDictionary<string, object>)response.Body.DeserializeJson<object>();
            Assert.Equal(expectedValue, body[expectedKey]);
        }

        [Theory]
        [InlineData("DELETE", "MyDelete")]
        [InlineData("GET", "MyGet")]
        [InlineData("OPTIONS", "MyOptions")]
        [InlineData("PATCH", "MyPatch")]
        [InlineData("POST", "MyPost")]
        [InlineData("PUT", "MyPut")]
        public void Attribute_routing_should_accept_all_HTTP_methods(string httpMethod, string expectedResult)
        {
            // Act
            BrowserResponse response = Browser.HandleRequest(httpMethod, "/my", with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResult, response.Body.AsString());
        }

        [Theory]
        [InlineData("/complex/guid/ED1527C7-FEE5-40B2-B228-5EAD3B2F55A4", "ed1527c7-fee5-40b2-b228-5ead3b2f55a4")]
        [InlineData("/complex/datetime/2001-02-03T04%3A05%3A06.0789", "2/3/2001 4:05:06 AM")]
        [InlineData("/complex/int/000987", "987")]
        [InlineData("/complex/boolean/true", "True")]
        [InlineData("/complex/regex/my-name", "my-name")]
        public void Complex_primitive_type_should_be_parsed_into_method(string path, string expectedBody)
        {
            // Act
            BrowserResponse response = Browser.Get(path, with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedBody, response.Body.AsString());
        }

        [Fact]
        public void Resolver_should_inject_request_data_to_method()
        {
            // Arrange
            var form = new ComplexViewModel.Form
            {
                User = "complex-user",
                Password = "complex-password"
            };

            // Act
            BrowserResponse response = Browser.Post(
                "/complex/post/form",
                with => with.JsonBody<ComplexViewModel.Form>(form));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("complex-user=complex-password", response.Body.AsString());
        }

        [Theory]
        [InlineData("GET", "rejected-get-same-name")]
        [InlineData("POST", "rejected-post-same-name")]
        public void Before_hook_should_handle_same_routing_path(string httpMethod, string expectedValue)
        {
            // Act
            BrowserResponse response = Browser.HandleRequest(
                httpMethod, "/before/same-name", with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = (IDictionary<string, object>)response.Body.DeserializeJson<object>();
            Assert.Equal(expectedValue, body["Result"]);
        }

        [Theory]
        [InlineData("/before/multiple-on-method")]
        [InlineData("/before/multiple-on-class")]
        [InlineData("/before/multiple-on-interface")]
        public void Decorate_multiple_before_attributes_should_throw_exception(string url)
        {
            // Act
            BrowserResponse response = Browser.Get(url, with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            ISet<Type> validExceptionTypes = new HashSet<Type>
            {
                typeof(RequestExecutionException),
                typeof(MultipleBeforeAttributeException),
            };

            ISet<Type> exceptionTypes = GetExceptionTypes(response);
            Assert.Subset(validExceptionTypes, exceptionTypes);
        }

        [Fact]
        public void Interface_should_register_as_multi_instance()
        {
            // Act
            int number1 = RequestInstaceNumber(Browser);
            int number2 = RequestInstaceNumber(Browser);

            // Assert
            Assert.Equal(1, number1);
            Assert.Equal(2, number2);
        }

        [Fact]
        public void Request_interface_route_without_implementation_should_return_500()
        {
            // Act
            BrowserResponse response = Browser.Get("/interface/without-implementation");

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            ISet<Type> validExceptionTypes = new HashSet<Type>
            {
                typeof(RequestExecutionException),
                typeof(TinyIoCResolutionException),
            };

            ISet<Type> exceptionTypes = GetExceptionTypes(response);
            Assert.Subset(validExceptionTypes, exceptionTypes);
        }

        private static ISet<Type> GetExceptionTypes(BrowserResponse response)
        {
            var exceptionTypes = new HashSet<Type>();

            var exception = response.Context.GetException();
            while (exception != null)
            {
                exceptionTypes.Add(exception.GetType());
                exception = exception.InnerException;
            }

            return exceptionTypes;
        }

        private static int RequestInstaceNumber(Browser browser)
        {
            BrowserResponse response = browser.Get(
                "/interface/number-of-instance",
                with => with.Accept("application/json"));

            int number = response.Body.DeserializeJson<int>();
            return number;
        }
    }
}
