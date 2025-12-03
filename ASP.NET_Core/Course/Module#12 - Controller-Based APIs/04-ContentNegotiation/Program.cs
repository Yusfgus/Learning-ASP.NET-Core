
using ContentNegotiation.Data;
using ContentNegotiation.Formatters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
                {
                    options.ReturnHttpNotAcceptable = true;
                    options.OutputFormatters.Add(new PlainTextTableOutputFormatter());
                })
                .AddXmlSerializerFormatters();

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
