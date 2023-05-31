using Nexus.Application.Handlers.Queries.Deployments.List;
using Nexus.Application.Services.Contracts;
using Nexus.Contracts;
using Nexus.WebAPI.Common;

namespace Nexus.WebAPI.Endpoints.Chains.Nexuses;

public record GetNexusRequestContract(
    ulong ContractChainId,
    string NexusAddress
) : IRequestContract;

public record GetNexusResponseContract(
    string NexusId,
    VaultInfoDTO Vaults
) : IResponseContract;

[GET("Api/Chains/{contractChainId}/Nexuses/{nexusAddress}")]
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
