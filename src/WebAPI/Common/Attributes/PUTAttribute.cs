namespace Nexus.WebAPI.Common;

[AttributeUsage(AttributeTargets.Class)]
public class PUTAttribute : RouteAttribute
{
    public PUTAttribute(string route)
        : base(route)
    {
    }
    public override IEndpointConventionBuilder Register(IEndpointRouteBuilder routes, Delegate handler)
    {
        return routes.MapPut(Route, handler);
    }
}
