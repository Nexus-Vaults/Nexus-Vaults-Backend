namespace Nexus.Application.Services.Contracts;
public interface INexus
{
    public Task<string> GetNameAsync();
    public Task<string> GetOwnerAsync();
}
