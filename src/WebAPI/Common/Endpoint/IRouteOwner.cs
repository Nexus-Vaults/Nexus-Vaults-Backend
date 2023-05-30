namespace Nexus.WebAPI.Common.Endpoint;

public interface IRouteOwner
{
    public IEndpointConventionBuilder RegisterRoute(IEndpointRouteBuilder routes);
}
