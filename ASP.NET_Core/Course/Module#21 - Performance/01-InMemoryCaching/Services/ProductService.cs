using InMemoryCaching.Data;
using InMemoryCaching.Models;
using InMemoryCaching.Requests;
using InMemoryCaching.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Services;

public class ProductService(AppDbContext context, IMemoryCache cache) : IProductService
{
    public async Task<List<ProductResponse>> GetProductsAsync_Old()
    {
        var cacheKey = "products:all";

        // try get from cache
        if (cache.TryGetValue(cacheKey, out List<ProductResponse>? products))
        {
            Console.WriteLine("Cache visited");
            return products!;
        }

        List<Product>? entities = await context.Products.ToListAsync();
        Console.WriteLine("Database visited");

        products = entities?.Select(p => ProductResponse.FromModel(p)).ToList() ?? [];

        // save in cache
        cache.Set(cacheKey, products, new MemoryCacheEntryOptions
        {
            Size = 1, // take one unit
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20), // TTL (time to live)
        });

        return products;
    }

    public async Task<List<ProductResponse>> GetProductsAsync()
    {
        return await cache.GetOrCreate("products:all", async (ICacheEntry entry) =>
        {
            entry.Size = 1; // take one unit
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20); // TTL (time to live)
            entry.Priority = CacheItemPriority.High; // When memory is low, ASP.NET Core removes items based on priority

            List<Product>? entities = await context.Products.ToListAsync();
            Console.WriteLine("Database visited");

            List<ProductResponse> productsResponse = entities?.Select(p => ProductResponse.FromModel(p)).ToList() ?? [];

            return productsResponse;
        })!;
    }

    public async Task<ProductResponse?> GetProductByIdAsync(int productId)
    {
        if (cache.TryGetValue($"products:{productId}", out ProductResponse? product))
        {
            Console.WriteLine("Cache visited");
            return product;
        }

        Product? entity = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        Console.WriteLine("Database visited");

        if (entity is null)
            return null;

        product = ProductResponse.FromModel(entity);

        cache.Set($"products:{productId}", product, new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
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