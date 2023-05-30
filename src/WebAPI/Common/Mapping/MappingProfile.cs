using AutoMapper;
using MediatR;
using Nexus.Application.Common.MediatR;
using Nexus.Contracts;
using Nexus.WebAPI.Common.Exceptions;
using System.Reflection;

namespace Nexus.WebAPI.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetHttpEndpointTypes();

        foreach (var type in types)
        {
            object? instance = Activator.CreateInstance(type);

            var contractToRequestMethod = type.GetMethod(nameof(HttpEndpoint<IRequestContract, IRequest<MediatrResult>, object, IResponseContract>.ConfigureContractToRequestMapping));
            var resultToResponseMethod = type.GetMethod(nameof(HttpEndpoint<IRequestContract, IRequest<MediatrResult>, object, IResponseContract>.ConfigureResultToResponseMapping));

            contractToRequestMethod?.Invoke(instance, new object[] { this });
            resultToResponseMethod?.Invoke(instance, new object[] { this });
        }
    }
}
