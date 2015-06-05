namespace Nancy.AttributeRouting.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Nancy.AttributeRouting.Tests.ViewModels;
    using Nancy.Testing;
    using Xunit;

    public class UrlBuilderTests
    {
        private readonly Browser app = new Browser(new DefaultNancyBootstrapper());

        public interface ITestCase
        {
            void Run();
        }

        public static IEnumerable<object[]> TestCases
        {
            get
            {
                // empty string is not a valid URL, should return single slash
                yield return new TestCase<MyViewModel>(
                    m => m.Index(),
                    "/");

                yield return new TestCase<MyViewModel>(
                    m => m.GetWithDefaultProperty(),
                    "/my-view-model");

                yield return new TestCase<MyViewModel>(
                    m => m.GetWithProperty("constructor-value"),
                    "/my-view-model/constructor-value");

                yield return new TestCase<MyViewModel>(
                    m => m.GetResult(),
                    "/my/result");

                yield return new TestCase<MyViewModel>(
                    m => m.GetResult(null),
                    new { value = "object-value" },
                    "/my/result/object-value");

                yield return new TestCase<MyViewModel>(
                    m => m.GetResult(null),
                    new Dictionary<string, string> { { "value", "dictionary-value" } },
                    "/my/result/dictionary-value");

                yield return new TestCase<MyViewModel>(
                    m => m.GetResult("direct-value"),
                    "/my/result/direct-value");

                // explicit value will override direct value
                yield return new TestCase<MyViewModel>(
                    m => m.GetResult("direct-value"),
                    new { value = "explicit-value" },
                    "/my/result/explicit-value");

                yield return new TestCase<HttpMethodViewModel>(
                    m => m.Delete(),
                    "/my");

                yield return new TestCase<HttpMethodViewModel>(
                    m => m.Get(),
                    "/my");

                yield return new TestCase<HttpMethodViewModel>(
                    m => m.Options(),
                    "/my");

                yield return new TestCase<HttpMethodViewModel>(
                    m => m.Patch(),
                    "/my");

                yield return new TestCase<HttpMethodViewModel>(
                    m => m.Post(),
                    "/my");

                yield return new TestCase<HttpMethodViewModel>(
                    m => m.Put(),
                    "/my");

                yield return new TestCase<ComplexViewModel>(
                    m => m.GetComplexRoute(
                        Guid.Parse("ED1527C7-FEE5-40B2-B228-5EAD3B2F55A4"),
                        DateTime.Parse("2001-02-03T04:05:06.0789"),
                        12,
                        true),
                    "/complex/non-string/12/True/ed1527c7-fee5-40b2-b228-5ead3b2f55a4/" + Uri.EscapeDataString("2/3/2001 4:05:06 AM"));

                // expression tree does not allow optional parameter, no way to get routing optional parameter
                yield return new TestCase<ComplexViewModel>(
                    m => m.GetWithOptionalParameter(null),
                    "/complex/get/optional/");

                yield return new TestCase<ComplexViewModel>(
                    m => m.GetWithOptionalParameter("passed-name"),
                    "/complex/get/optional/passed-name");

                yield return new TestCase<ComplexViewModel>(
                    m => m.GetWithSpecialCharacters("Space Here"),
                    "/complex/special/Space%20Here");

                yield return new TestCase<ComplexViewModel>(
                    m => m.GetWithSpecialCharacters("中文"),
                    "/complex/special/%E4%B8%AD%E6%96%87");

                yield return new TestCase<RoutePrefixViewModel>(
                    m => m.Get(),
                    "/route-prefix");

                yield return new TestCase<RoutePrefixViewModel.TypePrefixViewModel>(
                    m => m.GetTypePrefix(),
                    "/route-prefix/type-prefix");

                yield return new TestCase<RoutePrefixViewModel.ChildPrefixViewModel>(
                    m => m.GetChildPrefix(),
                    "/route-prefix/child-prefix");

                yield return new TestCase<RoutePrefixViewModel.GrandchildViewModel>(
                    m => m.GetGrandchild(),
                    "/route-prefix/child-prefix/grandchild");

                yield return new TestCase<RoutePrefixViewModel.InheritViewModel>(
                    m => m.GetInherit(),
                    "/route-prefix/inherit");

                yield return new TestCase<RoutePrefixViewModel.PlaceholderViewModel>(
                    m => m.GetResultWithProperty("passed-value"),
                    new { prefix = "passed-prefix" },
                    "/route-prefix/passed-prefix/passed-value");
            }
        }

        [Theory]
        [MemberData("TestCases")]
        public void Test_URL_builder(ITestCase testCase)
        {
            testCase.Run();
        }

        [Fact]
        public void GetUrl_should_throw_exception_when_method_has_two_attributes()
        {
            Assert.Throws<Exception>(
                () => Url.Builder.GetUrl<MyViewModel>(m => m.GetByTwoRoutings()));
        }

        [Fact]
        public void GetUrl_should_throw_exception_when_method_has_no_attributes()
        {
            Assert.Throws<Exception>(
                () => Url.Builder.GetUrl<MyViewModel>(m => m.GetWithoutRoutings()));
        }

        public class Url : NancyModule
        {
            public Url(IUrlBuilder urlbuilder)
            {
                Builder = urlbuilder;
            }

            public static IUrlBuilder Builder { get; private set; }
        }

        public class TestCase<T> : ITestCase where T : class
        {
            private readonly Expression<Func<T, object>> expression;

            private readonly IDictionary<string, string> dictionary;

            private readonly object parameters;

            private readonly string url;

            public TestCase(Expression<Func<T, object>> expression, string url)
            {
                this.expression = expression;
                this.url = url;
            }

            public TestCase(Expression<Func<T, object>> expression, object parameters, string url)
            {
                this.expression = expression;
                this.parameters = parameters;
                this.url = url;
            }

            public TestCase(Expression<Func<T, object>> expression, IDictionary<string, string> dictionary, string url)
            {
                this.expression = expression;
                this.dictionary = dictionary;
                this.url = url;
            }

            public static implicit operator object[](TestCase<T> testCase)
            {
                return new object[] { testCase };
            }

            public void Run()
            {
                // Act
                string url = this.parameters != null
                    ? Url.Builder.GetUrl<T>(this.expression, this.parameters)
                    : this.dictionary != null
                    ? Url.Builder.GetUrl<T>(this.expression, this.dictionary)
                    : Url.Builder.GetUrl<T>(this.expression);

                // Assert
                Assert.Equal(this.url, url);
            }

            public override string ToString()
            {
                return string.Format("{0} {1}", typeof(T).Name, this.expression);
            }
        }
    }
}
