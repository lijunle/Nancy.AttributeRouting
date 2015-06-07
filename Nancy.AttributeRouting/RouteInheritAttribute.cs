namespace Nancy.AttributeRouting
{
    using System;

    public class RouteInheritAttribute : Attribute
    {
        private readonly Type type;

        public RouteInheritAttribute(Type type)
        {
            this.type = type;
        }
    }
}
