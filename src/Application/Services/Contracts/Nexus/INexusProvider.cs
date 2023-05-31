using Common.Services;

namespace Nexus.Application.Services.Contracts;
public interface INexusProvider : IService
{
    public INexus GetInstance(ushort chainId, string nexusAddress);
}
