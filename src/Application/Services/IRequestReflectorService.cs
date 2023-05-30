using Common.Services;
using System.Reflection;

namespace Nexus.Application.Services;
public interface IRequestReflectorService : IService
{
    public Type GetHandlerType(Type declaringType);
    public MethodInfo GetHandlerMethod(Type declaringType);
}
