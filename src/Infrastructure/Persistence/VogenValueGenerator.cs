using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Nexus.Infrastructure.Persistence;
public class VogenValueGenerator<T> : ValueGenerator<T> where T : struct
{
    public override bool GeneratesTemporaryValues
        => true;

    public override T Next(EntityEntry entry)
    {
        return new T();
    }
}