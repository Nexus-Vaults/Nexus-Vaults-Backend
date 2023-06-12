using Nexus.Application.DTOs;
using Nexus.Application.Services.Contracts;

namespace Nexus.Application.Handlers.Queries;
public record NexusSubchainDTO(ushort ContractChainId, VaultInfoDTO[] Vaults, V1TokenBalance[] Balances, uint[] AcceptedGatewayIds);
