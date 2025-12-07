
using UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnitOfWork.Data.Config;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);  // set as primary key
        builder.Property(x => x.Id)
                .ValueGeneratedNever();

        builder.Property(x => x.Name)
                .HasColumnType("VARCHAR") // varchar(255)
                .HasMaxLength(100)
                .IsRequired();  // NOT NULL

        builder.Property(x => x.Price)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

        builder.Property(x => x.AverageRating)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

        builder.HasMany(x => x.ProductReviews)
                .WithOne()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

        // default data
        builder.HasData(SeedData.LoadProducts());
    }

}
