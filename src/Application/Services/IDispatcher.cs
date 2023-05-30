using Common.Services;
using MediatR;

namespace Nexus.Application.Services;
public interface IDispatcher : IService
{
    public void Enqueue(IRequest request, string jobName);
    public void Schedule(IRequest request, string jobName, TimeSpan delay);
}
