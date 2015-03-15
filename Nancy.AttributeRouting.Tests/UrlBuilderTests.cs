namespace Nancy.AttributeRouting.Tests
{
    using System;
    using System.Collections.Generic;
    using Nancy.AttributeRouting.Tests.ViewModels;
    using Nancy.Testing;
    using Xunit;

    public class UrlBuilderTests
    {
        private readonly Browser app = new Browser(new DefaultNancyBootstrapper());

        [Fact]
        public void GetUrl_from_constructor_should_return_URL()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(() => new MyViewModel());
            Assert.Equal("/my-view-model", url);
        }

        [Fact]
        public void GetUrl_from_parameter_constructor_should_return_URL()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(() => new MyViewModel("constructor-value"));
            Assert.Equal("/my-view-model/constructor-value", url);
        }

        [Fact]
        public void GetUrl_from_method_should_return_URL()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(v => v.Get());
            Assert.Equal("/my", url);
        }

        [Fact]
        public void GetUrl_should_throw_exception_when_method_has_two_attributes()
        {
            Assert.Throws<Exception>(
                () => Url.Builder.GetUrl<MyViewModel>(v => v.GetByTwoRoutings()));
        }

        [Fact]
        public void GetUrl_should_throw_exception_when_method_has_no_attributes()
        {
            Assert.Throws<Exception>(
                () => Url.Builder.GetUrl<MyViewModel>(v => v.GetWithoutRoutings()));
        }

        [Fact]
        public void GetUrl_with_object_parameters_should_return_URL()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(v => v.GetResult(null), new { value = "object-value" });
            Assert.Equal("/my/result/object-value", url);
        }

        [Fact]
        public void GetUrl_with_dictionary_parameters_should_return_URL()
        {
            var dictionary = new Dictionary<string, string>
            {
                { "value", "dictionary-value" }
            };

            string url = Url.Builder.GetUrl<MyViewModel>(v => v.GetResult(null), dictionary);
            Assert.Equal("/my/result/dictionary-value", url);
        }

        [Fact]
        public void GetUrl_with_direct_parameters_should_return_URL()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(v => v.GetResult("direct-value"));
            Assert.Equal("/my/result/direct-value", url);
        }

        [Fact]
        public void GetUrl_with_explicit_parameters_should_override_direct_parameters()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(v => v.GetResult("direct-value"), new { value = "explicit-value" });
            Assert.Equal("/my/result/explicit-value", url);
        }

        [Fact]
        public void GetUrl_with_extra_route_parameter_should_set_with_object_parameters()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(v => v.GetResultWithProperty("passed-value"), new { property = "passed-property" });
            Assert.Equal("/my/passed-property/result/passed-value", url);
        }

        [Fact]
        public void GetUrl_with_other_HTTP_method_attribute_should_return_URL()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(v => v.Put());
            Assert.Equal("/my", url);
        }

        [Fact]
        public void GetUrl_with_variable_should_return_URL()
        {
            string value = "variable-value";
            string url = Url.Builder.GetUrl<MyViewModel>(v => v.GetResult(value));
            Assert.Equal("/my/result/variable-value", url);
        }

        [Fact]
        public void GetUrl_with_computed_value_should_return_URL()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(v => v.GetResult(string.Format("{0}-{1}", "part1", "part2")));
            Assert.Equal("/my/result/part1-part2", url);
        }

        [Fact]
        public void GetUrl_with_non_string_type_should_return_URL()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            DateTime birth = DateTime.UtcNow;

            // Act
            string url = Url.Builder.GetUrl<ComplexViewModel>(v => v.GetComplexRoute(id, birth, 12, true));

            // Assert
            string expectedUrl = string.Format("/complex/non-string/12/True/{0}/{1}", id, birth);
            Assert.Equal(expectedUrl, url);
        }

        [Theory]
        [InlineData("Space Here", "Space%20Here")]
        [InlineData("中文", "%E4%B8%AD%E6%96%87")]
        public void GetUrl_with_special_characters_should_build_normalized_URL(string str, string expectedStr)
        {
            // Act
            string url = Url.Builder.GetUrl<ComplexViewModel>(v => v.GetWithSpecialCharacters(str));

            // Assert
            Assert.Equal("/complex/special/" + expectedStr, url);
        }

        public class Url : NancyModule
        {
            public Url(IUrlBuilder urlbuilder)
            {
                Builder = urlbuilder;
            }

            public static IUrlBuilder Builder { get; private set; }
        }
    }
}
