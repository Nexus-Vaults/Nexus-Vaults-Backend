namespace Nexus.Domain.Entities.Deployment;
public class FeatureDeployment
{
    public required string Address { get; init; }

    public required string Name { get; init; }
    public required string Description { get; init; }

    public required ulong ChainId { get; init; }
    public required string CatalogAddress { get; init; }

    public virtual ChainDeployment? Chain { get; set; } //Navigation Property
    public virtual CatalogDeployment? Catalog { get; set; } //Navigation Property

    public FeatureDeployment()
    {
    }
}
