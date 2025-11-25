
var builder = WebApplication.CreateBuilder(args);

// File-based Configuration ( json file )
builder.Configuration.AddJsonFile(path: "customconfig.json", optional: false, reloadOnChange: true)
                    .AddIniFile(path: "customconfig.ini", optional: false, reloadOnChange: true);


// In-Memory Configuration
var inMemory = new Dictionary<string, string?>
{
    { "INMEMORY_VAR", "Hello from memory!" }
};
builder.Configuration.AddInMemoryCollection(inMemory);



var app = builder.Build();


app.MapGet("/{key}", (string key, IConfiguration config) => 
{
    return config[key];
});

app.Run();
