using Nexus.Application.Handlers.Queries.Deployments.List;
using Nexus.Contracts;
using Nexus.WebAPI.Common;

namespace Nexus.WebAPI.Endpoints.Deployments.List;

public record ListDeploymentsRequestContract() : IRequestContract;

public record ListDeploymentsResponseContract(
    ChainDeployment[] Deployments
) : IResponseContract;

[GET("Deployments")]
public class ListDeploymentsEndpoint : HttpEndpoint<
    ListDeploymentsRequestContract,
    ListDeploymentsQuery.Request, ListDeploymentsQuery.Result,
    ListDeploymentsResponseContract>
{
    public override IResult MapToResponse(ListDeploymentsQuery.Result result)
    {
        return result.Status switch
        {
            ListDeploymentsQuery.Status.Success => Results.Ok(ResultToContract(result)),
            _ => throw new NotImplementedException()
        };
    }
}
