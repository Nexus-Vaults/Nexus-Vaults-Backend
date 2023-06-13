using Infrastructure.VaultV1.Service;
using Nethereum.Web3;
using Nexus.Application.Services.Contracts;
using System.Numerics;

namespace Nexus.Infrastructure.Services.Contracts;
public class VaultV1 : IVaultV1
{
    private readonly ushort ContractChainId;
    private readonly VaultV1Service Service;

    public VaultV1(ushort contractChainId, IWeb3 web3, string contractAddress)
    {
        ContractChainId = contractChainId;
        Service = new VaultV1Service(web3, contractAddress);
    }

    public ushort GetContractChainId()
    {
        return ContractChainId;
    }

    public Task<BigInteger> GetNativeBalanceAsync()
    {
        return Service.GetBalanceQueryAsync((byte)V1TokenTypes.Native, "");
    }

    public Task<BigInteger> GetBalanceAsync(V1TokenTypes tokenType, string tokenIdentifier)
    {
        return Service.GetBalanceQueryAsync(((byte)tokenType), tokenIdentifier);
    }
}
