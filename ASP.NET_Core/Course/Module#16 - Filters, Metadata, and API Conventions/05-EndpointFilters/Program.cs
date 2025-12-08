
using EndpointFilters.Filters;
using Shared;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Endpoint level Filter Registration

app.MapGet("/api/products", () =>
{
    Utils.Highlight("Action Execution");
    return new[] { "Keyboard [$52.99]", "Mouse, [$34.99]" };
})
.AddEndpointFilter<EnvelopeResultFilter>()
.AddEndpointFilter<TrackActionTimeEndpointFilter>()
;

//==================================================================

// Rout Group level Filter Registration

var group = app.MapGroup("/api/customers")
                .AddEndpointFilter<EnvelopeResultFilter>()
                .AddEndpointFilter<TrackActionTimeEndpointFilter>()
                ;

// Notice that order of registration matters

group.MapGet("", () =>
{
    Utils.Highlight("Action Execution");
    return new[] { "1: Ahmad [HR]", "2: Maisa, [Finance]" };
});

group.MapGet("{id:int}", (int id) =>
{
    Utils.Highlight("Action Execution");
    return new { Id = id, Name = "Ahmad", Role = "HR"};
});

//==================================================================

// No Filter Registration

app.MapGet("/api/employees", () =>
{
    Utils.Highlight("Action Execution");
    return new[] { "1: Yousef [Backend]", "2: Awad, [Frontend]" };
});

app.Run();
