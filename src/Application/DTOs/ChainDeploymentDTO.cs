using Nexus.Application.Common;
using Nexus.Domain.Entities.Deployment;

namespace Nexus.Application.DTOs;

public record ChainDeploymentDTO(ulong ChainId, string ChainName, string NexusFactoryAddress, string PublicCatalogAddress) : IMapFrom<ChainDeployment>;