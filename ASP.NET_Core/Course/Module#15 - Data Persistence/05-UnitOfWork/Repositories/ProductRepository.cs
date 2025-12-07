
using System.Threading.Tasks;
using UnitOfWork.Data;
using UnitOfWork.Interfaces;
using UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<int> GetProductsCountAsync(CancellationToken ct = default) 
        => await context.Products.CountAsync(ct);

    public async Task<List<Product>> GetProductsPageAsync(int page = 1, int pageSize = 10, CancellationToken ct = default)
    {
        return await context.Products
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync(ct);
    }

    public async Task<Product?> GetProductByIdAsync(Guid productId, CancellationToken ct = default)
        => await context.Products.FirstOrDefaultAsync(p => p.Id == productId, ct);

    public async Task<List<ProductReview>> GetProductReviewsAsync(Guid productId, CancellationToken ct = default)
        => await context.ProductReviews.Where(r => r.ProductId == productId).ToListAsync(ct);

    public async Task<ProductReview?> GetReviewAsync(Guid productId, Guid reviewId, CancellationToken ct = default)
        => await context.ProductReviews.FirstOrDefaultAsync(r => r.ProductId == productId && r.Id == reviewId, ct);

    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public async Task AddProductReviewAsync(ProductReview review, CancellationToken ct = default)
    {
        var product = await context.Products
                                    .Include(p => p.ProductReviews)
                                    .FirstOrDefaultAsync(p => p.Id == review.ProductId, ct);
        
        if (product is null)
            throw new InvalidOperationException();

        context.ProductReviews.Add(review);
        
        product.AverageRating = Math.Round(product.ProductReviews.Average(r => r.Stars), 1, MidpointRounding.AwayFromZero);
    }

    public async Task UpdateProductAsync(Product updatedProduct, CancellationToken ct = default)
    {
        var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == updatedProduct.Id, ct);
        if (existingProduct == null)
            throw new InvalidOperationException();

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Price = updatedProduct.Price;
    }

    public async Task DeleteProductAsync(Guid id, CancellationToken ct = default)
    {
        var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (product == null)
            throw new InvalidOperationException();

        context.Products.Remove(product);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default)
        => await context.Products.AnyAsync(p => p.Id == id, ct);

    public async Task<bool> ExistsByNameAsync(string? name, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        return await context.Products.AnyAsync(p => EF.Functions.Like(p.Name!.ToUpper(), name.ToUpper()), ct);
    }
}

