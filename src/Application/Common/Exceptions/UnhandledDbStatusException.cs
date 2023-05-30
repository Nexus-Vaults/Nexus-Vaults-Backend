namespace Nexus.Application.Common;
public class UnhandledDbStatusException : Exception
{
    internal UnhandledDbStatusException(DbSaveResult saveResult)
        : base($"Unhandled DB Status while saving: Status={saveResult.Status}")
    {
    }
}