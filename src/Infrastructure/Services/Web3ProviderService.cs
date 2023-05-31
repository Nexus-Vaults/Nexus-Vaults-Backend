using Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.Web3;
using Nexus.Application.Services;

namespace Nexus.Infrastructure.Services;
public class Web3ProviderService : Singleton, IWeb3ProviderService
{
    private IReadOnlyDictionary<ushort, IWeb3> Providers;

    protected override async ValueTask InitializeAsync()
    {
        using var scope = Provider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

        var rpcProviders = await dbContext.ChainDeployments
            .Select(x => new { x.ContractChainId, x.RpcUrl })
            .ToArrayAsync();

        Providers = rpcProviders
            .ToDictionary(x => x.ContractChainId, x => (IWeb3)new Web3(x.RpcUrl))
            .AsReadOnly();
    }

    public bool IsSupported(ushort contractChainId)
    {
        return Providers.ContainsKey(contractChainId);
    }

    public IWeb3 GetProvider(ushort contractChainId)
    {
        return Providers[contractChainId];
    }
}
