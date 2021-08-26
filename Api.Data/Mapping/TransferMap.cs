using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class TransferMap : IEntityTypeConfiguration<TransferEntity>
    {
        public void Configure(EntityTypeBuilder<TransferEntity> builder)
        {
            builder.ToTable("Transfer");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.AccountOrigin)
                   .IsRequired()
                   .HasMaxLength(60);

            builder.Property(u => u.AccountDestination)
                   .IsRequired()
                   .HasMaxLength(60);

            builder.Property(u => u.Value)
                   .IsRequired();

            builder.Property(u => u.Status)
                   .IsRequired()
                   .HasMaxLength(40);

            builder.Property(u => u.Message)
                   .IsRequired()
                   .HasMaxLength(10000);
        }
    }
}
