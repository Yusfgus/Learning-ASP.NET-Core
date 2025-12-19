using HybridCaching.Data;
using HybridCaching.Models;
using HybridCaching.Requests;
using HybridCaching.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace HybridCaching.Services;

public class ProductService(AppDbContext context, HybridCache cache) : IProductService
{
    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        return await cache.GetOrCreateAsync("products:all", async (CancellationToken ct) =>
        {
            List<Product>? entities = await context.Products.ToListAsync(ct);
            Console.WriteLine("Database visited");

            List<ProductResponse> productsResponse = entities?.Select(p => ProductResponse.FromModel(p)).ToList() ?? [];

            return productsResponse;
        },
        options: new HybridCacheEntryOptions
        {
            // Expiration = TimeSpan.FromSeconds(30),
        },
        tags: ["products-tag"]
        );
    }

    public async Task<ProductResponse?> GetProductByIdAsync(int productId)
    {
        return await cache.GetOrCreateAsync($"products:{productId}", async (CancellationToken ct) =>
        {
            Product? entity = await context.Products.FirstOrDefaultAsync(p => p.Id == productId, ct);
            Console.WriteLine("Database visited");

            // this will store the product in the cache even if it is null (not found)
            return entity is null ? null : ProductResponse.FromModel(entity);
        },
        tags: ["products-tag"]
        );
    }

    public async Task<ProductResponse> AddProductAsync(CreateProductRequest request)
    {
        Product product = new()
        {
            Name = request.Name,
            Price = request.Price
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        await cache.RemoveAsync("products:all");
        await cache.RemoveAsync($"products:{product.Id}");  // maybe it was requested before and it wasn't found in db so it was set to null in cache
        // await cache.RemoveByTagAsync("products-tag");

        return ProductResponse.FromModel(product);
    }

    public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
    {
        Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                        ?? throw new KeyNotFoundException("product not found");

        product.Name = request.Name;
        product.Price = request.Price;

        await context.SaveChangesAsync();

        // await cache.RemoveAsync("products:all");
        // await cache.RemoveAsync($"products:{productId}");
        await cache.RemoveByTagAsync("products-tag");
    }

    public async Task DeleteProductAsync(int productId)
    {
        Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                        ?? throw new KeyNotFoundException("product not found");

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        // await cache.RemoveAsync("products:all");
        // await cache.RemoveAsync($"products:{productId}");
        await cache.RemoveByTagAsync("products-tag");
    }
}