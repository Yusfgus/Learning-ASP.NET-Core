using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTAuthentication.Responses;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthentication.Services;

public class JwtTokenProvider(IConfiguration configuration)
{
    public TokenResponse GenerateJwtToken(GenerateTokenRequest request)
    {
        // 1) create claims

        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, request.Id!),
            new (JwtRegisteredClaimNames.Email, request.Email!),
            new (JwtRegisteredClaimNames.GivenName, request.FirstName!),
            new (JwtRegisteredClaimNames.FamilyName, request.LastName!),
        };

        foreach(var role in request.Roles)
            claims.Add(new(ClaimTypes.Role, role));

        foreach(var permission in request.Permissions)
            claims.Add(new("permission", permission));

        
        // 2) get jwtSettings from the configurations

        var jwtSettings = configuration.GetSection("JwtSettings");

        var issuer = jwtSettings["Issuer"]!;
        var audience = jwtSettings["Audience"]!;
        var key = jwtSettings["SecretKey"]!;
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["TokenExpirationInMinutes"]!));


        // build token descriptor
        
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature
            )
        };


        // generate jwt token

        var tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken securityToken = tokenHandler.CreateToken(descriptor);

        return new TokenResponse
        {
            AccessToken = tokenHandler.WriteToken(securityToken),
            RefreshToken = "7a6f23b4e1d04c9a8f5b6d7c8a9e01f1",
            Expires = expires
        };
    }
}

