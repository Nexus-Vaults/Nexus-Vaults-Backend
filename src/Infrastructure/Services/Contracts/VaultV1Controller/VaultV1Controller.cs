using Infrastructure.VaultV1Controller.Service;
using Nethereum.Web3;
using Nexus.Application.Services.Contracts;

namespace Nexus.Infrastructure.Services.Contracts;
public class VaultV1Controller : IVaultV1Controller
{
    private readonly VaultV1ControllerService Service;

    public VaultV1Controller(IWeb3 web3, string contractAddress)
    {
        Service = new VaultV1ControllerService(web3, contractAddress);
    }

    public async Task<VaultInfoDTO[]> GetVaultsAsync(byte[] nexusId)
    {
        var listResult = await Service.ListVaultsQueryAsync(nexusId);
        return listResult.ReturnValue1
            .Select(x => new VaultInfoDTO(x.VaultId, x.Vault))
            .ToArray();
    }
}
