namespace Nexus.Application.Services.Contracts;
public interface IVaultV1Controller
{
    public Task<VaultInfoDTO[]> GetVaultsAsync(byte[] nexusId);
}
