using Common.Services;
using MediatR;
using Nexus.Application;
using Nexus.Application.Common.MediatR;
using Nexus.Application.Services;
using System.Reflection;

namespace Nexus.Infrastructure.Services;
public class RequestReflectorService : Singleton, IRequestReflectorService
{
    private readonly Dictionary<Type, (Type HandlerType, MethodInfo HandlerMethod)> RequestReflectionInfo;

    public RequestReflectorService()
    {
        RequestReflectionInfo = new Dictionary<Type, (Type HandlerType, MethodInfo HandlerMethod)>();
    }

    protected override ValueTask InitializeAsync()
    {
        var declaringTypes = (Assembly.GetAssembly(typeof(ApplicationAssemblyMarker)) ?? throw new Exception("Application Assembly not found!"))
            .GetTypes()
            .Where(x => x.GetNestedTypes().Any(x => x.GetInterface(nameof(IBaseRequest)) is not null));

        foreach (var declaringType in declaringTypes)
        {
            var handlerType = declaringType.GetNestedTypes()
                .Where(x => x.GetInterfaces().Any(x => x.Name.Contains(nameof(IRequestHandler<IRequest>))))
                .SingleOrDefault() ?? throw new Exception("Requests must be declared in a class that also contains the handler!");

            var handlerMethod = handlerType.GetMethod(nameof(FarsightRequestHandler<IRequest<MediatrResult>, object>.HandleAsync), BindingFlags.Instance | BindingFlags.Public)
                ?? throw new Exception("No HandleAsync method found in handler type!");

            RequestReflectionInfo.Add(declaringType, (handlerType, handlerMethod));
        }

        return ValueTask.CompletedTask;
    }

    public MethodInfo GetHandlerMethod(Type declaringType)
    {
        return RequestReflectionInfo.TryGetValue(declaringType, out var requestInfo)
                ? requestInfo.HandlerMethod
                : throw new Exception($"The declaring type {declaringType.Name} does not contain a handler method!");
    }

    public Type GetHandlerType(Type declaringType)
    {
        return RequestReflectionInfo.TryGetValue(declaringType, out var requestInfo)
                ? requestInfo.HandlerType
                : throw new Exception($"The declaring type {declaringType.Name} does not contain a handler type!");
    }
}
