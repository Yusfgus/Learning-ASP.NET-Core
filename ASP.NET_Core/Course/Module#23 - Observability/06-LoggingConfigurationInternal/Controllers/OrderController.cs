using LoggingConfigurationInternal.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoggingConfigurationInternal.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(OrderService orderService) : ControllerBase
{

    [HttpPost("{orderId:guid}/process")]
    public IActionResult Process(Guid orderId)
    {
        var userId = Guid.Parse("ba36bbfb-e5aa-4bcc-934e-db395cc76898");

        orderService.ProcessOrder(orderId, userId);

        return Ok(new
        {
            OrderId = orderId,
            Status = "Processed"
        });
    }
}
