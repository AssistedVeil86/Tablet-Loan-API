using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabletLoan.VSA.Domain.Entities;

namespace TabletLoan.VSA.Infrastructure.Data.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.StudentCif)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.StudentName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.StudentLastname)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(e => e.TabletId);

        builder.Property(e => e.status)
            .HasConversion<string>();

        builder.HasOne(e => e.Tablet).WithMany(t => t.Loans)
            .HasForeignKey(e => e.TabletId).OnDelete(DeleteBehavior.Restrict);
    }
}