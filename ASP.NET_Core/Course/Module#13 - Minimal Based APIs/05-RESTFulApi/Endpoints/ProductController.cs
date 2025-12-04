
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using RESTFulApi.Data;
using RESTFulApi.Models;
using RESTFulApi.Requests;
using RESTFulApi.Responses;

namespace RESTFulApi.Endpoints;

public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder productApi = app.MapGroup("/api/products");

        productApi.MapMethods("/", ["OPTIONS"], OptionsProducts);

        productApi.MapMethods("/{productId:guid}", ["HEAD"], HeadProducts);

        productApi.MapGet("/{productId:guid}", GetProductById)
                .WithName("GetProductById");

        productApi.MapGet("/", GetPaged);

        productApi.MapPost("/", CreateProduct);

        productApi.MapPut("/{productId:guid}", UpdateProduct);

        productApi.MapPatch("/{productId:guid}", Patch);

        productApi.MapDelete("/{productId:guid}", DeleteProduct);

        productApi.MapPost("/process", ProcessAsync);

        productApi.MapGet("status/{jobId}", GetProcessingStatus);

        productApi.MapGet("csv", GetProductsCSV);

        productApi.MapGet("physical-csv", GetPhysicalFile);

        productApi.MapGet("products-legacy", GetRedirect);

        productApi.MapGet("temp-products", TempProducts);

        productApi.MapGet("legacy-products", GetPermanentRedirect);

        productApi.MapGet("product-catalog", Catalog);

        return productApi;
    }

    private static IResult OptionsProducts(HttpResponse response)
    {
        response.Headers.Append("Allow", "GET, HEAD, POST, PUT PATCH DELETE, OPTIONS");

        return Results.NoContent();
    }

    //======================================================================================================

    private static IResult HeadProducts(Guid productId, ProductRepository repository)
    {
        return repository.ExistsById(productId)? Results.Ok() : Results.NotFound();
    }

    //======================================================================================================

    private static Results<Ok<ProductResponse>, NotFound> GetProductById(ProductRepository repository, Guid productId, bool includeReviews = false)
    {
        Product? product = repository.GetProductById(productId);

        if(product is null)
            return TypedResults.NotFound();

        List<ProductReview>? reviews = includeReviews
                                       ? repository.GetProductReviews(productId) 
                                       : null;

        return TypedResults.Ok(ProductResponse.FromModel(product, reviews));
    }


    private static IResult GetPaged(ProductRepository repository, int page = 1, int pageSize = 10)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        int totalCount = repository.GetProductsCount();
        List<Product> products = repository.GetProductsPage(page, pageSize);

        PagedResult<ProductResponse> pageResult = PagedResult<ProductResponse>.Create(ProductResponse.FromModels(products),
                                                                                      totalCount,
                                                                                      page,
                                                                                      pageSize);

        return Results.Ok(pageResult);
    }

    //======================================================================================================

    private static IResult CreateProduct(CreateProductRequest request, ProductRepository repository)
    {
        if (repository.ExistsByName(request.Name))
            return Results.Conflict($"A product with the same name '{request.Name}' already exists.");

        Product product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Id = Guid.NewGuid()
        };

        repository.AddProduct(product);

        return Results.CreatedAtRoute(routeName: "GetProductById",
                            routeValues: new { productId = product.Id},
                            value: ProductResponse.FromModel(product)
                        );
    }

    //======================================================================================================

    private static IResult UpdateProduct(Guid productId, UpdateProductRequest request, ProductRepository repository)
    {
        if(!repository.ExistsById(productId))
            return Results.NotFound($"No product with Id '{productId}' was found");

        Product product = new Product
        {
            Name = request.Name,
            Price = request.Price ?? 0
        };

        if(!repository.UpdateProduct(product))
            return Results.StatusCode(500);

        return Results.NoContent();
    }

    //======================================================================================================

    private static async Task<IResult> Patch(Guid productId, ProductRepository repository, HttpRequest httpRequest)
    {
        using var reader = new StreamReader(httpRequest.Body);

        var json = await reader.ReadToEndAsync();

        JsonPatchDocument<UpdateProductRequest>? patchDoc = JsonConvert.DeserializeObject<JsonPatchDocument<UpdateProductRequest>>(json);

        if(patchDoc is null)
            return Results.BadRequest("Invalid patch document");

        Product? product = repository.GetProductById(productId);

        if(product is null)
            return Results.NotFound($"No product with Id '{productId}' was found");

        UpdateProductRequest updateProduct = new UpdateProductRequest
        {
            Name = product.Name,
            Price = product.Price
        };

        patchDoc.ApplyTo(updateProduct);

        product.Name = updateProduct.Name;
        product.Price = updateProduct.Price ?? 0;

        if(!repository.UpdateProduct(product))
            return Results.StatusCode(500);

        return Results.NoContent();
    }

    //======================================================================================================

    private static IResult DeleteProduct(Guid productId, ProductRepository repository)
    {
        if(!repository.ExistsById(productId))
            return Results.NotFound($"No product with Id '{productId}' was found");

        if (!repository.DeleteProduct(productId))
            return Results.StatusCode(500);

        return Results.NoContent();
    }

    //======================================================================================================

    private static IResult ProcessAsync()
    {
        var jobId = Guid.NewGuid();

        return Results.Accepted(
            $"/api/products/status/{jobId}",
            new { jobId, status = "Processing" }
        );
    }
    
    
    private static IResult GetProcessingStatus(Guid jobId)
    {
        bool isStillProcessing = new Random().Next(0, 1) == 1; // fake it

        return Results.Ok(new { jobId, status = isStillProcessing ? "Processing" : "Completed" });
    }

    //======================================================================================================
    
    private static IResult GetProductsCSV(ProductRepository repository)
    {
        var products = repository.GetProductsPage(1, 100);

        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("Id,Name,Price");

        foreach (var p in products)
        {
            csvBuilder.AppendLine($"{p.Id},{p.Name},{p.Price}");
        }

        var fileBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());

        return Results.File(fileBytes, "text/csv", "product-catalog_1_100.csv");
    }


    private static IResult GetPhysicalFile()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "product-catalog_1_100.csv");

        return TypedResults.PhysicalFile(filePath, "text/csv", "products-export.csv");
    }

    //======================================================================================================
    
    private static IResult GetRedirect()
    {
        return Results.Redirect("/api/products/temp-products");
    }


    private static IResult TempProducts()
    {
        return Results.Ok(new { message = "You're in the temp endpoint. Chill." });
    }

    //---------------------------------------------------

    private static IResult GetPermanentRedirect()
    {
        return Results.Redirect("/api/products/product-catalog", permanent: true);
    }


    private static IResult Catalog()
    {
        return Results.Ok(new { message = "This is the permanent new location." });
    }
}
