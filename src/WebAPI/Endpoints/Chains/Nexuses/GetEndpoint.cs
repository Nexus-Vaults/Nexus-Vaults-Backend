using Nexus.Application.Handlers.Queries.Nexus.Overview;
using Nexus.Application.Services.Contracts;
using Nexus.Contracts;
using Nexus.WebAPI.Common;

namespace Nexus.WebAPI.Endpoints.Chains.Nexuses;

public record GetNexusRequestContract(
    ushort ContractChainId,
    string NexusAddress
) : IRequestContract;

public record GetNexusResponseContract(
    string NexusId,
    VaultInfoDTO[] Vaults
) : IResponseContract;

[GET("Api/Chains/{contractChainId}/Nexuses/{nexusAddress}")]
public class GetEndpoint : HttpEndpoint<
    GetNexusRequestContract,
    GetNexusOverviewQuery.Request, GetNexusOverviewQuery.Result,
    GetNexusResponseContract>
{
    public override IResult MapToResponse(GetNexusOverviewQuery.Result result)
    {
        return result.Status switch
        {
            GetNexusOverviewQuery.Status.Success => Results.Ok(ResultToContract(result)),
            GetNexusOverviewQuery.Status.UnsupportedChain => Results.NotFound(),
            GetNexusOverviewQuery.Status.NexusNotFound => Results.NotFound(),
            _ => throw new NotImplementedException()
        };
    }
}
