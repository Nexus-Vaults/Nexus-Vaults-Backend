using AutoMapper;
using System.Reflection;

namespace Nexus.Application.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        // Add mapping here
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            object? instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(nameof(IMapFrom<object>.Mapping))
                ?? type.GetInterface("IMapFrom`1")!.GetMethod(nameof(IMapFrom<object>.Mapping));

            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}