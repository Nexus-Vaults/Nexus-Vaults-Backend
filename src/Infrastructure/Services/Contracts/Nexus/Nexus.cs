using Infrastructure.DiamondLoupeFacet.Service;
using Infrastructure.Nexus.Service;
using Nethereum.Web3;
using Nexus.Application.Services.Contracts;

namespace Nexus.Infrastructure.Services.Contracts.Nexus;
internal class Nexus : INexus
{
    private readonly ushort ContractChainId;
    private readonly NexusService NexusService;
    private readonly DiamondLoupeFacetService DiamondLoupeFacetService;

    public Nexus(ushort contractChainId, IWeb3 web3, string contractAddress)
    {
        ContractChainId = contractChainId;
        NexusService = new NexusService(web3, contractAddress);
        DiamondLoupeFacetService = new DiamondLoupeFacetService(web3, contractAddress);
    }

    public ushort GetContractChainId()
    {
        return ContractChainId;
    }

    public Task<string> GetNameAsync()
    {
        return NexusService.NexusNameQueryAsync();
    }

    public Task<string> GetOwnerAsync()
    {
        return NexusService.OwnerQueryAsync();
    }

    public async Task<IReadOnlyList<string>> GetInstalledFacetAddressesAsync()
    {
        return await DiamondLoupeFacetService.FacetAddressesQueryAsync();
    }
}
