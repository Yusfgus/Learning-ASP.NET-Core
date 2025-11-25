
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// method 1
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(AppSettings.Name));

// // method 2
// builder.Services.AddOptions<AppSettings>().Bind(builder.Configuration.GetSection(AppSettings.Name));

var app = builder.Build();

// read once at startup
app.Map("/ioptions", (IOptions<AppSettings> options) =>
{
    return options.Value;
});
// Use when:
//      - settings are static and never change
//      - performance-critical (fastest)


// reload on each request
app.Map("/ioptions-snapshot", (IOptionsSnapshot<AppSettings> options) =>
{
    return options.Value;
});
// Use when:
//      - running in Development
//      - config file may change during runtime
//      - you want live reload without restarting
// Works only in Scoped (per-request) services
// Does not work in singleton services


// real-time updates + events
app.Map("/ioptions-monitor", (IOptionsMonitor<AppSettings> options) =>
{
    return options.CurrentValue;
});
// Use when:
//      - you need to react to config changes immediately
//      - used in hosted services, background jobs, singletons


app.Run();
