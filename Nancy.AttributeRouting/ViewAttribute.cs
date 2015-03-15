namespace Nancy.AttributeRouting
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class ViewAttribute : Attribute
    {
        private readonly string path;

        public ViewAttribute(string path)
        {
            this.path = path;
        }

        public string Path
        {
            get { return this.path; }
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class ViewPrefixAttribute : Attribute
    {
        private readonly string prefix;

        public ViewPrefixAttribute(string prefix)
        {
            this.prefix = prefix;
        }
    }
}
