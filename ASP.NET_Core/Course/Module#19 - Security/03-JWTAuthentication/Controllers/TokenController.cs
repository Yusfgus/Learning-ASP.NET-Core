using JWTAuthentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthentication.Controllers;

[ApiController]
[Route("api/token")]
public class TokenController(JwtTokenProvider tokenProvider) : ControllerBase
{
    [HttpPost("generate")]
    public IActionResult GenerateToken(GenerateTokenRequest tokenRequest)
    {
        return Ok(tokenProvider.GenerateJwtToken(tokenRequest));
    }
}
