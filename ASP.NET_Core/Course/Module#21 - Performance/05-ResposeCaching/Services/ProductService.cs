using ResponseCaching.Data;
using ResponseCaching.Models;
using ResponseCaching.Requests;
using ResponseCaching.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ResponseCaching.Services;

public class ProductService(AppDbContext context) : IProductService
{
    public async Task<PagedResult<ProductResponse>> GetProductsAsync(int page = 1, int pageSize = 10)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 10);

        List<Product>? entities = await context.Products.AsNoTracking()
                                                        .Skip((page - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .ToListAsync();

        int totalCount = await context.Products.AsNoTracking().CountAsync();

        List<ProductResponse> products = entities?.Select(p => ProductResponse.FromModel(p)).ToList() ?? [];

        return new PagedResult<ProductResponse>
        {
            Items = products,
            TotalCount = totalCount,
            CurrentPage = page,
            PageSize = pageSize
        };
    }

    public async Task<ProductResponse?> GetProductByIdAsync(int productId)
    {
        Product? entity = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        
        return entity is null ? null : ProductResponse.FromModel(entity);
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

        return ProductResponse.FromModel(product);
    }

    public async Task UpdateProductAsync(int productId, UpdateProductRequest request)
    {
        Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId)
                        ?? throw new KeyNotFoundException("product not found");

        product.Name = request.Name;
        product.Price = request.Price;

        await context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == id)
                        ?? throw new KeyNotFoundException("product not found");

        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }
}