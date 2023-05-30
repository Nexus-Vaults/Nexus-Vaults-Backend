using Nexus.Application.Common.MediatR;

namespace Nexus.Application.Common;
public abstract class QueryHandler<TRequest, TResponse> : FarsightRequestHandler<TRequest, TResponse>
    where TRequest : Query<TResponse>
{
}
