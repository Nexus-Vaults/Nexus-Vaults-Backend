using Common.Services;

namespace Nexus.Application.Services.Contracts;
public interface INexusFactoryProvider : IService
{
    public INexusFactory GetNexusFactory(ulong chainId);
}
