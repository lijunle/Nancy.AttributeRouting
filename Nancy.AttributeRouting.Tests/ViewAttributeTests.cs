namespace Nancy.AttributeRouting.Tests
{
    using System.Collections.Generic;
    using Nancy.Testing;
    using Xunit;

    public class ViewAttributeTests
    {
        private static readonly Browser Browser = new Browser(new DefaultNancyBootstrapper());

        [Fact]
        public void View_attribute_should_point_out_file_location()
        {
            // Act
            BrowserResponse response = Browser.Get("/html", with => with.Accept("text/html"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string body = response.Body.AsString();
            Assert.Contains("Hello world!", body);
        }

        [Fact]
        public void View_attribute_should_not_affect_JSON_request()
        {
            // Act
            BrowserResponse response = Browser.Get("/html", with => with.Accept("application/json"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = (IDictionary<string, object>)response.Body.DeserializeJson<object>();
            Assert.Equal("Hello world!", json["Message"]);

            string content = response.Body.AsString();
            Assert.StartsWith("{", content);
            Assert.EndsWith("}", content);
            Assert.Contains("Hello world!", content);
        }

        [Fact]
        public void ViewPrefix_attribute_should_find_prepend_file_location()
        {
            // Act
            BrowserResponse response = Browser.Get("/html/inner", with => with.Accept("text/html"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string body = response.Body.AsString();
            Assert.Contains("Get inner message.", body);
        }
    }
}
