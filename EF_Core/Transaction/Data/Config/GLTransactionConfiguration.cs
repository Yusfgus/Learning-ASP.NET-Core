using EF_Core.Transactions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.Transactions.Data.Config;

public class GLTransactionConfiguration : IEntityTypeConfiguration<GLTransaction>
{
    public void Configure(EntityTypeBuilder<GLTransaction> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
                .HasColumnType("decimal(18, 2)").IsRequired()
                .IsRequired();

        builder.Property(x => x.Notes)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();

        builder.Property(x => x.CreatedAt)
                // .HasColumnType("Date")
                .IsRequired();

        builder.ToTable("GLTransactions");
    }
}