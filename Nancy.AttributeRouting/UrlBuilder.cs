namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Nancy.Routing;

    /// <summary>
    /// The URL builder interface. It accepts an expression related to a view model, then construct
    /// the URL string corresponding to the view model call.
    /// </summary>
    public interface IUrlBuilder
    {
        /// <summary>
        /// Gets URL from the method call of a <typeparamref name="T"/> instance.
        /// </summary>
        /// <param name="expression">
        /// The method call lambda expression from a <typeparamref name="T"/> instance.
        /// </param>
        /// <typeparam name="T">A view model class decorates with routing attributes.</typeparam>
        /// <returns>The constructed URL string.</returns>
        string GetUrl<T>(Expression<Func<T, object>> expression);

        /// <summary>
        /// Gets URL from the method call of a <typeparamref name="T"/> instance.
        /// </summary>
        /// <param name="expression">
        /// The method call lambda expression from a <typeparamref name="T"/> instance.
        /// </param>
        /// <param name="parameters">
        /// The parameter object to provide additional information to construct the URL. The object
        /// will be converted into dictionary then invoke another overload.
        /// </param>
        /// <typeparam name="T">A view model class decorates with routing attributes.</typeparam>
        /// <returns>The constructed URL string.</returns>
        string GetUrl<T>(Expression<Func<T, object>> expression, object parameters);

        /// <summary>
        /// Gets URL from the method call of a <typeparamref name="T"/> instance.
        /// </summary>
        /// <param name="expression">
        /// The method call lambda expression from a <typeparamref name="T"/> instance.
        /// </param>
        /// <param name="parameters">
        /// The parameter dictionary to provide additional information to construct the URL. The
        /// dictionary key is the routing template placeholder, the dictionary value is the actual
        /// value to replace the placeholder.
        /// </param>
        /// <typeparam name="T">A view model class decorates with routing attributes.</typeparam>
        /// <returns>The constructed URL string.</returns>
        string GetUrl<T>(Expression<Func<T, object>> expression, IDictionary<string, string> parameters);
    }

    /// <inheritdoc/>
    public class UrlBuilder : IUrlBuilder
    {
        private static readonly string ExpressionNotValidMessage =
            "GetUrl expression body must be a method call (e.g., `UrlBuilder.GetUrl<SomeViewModel>(m => m.Get())`).";

        private readonly IRouteSegmentExtractor segmentExtractor;

        /// <inheritdoc/>
        public UrlBuilder(IRouteSegmentExtractor segmentExtractor)
        {
            this.segmentExtractor = segmentExtractor;
        }

        /// <inheritdoc/>
        public string GetUrl<T>(Expression<Func<T, object>> expression)
        {
            return this.GetUrl<T>(expression, new Dictionary<string, string>());
        }

        /// <inheritdoc/>
        public string GetUrl<T>(Expression<Func<T, object>> expression, object parameters)
        {
            return this.GetUrl<T>(expression, parameters.ToDictionary());
        }

        /// <inheritdoc/>
        public string GetUrl<T>(Expression<Func<T, object>> expression, IDictionary<string, string> parameters)
        {
            var methodCall = expression.Body as MethodCallExpression;
            if (methodCall == null)
            {
                throw new ArgumentException(ExpressionNotValidMessage, nameof(expression));
            }

            string path = RouteAttribute.GetPath(methodCall.Method);
            IDictionary<string, string> methodParameters = methodCall.ToParameterDictionary();
            IDictionary<string, string> pathParameters = parameters.Merge(methodParameters);

            string url = this.segmentExtractor.GenerateUrl(path, pathParameters);
            return url;
        }
    }
}
