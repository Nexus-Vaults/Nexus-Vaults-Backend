namespace Nexus.WebAPI.Common;

[AttributeUsage(AttributeTargets.Class)]
public class DELETEAttribute : RouteAttribute
{
    public DELETEAttribute(string route)
        : base(route)
    {
    }

    public override IEndpointConventionBuilder Register(IEndpointRouteBuilder routes, Delegate handler)
    {
        return routes.MapDelete(Route, handler);
    }
}
