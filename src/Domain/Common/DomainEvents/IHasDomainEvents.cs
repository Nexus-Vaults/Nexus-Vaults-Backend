﻿namespace Nexus.Domain.Common;

public interface IHasDomainEvents
{
    public List<DomainEvent> DomainEvents { get; }
}