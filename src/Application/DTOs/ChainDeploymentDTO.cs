using Nexus.Application.Common;
using Nexus.Domain.Entities.Deployment;

namespace Nexus.Application.DTOs;

public class ChainDeploymentDTO : IMapFrom<ChainDeployment>
{
    public required ushort ContractChainId { get; init; }
    public required ulong EVMChainId { get; set; }
    public required string ChainName { get; init; }
    public required string NexusFactoryAddress { get; init; }
    public required string PublicCatalogAddress { get; init; }

    public ChainDeploymentDTO()
    {
    }
}