using ElectronicClassManager.Entities.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElectronicClassManager.Db.Configuration;

public class EntitiesDbContextFactory : IDesignTimeDbContextFactory<EntitiesDbContext>
{
    /// <inheritdoc />
    public EntitiesDbContext CreateDbContext(string[] args)
    {
        var conf = new EfConfiguration
        {
            Address = "localhost",
            Port = 6661,
            Db = "ecm",
            User = "sa",
            Password = "Qwerty123"
        };

        var builder = new DbContextOptionsBuilder<EntitiesDbContext>();
        builder.ConfigureOptionsBuilder(conf);

        return new EntitiesDbContext(builder.Options);
    }
}