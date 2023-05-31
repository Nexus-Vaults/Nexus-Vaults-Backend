using Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Nexus.Application.Services;
using Nexus.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Infrastructure.Services.Contracts.Nexus;
public class NexusProvider : Singleton, INexusProvider
{
    [Inject]
    private readonly Web3ProviderService Web3ProviderService = null!;

    public INexus GetInstance(ushort chainId, string nexusAddress)
    {
        return new Nexus(Web3ProviderService.GetProvider(chainId), nexusAddress);
    }
}
