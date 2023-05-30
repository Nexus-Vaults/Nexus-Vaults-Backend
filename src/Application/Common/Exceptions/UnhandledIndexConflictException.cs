namespace Nexus.Application.Common;
internal class UnhandledIndexConflictException : Exception
{
    public UnhandledIndexConflictException(DbSaveResult saveResult)
        : base($"Unhandled DB Index Conflict while saving: ConstraintName={saveResult.ConstraintName}")
    {
    }
}