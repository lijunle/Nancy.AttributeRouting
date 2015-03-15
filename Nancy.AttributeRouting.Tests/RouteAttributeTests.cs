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
        [InlineData("/my-view-model", "Property", "Value")]
        [InlineData("/my-view-model/my-property", "Property", "my-property")]
        [InlineData("/nested-view-model", "NestedProperty", "NestedValue")]
        [InlineData("/my/result", "Result", "MyResult")]
        [InlineData("/nested/result", "Result", "NestedResult")]
        [InlineData("/my/result/custom-value", "Result", "custom-value")]
        [InlineData("/my/property/result/custom-value", "Result", "property.custom-value")]
        [InlineData("/nested/result/custom-value", "Result", "nested-custom-value")]
        [InlineData("/my/1", "Result", "TheSameResultFromTwoRoutings")]
        [InlineData("/my/2", "Result", "TheSameResultFromTwoRoutings")]
        [InlineData("/complex/special/Space%20Here", "Str", "Space Here")]
        [InlineData("/complex/special/%E4%B8%AD%E6%96%87", "Str", "中文")]
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
        public void Resolver_should_inject_component_to_method()
        {
            // Act
            BrowserResponse response = Browser.Get("/complex/redirect/to/url");

            // Assert
            Assert.Equal(HttpStatusCode.SeeOther, response.StatusCode);
            Assert.Equal("/url", response.Headers["Location"]);
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

        [Fact]
        public void Resolve_should_handle_optional_parameters()
        {
            // Act
            BrowserResponse response = Browser.Get(
                "/complex/get/optional",
                with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OptionalName", response.Body.AsString());
        }

        [Fact]
        public void Resolve_should_override_optional_parameters()
        {
            // Act
            BrowserResponse response = Browser.Get(
                "/complex/get/optional/override-name",
                with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("override-name", response.Body.AsString());
        }
    }
}
