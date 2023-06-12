using Common.Services;
using Nexus.Application.Services.Contracts;

namespace Nexus.Infrastructure.Services.Contracts.Nexus;
public class NexusProvider : Singleton, INexusProvider
{
    [Inject]
    private readonly Web3ProviderService Web3ProviderService = null!;

    public INexus GetInstance(ushort contractChainId, string nexusAddress)
    {
        return new Nexus(contractChainId, Web3ProviderService.GetProvider(contractChainId), nexusAddress);
    }
}
