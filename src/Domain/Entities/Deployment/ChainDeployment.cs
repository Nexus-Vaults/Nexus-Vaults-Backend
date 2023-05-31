namespace Nexus.Domain.Entities.Deployment;
public class ChainDeployment
{
    public required ushort ContractChainId { get; init; }
    public required ulong? EVMChainId { get; init; }

    public required string RpcUrl { get; init; }
    public required string ChainName { get; set; }

    public required string NexusFactoryAddress { get; set; }
    public required string VaultV1ControllerAddress { get; set; }
    public required string PublicCatalogAddress { get; set; }

    public virtual List<CatalogDeployment>? Catalogs { get; set; } //Navigation Property
    public virtual List<FeatureDeployment>? Features { get; set; } //Navigation Property

    public ChainDeployment()
    {
    }
}
