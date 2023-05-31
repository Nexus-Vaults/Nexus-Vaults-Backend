using Infrastructure.NexusFactory.Service;
using Nethereum.Web3;
using Nexus.Application.Services.Contracts;

namespace Nexus.Infrastructure.Services.Contracts;
public class NexusFactory : INexusFactory
{
	private readonly NexusFactoryService Service;

	public NexusFactory(IWeb3 web3, string contractAddress)
	{
		Service = new NexusFactoryService(web3, contractAddress);
	}

	public Task<bool> HasDeployedAsync(string nexusAddress)
	{
		return Service.HasDeployedQueryAsync(nexusAddress);
	}
}
