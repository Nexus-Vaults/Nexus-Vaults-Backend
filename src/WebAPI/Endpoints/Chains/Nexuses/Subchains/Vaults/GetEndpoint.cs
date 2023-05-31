using Nexus.Application.Handlers.Queries.Vaults.Overview;
using Nexus.Contracts;
using Nexus.WebAPI.Common;

namespace Nexus.WebAPI.Endpoints.Chains.Nexuses.Subchains.Vaults;

public record GetVaultRequestContract(
    ushort NexusContractChainId,
    string NexusAddress,
    ushort VaultContractChainId,
    uint VaultId
) : IRequestContract;

public record GetVaultResponseContract() : IResponseContract;

[GET("Api/Chains/{nexusContractChainId}/Nexuses/{nexusAddress}/Subchains/{vaultContractChainId}/Vaults/{vaultId}")]
public class GetEndpoint : HttpEndpoint<
    GetVaultRequestContract,
    GetVaultOverviewQuery.Request, GetVaultOverviewQuery.Result,
    GetVaultResponseContract>
{
    public override IResult MapToResponse(GetVaultOverviewQuery.Result result)
    {
        return result.Status switch
        {
            GetVaultOverviewQuery.Status.Success => Results.Ok(ResultToContract(result)),
            _ => throw new NotImplementedException()
        };
    }
}
