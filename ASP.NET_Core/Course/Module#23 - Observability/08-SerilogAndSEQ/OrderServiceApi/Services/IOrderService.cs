using SerilogAndSEQ.OrderServiceApi.Requests;
using SerilogAndSEQ.OrderServiceApi.Responses;

namespace SerilogAndSEQ.OrderServiceApi.Services;

public interface IOrderService
{
    Task<OrderResponse?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<OrderResponse> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
    Task PayAsync(Guid orderId, PaymentRequest request, CancellationToken cancellationToken = default);
    Task CancelAsync(Guid orderId, CancellationToken cancellationToken = default);
}
