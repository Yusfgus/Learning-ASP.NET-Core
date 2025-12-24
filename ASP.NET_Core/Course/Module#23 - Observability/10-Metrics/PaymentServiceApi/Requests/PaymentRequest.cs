using Metrics.PaymentServiceApi.Enums;

namespace Metrics.PaymentServiceApi.Requests;

public class PaymentRequest
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public PaymentMethod PaymentMethod { get; set; }
    public string? CardNumber { get; set; }
    public string? CardHolderName { get; set; }
}