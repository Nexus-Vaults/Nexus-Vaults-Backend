using Nexus.Application.Common;
using Nexus.Domain.Entities.Deployment;

namespace Nexus.Application.DTOs;

public class ChainDeploymentDTO : IMapFrom<ChainDeployment>
{
    public required ulong ChainId { get; init; }
    public required string ChainName { get; init; }
    public required string NexusFactoryAddress { get; init; }
    public required string PublicCatalogAddress { get; init; }

    public ChainDeploymentDTO()
    {
    }
}