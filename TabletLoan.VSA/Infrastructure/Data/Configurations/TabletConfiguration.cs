using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TabletLoan.VSA.Domain.Entities;

namespace TabletLoan.VSA.Infrastructure.Data.Configurations;

public class TabletConfiguration : IEntityTypeConfiguration<Tablet>
{
    public void Configure(EntityTypeBuilder<Tablet> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(e => e.ServoPin)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(e => e.SwitchPin)
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(e => e.AirDroidDeviceId)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.AirDroidCDeviceId)
            .HasMaxLength(50)
            .IsRequired();
    }
}