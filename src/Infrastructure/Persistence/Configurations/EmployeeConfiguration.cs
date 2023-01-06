using Ems.Domain.Entities;
using Ems.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ems.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration: IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {

        builder.ToTable("Employees");
        builder
            .HasMany(e => e.Reports)
            .WithOne(e => e.Manager);

        builder.OwnsOne(x => x.Address);
    }
}
