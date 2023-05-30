namespace Nexus.Application.Common;

/// <summary>
/// Specifies that the caller needs to be unauthenticated!
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class UnauthorizedAttribute : Attribute
{
}
