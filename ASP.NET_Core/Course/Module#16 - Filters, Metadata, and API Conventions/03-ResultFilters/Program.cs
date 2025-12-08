
using ResultFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<EnvelopResultFilter>();
});

var app = builder.Build();

app.MapControllers();

app.Run();
