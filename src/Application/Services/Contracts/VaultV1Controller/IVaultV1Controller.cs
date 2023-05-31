namespace Nexus.Application.Services.Contracts;
public interface IVaultV1Controller : IContract
{
    public Task<VaultInfoDTO[]> GetVaultsAsync(byte[] nexusId);
    public Task<uint[]> GetAcceptedGatewayIdsAsync(byte[] nexusId);
}
