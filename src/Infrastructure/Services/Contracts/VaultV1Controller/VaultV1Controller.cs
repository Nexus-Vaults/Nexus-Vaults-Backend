﻿using Infrastructure.VaultV1Controller.ContractDefinition;
using Infrastructure.VaultV1Controller.Service;
using Nethereum.Web3;
using Nexus.Application.DTOs;
using Nexus.Application.Services.Contracts;

namespace Nexus.Infrastructure.Services.Contracts;
public class VaultV1Controller : IVaultV1Controller
{
    private readonly ushort ContractChainId;
    private readonly VaultV1ControllerService Service;

    public VaultV1Controller(ushort contractChainId, IWeb3 web3, string contractAddress)
    {
        ContractChainId = contractChainId;
        Service = new VaultV1ControllerService(web3, contractAddress);
    }

    public ushort GetContractChainId()
    {
        return ContractChainId;
    }

    public async Task<VaultInfoDTO[]> GetVaultsAsync(byte[] nexusId)
    {
        var listResult = await Service.ListVaultsQueryAsync(nexusId);

        return listResult is null
            ? Array.Empty<VaultInfoDTO>()
            : listResult.ReturnValue1
                .Select(x => new VaultInfoDTO(x.VaultId, x.Vault))
                .ToArray();
    }

    public async Task<uint[]> GetAcceptedGatewayIdsAsync(byte[] nexusId)
    {
        var listResult = await Service.ListAcceptedGatewaysQueryAsync(nexusId);

        return listResult is null
            ? Array.Empty<uint>()
            : listResult.ToArray();
    }

    public async Task<V1TokenBalanceDTO[]> AggregateBalancesAsync(byte[] nexusId, IEnumerable<Application.DTOs.V1TokenInfoDTO> tokens)
    {
        var t = tokens.Select(x => new V1TokenInfo()
        {
            TokenType = (byte)x.TokenType,
            TokenIdentifier = x.TokenIdentifier,
        }).ToList();

        var balances = await Service.AggregateBalancesQueryAsync(nexusId, t);

        return tokens
            .Select((x, i) => new V1TokenBalanceDTO()
            {
                Token = x,
                Balance = balances[i]
            })
            .ToArray();
    }
}
