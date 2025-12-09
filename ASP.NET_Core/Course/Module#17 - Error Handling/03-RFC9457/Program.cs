
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-handling/development");
}
else
{
    app.UseExceptionHandler("/error-handling/production");
}

app.MapControllers();

app.Run();
