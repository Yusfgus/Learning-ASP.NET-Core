using OutputCaching.Models;
using OutputCaching.Requests;
using OutputCaching.Responses;

namespace OutputCaching.Services;

public interface IProductService
{
    Task<PagedResult<ProductResponse>> GetProductsAsync(int page, int pageSize);

    Task<ProductResponse?> GetProductByIdAsync(int productId);

    Task<ProductResponse> AddProductAsync(CreateProductRequest request);

    Task UpdateProductAsync(int productId, UpdateProductRequest request);

    Task DeleteProductAsync(int id);
}
