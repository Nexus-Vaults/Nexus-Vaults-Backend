namespace Nexus.Domain.Common;

public abstract class DomainEvent
{
    public bool IsPublished { get; set; }
    public DateTimeOffset DateOccurred { get; protected set; }

    protected DomainEvent()
    {
        DateOccurred = DateTimeOffset.UtcNow;
    }
}