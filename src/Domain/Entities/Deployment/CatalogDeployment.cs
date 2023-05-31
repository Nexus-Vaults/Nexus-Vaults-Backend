namespace Nexus.Domain.Entities.Deployment;
public class CatalogDeployment
{
    public required string Address { get; init; }

    public required ulong ContractChainId { get; init; }

    public virtual ChainDeployment? Chain { get; init; } //Navigation Property
    public virtual List<FeatureDeployment>? Features { get; set; } //Navigation Property

    public CatalogDeployment()
    {
    }
}
