namespace Nexus.Application.Services.Contracts;
public interface INexusFactory : IContract
{
    public Task<bool> HasDeployedAsync(string nexusAddress);
}
