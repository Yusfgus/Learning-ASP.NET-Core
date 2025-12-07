
using Cancellation_Token.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cancellation_Token.Data.Config;

public class ProductReviewsConfigurations : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.ToTable("ProductReviews");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .ValueGeneratedNever();

        builder.Property(x => x.ProductId)
                .IsRequired();

        builder.Property(x => x.Reviewer)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

        builder.Property(x => x.Stars)
                .IsRequired();

        // default data
        builder.HasData(SeedData.LoadProductReviews());
    }
}
