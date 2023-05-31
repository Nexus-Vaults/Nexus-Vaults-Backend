using Common.Services;

namespace Nexus.Application.Services.Contracts;
public interface IVaultV1ControllerProvider : IService
{
    public IVaultV1Controller[] GetAllInstances();
    public IVaultV1Controller GetInstance(ushort chainId);
}
