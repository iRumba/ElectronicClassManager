using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ElectronicClassManager.Entities.Storage;

public class EntitiesDbContext : DbContext
{
    public EntitiesDbContext(DbContextOptions<EntitiesDbContext> options) : base(options)
    {

    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PersonConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
