
using System.Threading.Tasks;
using UnitOfWork.Interfaces;
using UnitOfWork.Models;
using UnitOfWork.Requests;
using UnitOfWork.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UnitOfWork.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder productApi = app.MapGroup("/api/products");

        productApi.MapGet("/", GetPaged);
        productApi.MapGet("{productId:guid}", GetProductById).WithName(nameof(GetProductById));
        productApi.MapPost("/", CreateProduct);
        productApi.MapPost("{productId:guid}/reviews", CreateProductReview);
        productApi.MapPut("{productId:guid}", Put);
        productApi.MapDelete("{productId:guid}", Delete);

        return productApi;
    }
    
    private static async Task<IResult> GetPaged(
        IUnitOfWork uof,
        int page = 1,
        int pageSize = 10,
        CancellationToken ct = default)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        int totalCount = await uof.ProductRepository.GetProductsCountAsync(ct);
        var products = await uof.ProductRepository.GetProductsPageAsync(page, pageSize, ct);

        var pagedResult = PagedResult<ProductResponse>.Create(ProductResponse.FromModels(products),
                                                              totalCount,
                                                              page,
                                                              pageSize);

        return Results.Ok(pagedResult);
    }

    private static async Task<Results<Ok<ProductResponse>, NotFound<string>>> GetProductById(
        Guid productId,
        IUnitOfWork uof,
        bool includeReviews = false,
        CancellationToken ct = default)
    {
        var product = await uof.ProductRepository.GetProductByIdAsync(productId, ct);

        if (product is null)
            return TypedResults.NotFound($"Product with Id '{productId}' not found");

        List<ProductReview>? reviews = null;

        if (includeReviews)
            reviews = await uof.ProductRepository.GetProductReviewsAsync(productId, ct);

        return TypedResults.Ok(ProductResponse.FromModel(product, reviews));
    }

    private static async Task<IResult> CreateProduct(
        CreateProductRequest request,
        IUnitOfWork uof,
        CancellationToken ct = default)
    {
        if (await uof.ProductRepository.ExistsByNameAsync(request.Name, ct))
            return Results.Conflict($"A product with the name '{request.Name}' already exists.");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price
        };

        uof.ProductRepository.AddProduct(product);
        await uof.SaveChangesAsync();

        return Results.CreatedAtRoute(routeName: nameof(GetProductById),
                                      routeValues: new { productId = product.Id },
                                      value: ProductResponse.FromModel(product));
    }

    private static async Task<IResult> CreateProductReview(
        Guid productId,
        CreateProductReviewRequest request,
        IUnitOfWork uof,
        CancellationToken ct = default)
    {
        if (!await uof.ProductRepository.ExistsByIdAsync(productId, ct))
            return Results.NotFound($"Product with Id '{productId}' not found");

        var productReview = new ProductReview
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Reviewer = request.Reviewer,
            Stars = request.Stars
        };

        await uof.ProductRepository.AddProductReviewAsync(productReview, ct);
        await uof.SaveChangesAsync();

        return Results.Created($"/api/products/{productId}/reviews/{productReview.Id}",
                               ProductReviewResponse.FromModel(productReview));
    }

    private static async Task<IResult> Put(
        Guid productId,
        UpdateProductRequest request,
        IUnitOfWork uof,
        CancellationToken ct = default)
    {
        var product = await uof.ProductRepository.GetProductByIdAsync(productId, ct);

        if (product is null)
            return Results.NotFound($"Product with Id '{productId}' not found");

        product.Name = request.Name;
        product.Price = request.Price ?? 0;

        await uof.ProductRepository.UpdateProductAsync(product, ct);
        await uof.SaveChangesAsync();

        return Results.NoContent();
    }

    private static async Task<IResult> Delete(
        Guid productId,
        IUnitOfWork uof,
        CancellationToken ct = default)
    {
        if (!await uof.ProductRepository.ExistsByIdAsync(productId, ct))
            return Results.NotFound($"Product with Id '{productId}' not found");

        await uof.ProductRepository.DeleteProductAsync(productId, ct);
        await uof.SaveChangesAsync();

        return Results.NoContent();
    }

}

