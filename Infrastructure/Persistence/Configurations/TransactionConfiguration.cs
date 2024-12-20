using Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new TransactionId(x));

        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.EmployeeId).IsRequired();

        builder.HasOne(x => x.Employee)
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .HasConstraintName("fk_transaction_employee")
            .OnDelete(DeleteBehavior.Restrict);
    }
}