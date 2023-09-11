using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicClassManager.Entities.Storage.Configurations;

public class StudentsConfiguration : IEntityTypeConfiguration<Student>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

    }
}