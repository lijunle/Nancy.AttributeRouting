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
            string url = Url.Builder.GetUrl<MyViewModel>(m => m.GetWithDefaultProperty());
            Assert.Equal("/my-view-model", url);
        }

        [Fact]
        public void GetUrl_from_parameter_constructor_should_return_URL()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(m => m.GetWithProperty("constructor-value"));
            Assert.Equal("/my-view-model/constructor-value", url);
        }

        [Fact]
        public void GetUrl_from_method_should_return_URL()
        {
            string url = Url.Builder.GetUrl<HttpMethodViewModel>(v => v.Get());
            Assert.Equal("/my", url);
        }

        [Fact]
        public void GetUrl_from_index_should_return_valid_URL()
        {
            string url = Url.Builder.GetUrl<MyViewModel>(v => v.Index());
            Assert.Equal("/", url); // empty string is not a valid URL, should return single slash
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
        public void GetUrl_with_other_HTTP_method_attribute_should_return_URL()
        {
            string url = Url.Builder.GetUrl<HttpMethodViewModel>(v => v.Put());
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
            string expectedUrl = string.Format(
                "/complex/non-string/12/True/{0}/{1}", id, Uri.EscapeDataString(birth.ToString()));

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

        [Fact]
        public void GetUrl_should_build_a_full_URL_from_RoutePrefix_0()
        {
            // Act
            string url = Url.Builder.GetUrl<RoutePrefixViewModel>(m => m.Get());

            // Assert
            Assert.Equal("/route-prefix", url);
        }

        [Fact]
        public void GetUrl_should_build_a_full_URL_from_RoutePrefix_1()
        {
            // Act
            string url = Url.Builder.GetUrl<RoutePrefixViewModel.InnerPrefixViewModel>(m => m.GetInnerPrefix());

            // Assert
            Assert.Equal("/route-prefix/inner", url);
        }

        [Fact]
        public void GetUrl_should_build_a_full_URL_from_RoutePrefix_2()
        {
            // Act
            string url = Url.Builder.GetUrl<RoutePrefixViewModel.InheritPrefixViewModel>(m => m.GetInheritPrefix());

            // Assert
            Assert.Equal("/route-prefix/inherit", url);
        }

        [Fact]
        public void GetUrl_should_build_a_full_URL_from_RoutePrefix_3()
        {
            // Act
            string url = Url.Builder.GetUrl<RoutePrefixViewModel.InheritInnerViewModel>(m => m.GetInheritInner());

            // Assert
            Assert.Equal("/route-prefix/inner/inherit", url);
        }

        [Fact]
        public void GetUrl_with_extra_route_parameter_should_set_parameters_to_route_prefix()
        {
            string url = Url.Builder.GetUrl<RoutePrefixViewModel.PlaceholderViewModel>(v => v.GetResultWithProperty("passed-value"), new { prefix = "passed-prefix" });
            Assert.Equal("/route-prefix/passed-prefix/passed-value", url);
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
