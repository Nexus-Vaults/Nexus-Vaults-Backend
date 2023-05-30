using Nexus.WebAPI.Common.Endpoint;
using System.Reflection;

namespace Nexus.WebAPI.Common.Exceptions;

public static partial class Extensions
{
    public static Type[] GetHttpEndpointTypes(this Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(x => x.GetInterface(nameof(IRouteOwner)) != null)
            .Where(x => !x.IsInterface && !x.IsAbstract && !x.IsGenericType)
            .ToArray();
    }
}
