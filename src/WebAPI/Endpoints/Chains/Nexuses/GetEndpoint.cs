using AutoMapper;
using Nexus.Application.Handlers.Queries;
using Nexus.Contracts;
using Nexus.WebAPI.Common;

namespace Nexus.WebAPI.Endpoints.Chains.Nexuses;

public record GetNexusRequestContract(
    ushort ContractChainId,
    string NexusAddress
) : IRequestContract;

public record GetNexusResponseContract(
    string NexusId,
    string Name,
    string Owner,
    NexusSubchainDTO[] Subchains,
    bool HasLoupeFacet,
    string[] FacetAddresses
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
