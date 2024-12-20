using Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new EmployeeId(x));
        
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Salary).IsRequired();
        builder.Property(x => x.Position).IsRequired();
        
    }
}