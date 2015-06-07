namespace Nancy.AttributeRouting
{
    using System;
    using System.Reflection;

    /// <summary>
    /// <see cref="RouteInheritAttribute"/> indicates a type inherit another type's routing
    /// information, including routing prefix, view prefix and before hooks.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class RouteInheritAttribute : Attribute
    {
        private readonly Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteInheritAttribute"/> class.
        /// </summary>
        /// <param name="type">The type to inherit its routing information.</param>
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
