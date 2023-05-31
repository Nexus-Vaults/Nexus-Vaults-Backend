using Common.Services;

namespace Nexus.Application.Services;
public interface IWeb3ProviderService : IService
{
    public bool IsSupported(ushort contractChainId);
}
