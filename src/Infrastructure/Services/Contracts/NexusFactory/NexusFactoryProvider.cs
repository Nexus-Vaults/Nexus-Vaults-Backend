using Common.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Application.Services;
using Nexus.Application.Services.Contracts;

namespace Nexus.Infrastructure.Services.Contracts;
public class NexusFactoryProvider : Singleton, INexusFactoryProvider
{
    [Inject]
    private readonly Web3ProviderService Web3ProviderService = null!;

    private IReadOnlyDictionary<ulong, string> ContractAddresses;

    protected override async ValueTask InitializeAsync()
    {
        using var scope = Provider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

        var contractAddresses = await dbContext.ChainDeployments
            .Select(x => new { x.ChainId, x.NexusFactoryAddress })
            .ToArrayAsync();

        ContractAddresses = contractAddresses
            .ToDictionary(x => x.ChainId, x => x.NexusFactoryAddress)
            .AsReadOnly();
    }


    public INexusFactory GetNexusFactory(ulong chainId)
    {
        return new NexusFactory(Web3ProviderService.GetProvider(chainId), ContractAddresses[chainId]);
    }
}
