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

    private IReadOnlyDictionary<ushort, string> ContractAddresses;

    protected override async ValueTask InitializeAsync()
    {
        using var scope = Provider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();

        var contractAddresses = await dbContext.ChainDeployments
            .Select(x => new { x.ContractChainId, x.NexusFactoryAddress })
            .ToArrayAsync();

        ContractAddresses = contractAddresses
            .ToDictionary(x => x.ContractChainId, x => x.NexusFactoryAddress)
            .AsReadOnly();
    }


    public INexusFactory GetInstance(ushort contractChainId)
    {
        return new NexusFactory(contractChainId, Web3ProviderService.GetProvider(contractChainId), ContractAddresses[contractChainId]);
    }
}
