
using ActionFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // options.Filters.Add<TrackActionTimeFilterV1>(); // global filter registration ( will take action on all controllers/actions )
});

var app = builder.Build();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();
