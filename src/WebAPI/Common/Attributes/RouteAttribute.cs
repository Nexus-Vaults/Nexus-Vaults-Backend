namespace Nexus.WebAPI.Common;

public abstract class RouteAttribute : Attribute
{
    public string Route { get; }

    public RouteAttribute(string route)
    {
        Route = route;
    }

    public abstract IEndpointConventionBuilder Register(IEndpointRouteBuilder routes, Delegate handler);
}
