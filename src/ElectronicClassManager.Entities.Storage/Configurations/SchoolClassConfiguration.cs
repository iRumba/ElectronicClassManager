using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicClassManager.Entities.Storage.Configurations;

public class SchoolClassConfiguration : IEntityTypeConfiguration<SchoolClass>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SchoolClass> builder)
    {
        builder.HasIndex(x => x.PseudoName).IsUnique();
    }
}