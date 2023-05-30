namespace Nexus.WebAPI.Common;

[AttributeUsage(AttributeTargets.Class)]
public class POSTAttribute : RouteAttribute
{
    public POSTAttribute(string route)
        : base(route)
    {
    }

    public override IEndpointConventionBuilder Register(IEndpointRouteBuilder routes, Delegate handler)
    {
        return routes.MapPost(Route, handler);
    }
}
