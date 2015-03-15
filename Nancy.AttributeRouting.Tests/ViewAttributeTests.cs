namespace Nancy.AttributeRouting.Tests
{
    using System.Collections.Generic;
    using Nancy.Testing;
    using Xunit;

    public class ViewAttributeTests
    {
        private static readonly Browser Browser = new Browser(new DefaultNancyBootstrapper());

        [Theory]
        [InlineData("/html", "Hello world!")]
        [InlineData("/html/inner", "Get inner message.")]
        [InlineData("/html/deeper", "Get deeper message.")]
        [InlineData("/html/inner/inherit", "Inherit prefix message.")]
        public void View_attribute_should_point_out_file_location(string url, string expectedContent)
        {
            // Act
            BrowserResponse response = Browser.Get(url, with => with.Accept("text/html"));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string content = response.Body.AsString();
            Assert.Contains(expectedContent, content);
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
    }
}
