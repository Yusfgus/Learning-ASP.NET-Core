namespace ILoggerCategory.Services;

public class OrderService(ILogger<OrderService> logger)
{
    public Task ProcessOrder(Guid orderId)
    {
        logger.LogInformation("Order {OrderId} processing started", orderId);

        return Task.CompletedTask;
    }
}
