using Nexus.Application.DTOs;
using Nexus.Application.Handlers.Queries.Deployments.List;
using Nexus.Contracts;
using Nexus.WebAPI.Common;
using Nexus.WebAPI.Common.Attributes;

namespace Nexus.WebAPI.Endpoints.Deployments;

public record ListDeploymentsRequestContract() : IRequestContract;

public record ListDeploymentsResponseContract(
    ChainDeploymentDTO[] Deployments
) : IResponseContract;

[GET("Api/Deployments")]
[CACHE(300)]
public class ListEndpoint : HttpEndpoint<
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
