using Nexus.Application.Handlers.Queries;
using Nexus.Application.Services.Contracts;
using Nexus.Contracts;
using Nexus.WebAPI.Common;

namespace Nexus.WebAPI.Endpoints.Chains.Nexuses.Subchains.Assets;

public record GetNexusVaultBalancesContract(
    ushort ContractChainId,
    string NexusAddress,
    ushort SubchainContractChainId,
    V1TokenTypes TokenType,
    string TokenIdentifier
) : IRequestContract;

public record GetNexusVaultBalancesResponseContract(
    VaultAssetBalanceDTO[] VaultBalances
) : IResponseContract;

[GET("Api/Chains/{contractChainId}/Nexuses/{nexusAddress}/Subchains/{subchainContractChainId}/Assets/{tokenType}/{tokenIdentifier}/VaultBalances")]
public class GetVaultBalancesEndpoint : HttpEndpoint<
    GetNexusVaultBalancesContract,
    GetVaultAssetBalancesQuery.Request, GetVaultAssetBalancesQuery.Result,
    GetNexusVaultBalancesResponseContract>
{
    public override IResult MapToResponse(GetVaultAssetBalancesQuery.Result result)
    {
        return result.Status switch
        {
            GetVaultAssetBalancesQuery.Status.Success => Results.Ok(ResultToContract(result)),
            GetVaultAssetBalancesQuery.Status.UnsupportedChain => Results.NotFound(),
            GetVaultAssetBalancesQuery.Status.NexusNotFound => Results.NotFound(),
            _ => throw new NotImplementedException()
        };
    }
}
