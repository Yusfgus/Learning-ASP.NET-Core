using EF_Core.Transactions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EF_Core.Transactions.Data.Config;

public class AccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.HasKey(x => x.AccountId);
        builder.Property(x => x.AccountId).ValueGeneratedNever();

        builder.Property(x => x.AccountId)
                .HasColumnType("VARCHAR")
                .HasMaxLength(20).IsRequired();

        builder.Property(x => x.CurrentBalance)
                .HasColumnType("decimal(18, 2)").IsRequired();

        builder.HasMany(x => x.GLTransactions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("BankAccounts");
    }
}