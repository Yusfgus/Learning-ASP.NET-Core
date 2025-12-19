using System.Text.Json;
using DistributedCaching.Data;
using DistributedCaching.Models;
using DistributedCaching.Requests;
using DistributedCaching.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCaching.Services;

public class ProductService(AppDbContext context, IDistributedCache cache) : IProductService
{
    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        var cacheKey = "products:all";

        List<ProductResponse> products;

        string? cachedData = await cache.GetStringAsync(cacheKey);
        if (cachedData is not null)
        {
            Console.WriteLine("Cache visited");
            products = JsonSerializer.Deserialize<List<ProductResponse>>(cachedData)!;
            return products;
        }

        List<Product>? entities = await context.Products.ToListAsync();
        Console.WriteLine("Database visited");

        products = entities?.Select(p => ProductResponse.FromModel(p)).ToList() ?? [];

        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(products), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
        });

        return products;
    }

    public async Task<ProductResponse?> GetProductByIdAsync(int productId)
    {
        string cacheKey = $"products:{productId}";

        ProductResponse product;

        string? cachedData = await cache.GetStringAsync(cacheKey);

        if (cachedData is not null)
        {
            Console.WriteLine("Cache visited");
            product = JsonSerializer.Deserialize<ProductResponse>(cachedData)!;
            return product;
        }

        Product? entity = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        Console.WriteLine("Database visited");

        if (entity is null)
            return null;

        product = ProductResponse.FromModel(entity);

        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(product), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
        });

        return product;
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

        cache.Remove("products:all");

        return ProductResponse.FromModel(product);
    }

    public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
    {
        Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                        ?? throw new KeyNotFoundException("product not found");

        product.Name = request.Name;
        product.Price = request.Price;

        await context.SaveChangesAsync();

        cache.Remove($"products:{product.Id}");
        cache.Remove("products:all");
    }

    public async Task DeleteProductAsync(int id)
    {
        Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == id)
                        ?? throw new KeyNotFoundException("product not found");

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        cache.Remove($"products:{product.Id}");
        cache.Remove("products:all");
    }
}