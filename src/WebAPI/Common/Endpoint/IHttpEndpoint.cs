using MediatR;
using Nexus.WebAPI.Common.Endpoint;
namespace Nexus.WebAPI.Common;
public interface IHttpEndpoint<TContract> : IRouteOwner
{
    Task<IResult> HandleAsync(TContract requestContract, IMediator mediator, CancellationToken cancellationToken);
}
