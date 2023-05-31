﻿using Infrastructure.Nexus.Service;
using Infrastructure.NexusFactory.Service;
using Nethereum.Web3;
using Nexus.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Infrastructure.Services.Contracts.Nexus;
internal class Nexus : INexus
{
    private readonly ushort ContractChainId;
    private readonly NexusService Service;

    public Nexus(ushort contractChainId, IWeb3 web3, string contractAddress)
    {
        ContractChainId = contractChainId;
        Service = new NexusService(web3, contractAddress);
    }

    public ushort GetContractChainId()
    {
        return ContractChainId;
    }

    public Task<string> GetNameAsync()
    {
        return Service.NexusNameQueryAsync();
    }

    public Task<string> GetOwnerAsync()
    {
        return Service.OwnerQueryAsync();
    }

}