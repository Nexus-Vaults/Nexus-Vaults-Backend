using Infrastructure.NexusFactory.Service;
using Nethereum.Web3;
using Nexus.Application.Services.Contracts;

namespace Nexus.Infrastructure.Services.Contracts;
public class NexusFactory : INexusFactory
{
    private readonly ushort ContractChainId;
    private readonly NexusFactoryService Service;

    public NexusFactory(ushort contractChainId, IWeb3 web3, string contractAddress)
    {
        ContractChainId = contractChainId;
        Service = new NexusFactoryService(web3, contractAddress);
    }

    public ushort GetContractChainId()
    {
        return ContractChainId;
    }

    public Task<bool> HasDeployedAsync(string nexusAddress)
    {
        return Service.HasDeployedQueryAsync(nexusAddress);
    }
}
