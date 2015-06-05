namespace Nancy.AttributeRouting.Tests
{
    using System;
    using System.Collections.Generic;
    using Nancy.AttributeRouting.Tests.ViewModels;
    using Nancy.Testing;
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
        [InlineData("/complex/get/optional", "Name", "default")]
        [InlineData("/complex/get/optional/override-name", "Name", "override-name")]
        [InlineData("/complex/special/Space%20Here", "Str", "Space Here")]
        [InlineData("/complex/special/%E4%B8%AD%E6%96%87", "Str", "中文")]
        [InlineData("/route-prefix", "Result", "value")]
        [InlineData("/route-prefix/type-prefix", "Result", "from-type-prefix")]
        [InlineData("/route-prefix/child-prefix", "Result", "from-child-prefix")]
        [InlineData("/route-prefix/child-prefix/grandchild", "Result", "from-grandchild")]
        [InlineData("/route-prefix/inherit", "Result", "inherit-not-affect-routing")]
        [InlineData("/route-prefix/custom-prefix/custom-value", "Result", "custom-prefix.custom-value")]
        [InlineData("/before", "Result", "reject-root-path")]
        [InlineData("/before/rejected", "Result", "before-rejected")]
        [InlineData("/before/passed", "Result", "before-passed")]
        [InlineData("/before/child/rejected", "Result", "rejected-from-child")]
        [InlineData("/before/child/passed", "Result", "passed-from-child")]
        [InlineData("/before/child/nearest", "Result", "rejected-from-nearest")]
        public void Attribute_routing_should_accept_get_request(string path, string expectedKey, string expectedValue)
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
        public void Complex_primitive_type_should_be_parsed_into_method(string path, string expectedBody)
        {
            // Act
            BrowserResponse response = Browser.Get(path, with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedBody, response.Body.AsString());
        }

        [Fact]
        public void Complex_primitive_type_should_be_passed_into_method()
        {
            // Arrange
            int age = 98;
            bool graduated = true;
            string id = "D696DBC7-A14B-405B-B4B5-6CCBE9309FFF";
            string birth = "2001-02-03T04:05:06Z";
            string url = string.Format("/complex/non-string/{0}/{1}/{2}/{3}", age, graduated, id, birth);

            // Act
            BrowserResponse response = Browser.Get(url, with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = (IDictionary<string, object>)response.Body.DeserializeJson<object>();
            Assert.Equal(age, body["Age"]);
            Assert.Equal(graduated, body["Graduated"]);
            Assert.Equal(Guid.Parse(id), Guid.Parse((string)body["Id"]));
            Assert.Equal(DateTime.Parse(birth), DateTime.Parse((string)body["Birth"]));
        }

        [Fact]
        public void Resolver_should_inject_component_to_constructor()
        {
            // Act
            BrowserResponse response = Browser.Get("/complex/with-injection", with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var body = (IDictionary<string, object>)response.Body.DeserializeJson<object>();
            Assert.Equal("/complex/with-injection", body["Url"]);
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
    }
}
