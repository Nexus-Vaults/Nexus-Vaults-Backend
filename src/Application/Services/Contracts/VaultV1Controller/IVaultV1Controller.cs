using Nexus.Application.DTOs;

namespace Nexus.Application.Services.Contracts;
public interface IVaultV1Controller : IContract
{
    public Task<VaultInfoDTO[]> GetVaultsAsync(byte[] nexusId);
    public Task<V1TokenBalance[]> AggregateBalancesAsync(byte[] nexusId, IEnumerable<V1TokenInfoDTO> tokens);
    public Task<uint[]> GetAcceptedGatewayIdsAsync(byte[] nexusId);
}
