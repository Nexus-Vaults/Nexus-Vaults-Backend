using Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nethereum.Web3;
using Nexus.Application.Services;

namespace Nexus.Infrastructure.Services;
public class Web3ProviderService : Singleton, IWeb3ProviderService
{
    private IReadOnlyDictionary<ulong, IWeb3> Providers;

    protected override async ValueTask InitializeAsync()
    {
        using var scope = Provider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

        var rpcProviders = await dbContext.ChainDeployments
            .Select(x => new { x.ChainId, x.RpcUrl })
            .ToArrayAsync();

        Providers = rpcProviders
            .ToDictionary(x => x.ChainId, x => (IWeb3)new Web3(x.RpcUrl))
            .AsReadOnly();
    }

    public bool IsSupported(ulong chainId)
    {
        return Providers.ContainsKey(chainId);
    }

    public IWeb3 GetProvider(ulong chainId)
    {
        return Providers[chainId];
    }
}
