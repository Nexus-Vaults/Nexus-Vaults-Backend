namespace Nexus.Application.Services.Contracts;
public interface INexusFactory
{
    public Task<bool> HasDeployedAsync(string nexusAddress);
}
