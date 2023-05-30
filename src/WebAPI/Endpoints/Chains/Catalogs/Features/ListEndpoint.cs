using Nexus.Application.DTOs;
using Nexus.Application.Handlers.Queries.Features.ListByChain;
using Nexus.Contracts;
using Nexus.WebAPI.Common;

namespace Nexus.WebAPI.Endpoints.Chains.Catalogs.Features;

public record ListChainFeaturesRequestContract(
    ulong ChainId,
    string CatalogAddress
) : IRequestContract;

public record ListChainFeaturesResponseContract(
    FeatureDeploymentDTO[] Features
) : IResponseContract;

[GET("Api/Chains/{chainId}/Catalogs/{catalogAddress}/Features")]
public class ListEndpoint : HttpEndpoint<
    ListChainFeaturesRequestContract,
    ListFeaturesByCatalogQuery.Request, ListFeaturesByCatalogQuery.Result,
    ListChainFeaturesResponseContract>
{
    public override IResult MapToResponse(ListFeaturesByCatalogQuery.Result result)
    {
        return result.Status switch
        {
            ListFeaturesByCatalogQuery.Status.Success => Results.Ok(ResultToContract(result)),
            ListFeaturesByCatalogQuery.Status.ChainDeploymentNotFound => Results.NotFound,
            ListFeaturesByCatalogQuery.Status.CatalogNotFound => Results.NotFound,
            _ => throw new NotImplementedException()
        };
    }
}
