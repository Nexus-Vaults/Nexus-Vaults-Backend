namespace Nexus.WebAPI.Common.Exceptions;

public class DuplicateCacheAttributeException : Exception
{
    public DuplicateCacheAttributeException(Type endpointType)
    : base($"Found more than one cache attribute (CACHE) on the endpoint {endpointType.Name}!")
    {
    }
}
