using Nexus.Application.Services.Contracts;

namespace Nexus.Application.Handlers.Queries;
public record NexusSubchainDTO(ushort ContractChainId, VaultInfoDTO[] Vaults, uint[] AcceptedGatewayIds);
