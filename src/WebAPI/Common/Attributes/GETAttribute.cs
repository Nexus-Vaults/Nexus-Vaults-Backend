namespace Nexus.WebAPI.Common;

[AttributeUsage(AttributeTargets.Class)]
public class GETAttribute : RouteAttribute
{
    public GETAttribute(string route)
        : base(route)
    {
    }

    public override IEndpointConventionBuilder Register(IEndpointRouteBuilder routes, Delegate handler)
    {
        return routes.MapGet(Route, handler);
    }
}
