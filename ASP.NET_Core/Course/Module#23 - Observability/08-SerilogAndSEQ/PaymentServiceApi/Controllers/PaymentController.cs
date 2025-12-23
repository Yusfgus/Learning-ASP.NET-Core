using SerilogAndSEQ.PaymentServiceApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace SerilogAndSEQ.PaymentServiceApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(IConfiguration configuration, ILogger<PaymentController> logger) : ControllerBase
{
    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
    {
        logger.LogInformation("Start processing payment for OrderId: {OrderId}, Amount: {Amount}", request.OrderId, request.Amount);
        
        try
        {
            if (request == null || request.OrderId == Guid.Empty || request.Amount <= 0)
            {
                logger.LogWarning("Invalid payment request received. OrderId: {OrderId}, Amount: {Amount}", request?.OrderId, request?.Amount);

                return BadRequest("Invalid payment request");
            }

            if (string.IsNullOrWhiteSpace(configuration["PaymentGateway:ApiKey"]))
            {
                logger.LogError("Fatal: Missing API Key. Payment failed for OrderId: {OrderId}", request.OrderId);

                throw new InvalidOperationException("Fatal: Missing API Key");
            }

            // Simulate processing delay
            await Task.Delay(Random.Shared.Next(100, 500));

            // Mock success/failure
            if (Random.Shared.NextDouble() <= 0.1)
            {
                logger.LogWarning("Payment failed for OrderId: {OrderId}", request.OrderId);

                return StatusCode(502, new { Message = "Payment processing failed." });
            }

            logger.LogInformation("Payment succeeded for OrderId: {OrderId}", request.OrderId);

            return Ok(new
            {
                TransactionId = $"txn_{Guid.NewGuid():N}"[..8],
                Success = true
            });
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Unhandled exception during payment processing for OrderId: {OrderId}", request.OrderId);

            return StatusCode(500, new { Message = "Critical error occurred." });
        }
    }
}
