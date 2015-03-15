namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Nancy.Extensions;
    using Nancy.Routing;

    public interface IUrlBuilder
    {
        string GetUrl<T>(Expression<Func<T>> expression);

        string GetUrl<T>(Expression<Func<T>> expression, object parameters);

        string GetUrl<T>(Expression<Func<T>> expression, IDictionary<string, string> parameters);

        string GetUrl<T>(Expression<Func<T, object>> expression);

        string GetUrl<T>(Expression<Func<T, object>> expression, object parameters);

        string GetUrl<T>(Expression<Func<T, object>> expression, IDictionary<string, string> parameters);
    }

    public class UrlBuilder : IUrlBuilder
    {
        private static readonly string ExpressionNotValidMessage =
            "GetUrl expression body must be a method call (e.g., `UrlBuilder.GetUrl<Model>(model => model.Get())`)," +
            " or new constructor call (e.g., `UrlBuilder.GetUrl<Model>(() => new Model())`).";

        private readonly IRouteSegmentExtractor segmentExtractor;

        public UrlBuilder(IRouteSegmentExtractor segmentExtractor)
        {
            this.segmentExtractor = segmentExtractor;
        }

        public string GetUrl<T>(Expression<Func<T>> expression)
        {
            return this.GetUrl<T>(expression, new Dictionary<string, string>());
        }

        public string GetUrl<T>(Expression<Func<T>> expression, object parameters)
        {
            IDictionary<string, string> dictionary = ObjectToDictionary(parameters);
            return this.GetUrl<T>(expression, dictionary);
        }

        public string GetUrl<T>(Expression<Func<T>> expression, IDictionary<string, string> parameters)
        {
            var newCall = expression.Body as NewExpression;
            if (newCall == null)
            {
                throw new Exception(ExpressionNotValidMessage);
            }

            string path = GetRoutePath(newCall.Constructor);

            IDictionary<string, string> constructorParameters =
                MethodCallToDictionary(newCall.Constructor, newCall.Arguments);

            return ExtractParametersToPath(this.segmentExtractor, path, Merge(constructorParameters, parameters));
        }

        public string GetUrl<T>(Expression<Func<T, object>> expression)
        {
            return this.GetUrl<T>(expression, new Dictionary<string, string>());
        }

        public string GetUrl<T>(Expression<Func<T, object>> expression, object parameters)
        {
            IDictionary<string, string> dictionary = ObjectToDictionary(parameters);
            return this.GetUrl<T>(expression, dictionary);
        }

        public string GetUrl<T>(Expression<Func<T, object>> expression, IDictionary<string, string> parameters)
        {
            var methodCall = expression.Body as MethodCallExpression;
            if (methodCall == null)
            {
                throw new Exception(ExpressionNotValidMessage);
            }

            string path = GetRoutePath(methodCall.Method);

            IDictionary<string, string> methodParameters =
                MethodCallToDictionary(methodCall.Method, methodCall.Arguments);

            return ExtractParametersToPath(this.segmentExtractor, path, Merge(methodParameters, parameters));
        }

        private static string GetRoutePath(MethodBase method)
        {
            IEnumerable<CustomAttributeData> data = method.CustomAttributes
                .Where(attr => typeof(RouteAttribute).IsAssignableFrom(attr.AttributeType));

            if (data.Count() == 0)
            {
                string message = string.Format(
                    "Does not find any route attribute with method {0}.",
                    method.Name);

                throw new Exception(message);
            }
            else if (data.Count() > 1)
            {
                string message = string.Format(
                    "Method {0} associates with more than one route attributes. Only one is allowed.",
                    method.Name);

                throw new Exception(message);
            }

            return (string)data.First().ConstructorArguments.First().Value;
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

        private static IDictionary<string, string> ObjectToDictionary(object parameters)
        {
            return TypeDescriptor.GetProperties(parameters)
                .OfType<PropertyDescriptor>()
                .ToDictionary(p => p.Name, p => Convert.ToString(p.GetValue(parameters)));
        }

        private static IDictionary<string, string> Merge(
            IDictionary<string, string> dict1,
            IDictionary<string, string> dict2)
        {
            dict2.ToList().ForEach(kvp => dict1[kvp.Key] = kvp.Value);
            return dict1;
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

            return string.Join("/", new[] { string.Empty }.Concat(segmentValues));
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
