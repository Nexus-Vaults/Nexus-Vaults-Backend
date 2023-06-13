using Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Application.Services;
using Nexus.Application.Services.Contracts;

namespace Nexus.Infrastructure.Services.Contracts;
public class VaultV1Provider : Singleton, IVaultV1Provider
{
    [Inject]
    private readonly Web3ProviderService Web3ProviderService = null!;

    public IVaultV1 GetInstance(ushort contractChainId, string contractAddress)
    {
        return new VaultV1(contractChainId, Web3ProviderService.GetProvider(contractChainId), contractAddress);
    }
}
