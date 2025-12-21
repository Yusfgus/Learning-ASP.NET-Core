using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

app.MapControllers();

app.Run();

// docker container run -d --name paymentservice --network OrderPaymentSystem -p 9093:80 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_URLS=http://+:80 paymentservice