namespace StructuredLogging.Services;

public class OrderService(ILogger<OrderService> logger)
{
    public Task ProcessOrder(Guid orderId, Guid userId)
    {
        // unstructured logging
        logger.LogError($"Failed to process order {orderId} for user {userId}");

        // structured logging
        logger.LogError("Failed to process order {Order Id} for user {User Id}", orderId, userId);

        return Task.CompletedTask;
    }
}
