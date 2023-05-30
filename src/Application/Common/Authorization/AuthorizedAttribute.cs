namespace Nexus.Application.Common;

/// <summary>
/// Specifies that the caller needs to be authenticated!
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class AuthorizedAttribute : Attribute
{
}
