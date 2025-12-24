using Metrics.OrderServiceApi.Requests;
using Metrics.OrderServiceApi.Responses;

namespace Metrics.OrderServiceApi.Services;

public interface IOrderService
{
    Task<OrderResponse?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<OrderResponse> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);
    Task PayAsync(Guid orderId, PaymentRequest request, CancellationToken cancellationToken = default);
    Task CancelAsync(Guid orderId, CancellationToken cancellationToken = default);
}
