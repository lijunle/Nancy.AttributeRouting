namespace Nancy.AttributeRouting
{
    using System;

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
}
