using Nexus.Application.Common;

namespace Nexus.Application.Handlers.Queries.Deployments.List;
public class ListDeploymentsQuery
{
    public class Request : Query<Result>
    {
    }

    public enum Status : byte
    {
        Success,
    }

    public record Result(Status Status, ChainDeployment[] Deployments);


    public class Handler : QueryHandler<Request, Result>
    {
        public override Task<Result> HandleAsync(Request request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
