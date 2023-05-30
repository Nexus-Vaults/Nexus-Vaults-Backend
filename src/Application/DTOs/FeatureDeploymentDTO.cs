using Nexus.Application.Common;
using Nexus.Domain.Entities.Deployment;

namespace Nexus.Application.DTOs;

public record FeatureDeploymentDTO(string Address, string Name, string Description) : IMapFrom<FeatureDeployment>;
