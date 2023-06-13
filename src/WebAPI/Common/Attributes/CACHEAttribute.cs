namespace Nexus.WebAPI.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CACHEAttribute : Attribute
{
    public TimeSpan Expiration { get; }

    public CACHEAttribute(int cacheSeconds)
    {
        Expiration = TimeSpan.FromSeconds(cacheSeconds);
    }
}
