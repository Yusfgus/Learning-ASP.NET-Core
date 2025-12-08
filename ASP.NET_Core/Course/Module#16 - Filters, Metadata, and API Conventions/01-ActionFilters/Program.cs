
using ActionFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // options.Filters.Add<TrackActionTimeFilterV1>(); // global filter registration ( will take action on all controllers/actions )
});

// builder.Services.AddScoped<TrackActionTimeFilterV1>();

var app = builder.Build();

app.MapControllers();

app.Run();
