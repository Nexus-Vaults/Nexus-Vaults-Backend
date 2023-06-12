using System.Numerics;

namespace Nexus.Application.Services.Contracts;
public interface IVaultV1 : IContract
{
    public Task<BigInteger> GetNativeBalanceAsync();
}
