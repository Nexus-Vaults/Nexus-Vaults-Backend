using Nexus.Application.Handlers.Queries;
using Nexus.Contracts;
using Nexus.WebAPI.Common;

namespace Nexus.WebAPI.Endpoints.Chains.Nexuses;

public record GetNexusExistenceRequestContract(
    ushort ContractChainId,
    string NexusAddress
) : IRequestContract;

public record GetNexusExistenceResponseContract(
    bool Exists
) : IResponseContract;

[GET("Api/Chains/{contractChainId}/Nexuses/{nexusAddress}/Exists")]
public class ExistsEndpoint : HttpEndpoint<
    GetNexusExistenceRequestContract,
    GetNexusExistenceQuery.Request, GetNexusExistenceQuery.Result,
    GetNexusExistenceResponseContract>
{
    public override IResult MapToResponse(GetNexusExistenceQuery.Result result)
    {
        return result.Status switch
        {
            GetNexusExistenceQuery.Status.Success => Results.Ok(new GetNexusExistenceResponseContract(true)),
            GetNexusExistenceQuery.Status.UnsupportedChain => Results.Ok(new GetNexusExistenceResponseContract(false)),
            GetNexusExistenceQuery.Status.NexusNotFound => Results.Ok(new GetNexusExistenceResponseContract(false)),
            _ => throw new NotImplementedException()
        };
    }
}
