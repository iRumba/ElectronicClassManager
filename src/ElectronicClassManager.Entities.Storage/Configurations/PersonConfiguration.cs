using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicClassManager.Entities.Storage.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons")
            .HasKey(x => x.Id);

        builder.Property(x => x.Id).HasValueGenerator<IdGenerator>();
        
    }
}