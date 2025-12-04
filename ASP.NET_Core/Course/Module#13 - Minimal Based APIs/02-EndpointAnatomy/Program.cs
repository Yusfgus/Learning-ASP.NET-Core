
using EndpointAnatomy.Data;
using EndpointAnatomy.Models;
using EndpointAnatomy.Responses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();


// Http Verb + Route template + Handler (lambda or Method reference)
app.MapGet("api/products", (ProductRepository repository) =>
{
    List<Product>? products = repository.GetProductsPage();
    return products is null 
            ? Results.Ok(new List<Product>()) 
            : Results.Ok(ProductResponse.FromModels(products));
});


app.MapGet("api/products/{id:guid}", (Guid id, ProductRepository repository) =>
{
    Product? product = repository.GetProductById(id);
    return product is null 
            ? Results.Ok(new List<ProductResponse>()) 
            : Results.Ok(ProductResponse.FromModel(product, repository.GetProductReviews(id)));
});


app.MapGet("api/products/{id:guid}/reviews", GetReviews);


async Task<IResult> GetReviews(Guid id, ProductRepository repository)
{
    await Task.Delay(600);

    List<ProductReview> reviews = repository.GetProductReviews(id);
    return reviews is null 
            ? Results.Ok(new List<ProductReviewResponse>()) 
            : Results.Ok(ProductReviewResponse.FromModels(reviews));
}


app.Run();
