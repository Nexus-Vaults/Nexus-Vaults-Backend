using Common.Services;

namespace Nexus.Application.Services.Contracts;
public interface IVaultV1Provider : IService
{
    public IVaultV1 GetInstance(ushort chainId);
}
