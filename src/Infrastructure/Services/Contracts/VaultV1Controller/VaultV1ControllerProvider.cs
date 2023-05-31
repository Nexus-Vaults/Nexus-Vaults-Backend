using Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Application.Services;
using Nexus.Application.Services.Contracts;

namespace Nexus.Infrastructure.Services.Contracts;
public class VaultV1ControllerProvider : Singleton, IVaultV1ControllerProvider
{
    [Inject]
    private readonly Web3ProviderService Web3ProviderService = null!;

    private IReadOnlyDictionary<ushort, string> ContractAddresses;

    protected override async ValueTask InitializeAsync()
    {
        using var scope = Provider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

        var contractAddresses = await dbContext.ChainDeployments
            .Select(x => new { x.ContractChainId, x.VaultV1ControllerAddress })
            .ToArrayAsync();

        ContractAddresses = contractAddresses
            .ToDictionary(x => x.ContractChainId, x => x.VaultV1ControllerAddress)
            .AsReadOnly();
    }

    public IVaultV1Controller GetInstance(ushort contractChainId)
    {
        return new VaultV1Controller(contractChainId, Web3ProviderService.GetProvider(contractChainId), ContractAddresses[contractChainId]);
    }

    public IVaultV1Controller[] GetAllInstances()
    {
        return ContractAddresses
            .Select(x => new VaultV1Controller(x.Key, Web3ProviderService.GetProvider(x.Key), x.Value))
            .ToArray();
    }
}
