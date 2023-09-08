using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ElectronicClassManager.Entities.Storage;

public class IdGenerator : ValueGenerator<Guid>
{
    /// <inheritdoc />
    public override Guid Next(EntityEntry entry)
    {
        return Guid.NewGuid();
    }

    /// <inheritdoc />
    public override bool GeneratesTemporaryValues => false;
}