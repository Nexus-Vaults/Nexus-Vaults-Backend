using Nexus.Application.Common;
using Nexus.Domain.Entities.Deployment;

namespace Nexus.Application.DTOs;

public class FeatureDeploymentDTO : IMapFrom<FeatureDeployment>
{
    public required string Address { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}
