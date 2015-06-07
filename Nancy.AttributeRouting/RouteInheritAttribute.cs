namespace Nancy.AttributeRouting
{
    using System;
    using System.Reflection;

    public class RouteInheritAttribute : Attribute
    {
        private readonly Type type;

        public RouteInheritAttribute(Type type)
        {
            this.type = type;
        }

        internal static Type GetAncestorType(Type type)
        {
            var attr = type.GetCustomAttribute<RouteInheritAttribute>(inherit: false);
            return attr == null ? null : attr.type;
        }
    }
}
