namespace Nancy.AttributeRouting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Nancy.Extensions;
    using Nancy.Routing;

    // The following codes are inspired from Nancy.Linker https://github.com/horsdal/Nancy.Linker
    internal static class SegmentExtractorExtensions
    {
        public static string GenerateUrl(
            this IRouteSegmentExtractor segmentExtractor,
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
                throw new ArgumentException(message, nameof(segment));
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

        private static string GetRegexSegmentValue(string segment, IDictionary<string, string> parameters) =>
            Regex.Replace(segment, @"\(\?<(?<name>.*?)>.*?\)", x => parameters[x.Groups["name"].Value]);

        private static string GetConstrainedParamterValue(string segment, IDictionary<string, string> parameters)
        {
            string res;
            parameters.TryGetValue(segment.Substring(1, segment.IndexOf(':') - 1).Trim(), out res);
            return res;
        }

        private static bool IsContrainedParameter(string segment) => segment.Contains(':');

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
    }
}
