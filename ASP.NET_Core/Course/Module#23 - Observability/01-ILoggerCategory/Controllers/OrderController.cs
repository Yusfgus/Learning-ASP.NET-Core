using ILoggerCategory.Services;
using Microsoft.AspNetCore.Mvc;

namespace ILoggerCategory.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(OrderService orderService) : ControllerBase
{

    [HttpPost("{orderId:guid}/process")]
    public IActionResult Process(Guid orderId)
    {

        orderService.ProcessOrder(orderId);

        return Ok(new
        {
            OrderId = orderId,
            Status = "Processed"
        });
    }
}
