
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// add parameter transformer
builder.Services.AddRouting(options =>
{
    options.ConstraintMap["slugify"] = typeof(SlugifyTransformer);
});


var app = builder.Build();


app.MapGet("/book/{title:slugify}", (string title) =>
{
    return Results.Ok(new {title});
})
.WithName("BookByTitle");


app.MapGet("/generate", (LinkGenerator link, HttpContext context) =>
{
    var url = link.GetPathByName("BookByTitle", new
    {
        title = "Clean Code A Handbook of Agile Software Craftsmanship"
    });

    return Results.Ok(new { generatedUrl = url });
});

app.Run();


// from: Clean Code A Handbook of Agile Software Craftsmanship
// to: clean-code-a-handbook-of-agile-software-craftsmanship
class SlugifyTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value is null
            ? null
            : Regex.Replace(value.ToString()!, "([a-z])([A-Z])", "$1-$2")
            .Replace(" ", "-")
            .ToLowerInvariant();
    }
}