namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Nancy.Extensions;
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

    /// <summary>
    /// The URL builder. It accepts an expression related to a view model, then construct the URL
    /// string corresponding to the view model call.
    /// </summary>
    public class UrlBuilder : IUrlBuilder
    {
        private static readonly string ExpressionNotValidMessage =
            "GetUrl expression body must be a method call (e.g., `UrlBuilder.GetUrl<SomeViewModel>(m => m.Get())`).";

        private readonly IRouteSegmentExtractor segmentExtractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBuilder"/> class.
        /// </summary>
        /// <param name="segmentExtractor">
        /// The routing segment extractor. The Nancy IoC container will resolve this for us.
        /// </param>
        public UrlBuilder(IRouteSegmentExtractor segmentExtractor)
        {
            this.segmentExtractor = segmentExtractor;
        }

        /// <summary>
        /// Gets URL from the method call of a <typeparamref name="T"/> instance.
        /// </summary>
        /// <param name="expression">
        /// The method call lambda expression from a <typeparamref name="T"/> instance.
        /// </param>
        /// <typeparam name="T">A view model class decorates with routing attributes.</typeparam>
        /// <returns>The constructed URL string.</returns>
        public string GetUrl<T>(Expression<Func<T, object>> expression)
        {
            return this.GetUrl<T>(expression, new Dictionary<string, string>());
        }

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
        public string GetUrl<T>(Expression<Func<T, object>> expression, object parameters)
        {
            return this.GetUrl<T>(expression, parameters.ToDictionary());
        }

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
        public string GetUrl<T>(Expression<Func<T, object>> expression, IDictionary<string, string> parameters)
        {
            var methodCall = expression.Body as MethodCallExpression;
            if (methodCall == null)
            {
                throw new Exception(ExpressionNotValidMessage);
            }

            string path = RouteAttribute.GetPath(methodCall.Method);

            IDictionary<string, string> methodParameters =
                MethodCallToDictionary(methodCall.Method, methodCall.Arguments);

            return ExtractParametersToPath(this.segmentExtractor, path, parameters.Merge(methodParameters));
        }

        private static IDictionary<string, string> MethodCallToDictionary(
            MethodBase method,
            IReadOnlyCollection<Expression> arguments)
        {
            IEnumerable<string> paramNames =
                method.GetParameters().Select(parameter => parameter.Name);

            IEnumerable<object> paramValues =
                arguments.Select(argument => Expression.Lambda(argument).Compile().DynamicInvoke());

            Dictionary<string, string> result =
                paramNames.Zip(paramValues, (name, value) => Tuple.Create(name, value))
                    .ToDictionary(tuple => tuple.Item1, tuple => Convert.ToString(tuple.Item2));

            return result;
        }

        #region Inspired From Nancy.Linker https://github.com/horsdal/Nancy.Linker

        private static string ExtractParametersToPath(
            IRouteSegmentExtractor segmentExtractor,
            string path,
            IDictionary<string, string> parameters)
        {
            IEnumerable<string> segmentValues =
                segmentExtractor.Extract(path)
                    .Select(segment => GetSegmentValue(segment, parameters));

            return string.Concat("/", string.Join("/", segmentValues));
        }

        private static string GetSegmentValue(string segment, IDictionary<string, string> parameters)
        {
            string result = TryGetParameterValue(segment, parameters);
            if (result == null)
            {
                string message = string.Format("Value for path segment {0} missing", segment);
                throw new ArgumentException(message, "segment");
            }

            return Uri.EscapeDataString(result);
        }

        private static string TryGetParameterValue(string segment, IDictionary<string, string> parameters)
        {
            if (segment.StartsWith("("))
            {
                return GetRegexSegmentValue(segment, parameters);
            }
            else if (segment.IsParameterized())
            {
                return GetParameterizedSegmentValue(segment, parameters);
            }
            else if (IsContrainedParameter(segment))
            {
                return GetConstrainedParamterValue(segment, parameters);
            }
            else
            {
                return segment;
            }
        }

        private static string GetRegexSegmentValue(string segment, IDictionary<string, string> parameters)
        {
            return Regex.Replace(segment, @"\(\?<(?<name>.*?)>.*?\)", x => parameters[x.Groups["name"].Value]);
        }

        private static string GetConstrainedParamterValue(string segment, IDictionary<string, string> parameters)
        {
            string res;
            parameters.TryGetValue(segment.Substring(1, segment.IndexOf(':') - 1).Trim(), out res);
            return res;
        }

        private static bool IsContrainedParameter(string segment)
        {
            return segment.Contains(':');
        }

        private static string GetParameterizedSegmentValue(string segment, IDictionary<string, string> parameters)
        {
            string res;
            var segmentInfo = segment.GetParameterDetails().Single();

            if (!segmentInfo.IsOptional || string.IsNullOrEmpty(segmentInfo.DefaultValue))
            {
                parameters.TryGetValue(segmentInfo.Name, out res);
            }
            else
            {
                res = parameters.ContainsKey(segmentInfo.Name)
                  ? parameters[segmentInfo.Name]
                  : segmentInfo.DefaultValue;
            }

            return res;
        }

        #endregion Inspired From Nancy.Linker https://github.com/horsdal/Nancy.Linker
    }
}
