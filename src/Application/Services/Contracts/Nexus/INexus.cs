namespace Nexus.Application.Services.Contracts;
public interface INexus : IContract
{
    public Task<string> GetNameAsync();
    public Task<string> GetOwnerAsync();
}
