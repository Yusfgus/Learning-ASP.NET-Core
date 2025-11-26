
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IPaymentProvider>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return config["PaymentProvider"] switch
    {
        "Stripe" => new StripePayment(),
        "Paypal" => new PaypalPayment(),
        _ => new PaypalPayment(),
    };
});

var app = builder.Build();

app.MapGet("/pay/{amount}", (IPaymentProvider paymentProvider, decimal amount) =>
{
    return Results.Ok(paymentProvider.Pay(amount));
});

app.Run();
