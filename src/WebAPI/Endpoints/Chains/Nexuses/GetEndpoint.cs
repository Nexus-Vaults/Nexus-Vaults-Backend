using Nexus.Application.Handlers.Queries.Deployments.List;
using Nexus.Contracts;
using Nexus.WebAPI.Common;

namespace Nexus.WebAPI.Endpoints.Chains.Nexuses;

public record GetNexusRequestContract(
    ulong ChainId,
    string NexusAddress
) : IRequestContract;

public record GetNexusResponseContract(

) : IResponseContract;

[GET("Api/Chains/{chainId}/Nexuses/{nexusAddress}")]
public class GetEndpoint : HttpEndpoint<
    GetNexusRequestContract,
    ListDeploymentsQuery.Request, ListDeploymentsQuery.Result,
    GetNexusResponseContract>
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
