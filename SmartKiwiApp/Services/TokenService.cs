using System.Text;
using SmartKiwiApp.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SmartKiwiApp.Services;
public class TokenService:ITokenService
{
    public string GenerateToken(User user)
    {

        var privateKEy = JwtConfig.PrivateKey;
        var encodedKey = Encoding.ASCII.GetBytes(privateKEy);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), algorithm:SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = GenerateClaims(user),
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2),
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        
        return handler.WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string userToken)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(JwtConfig.PrivateKey);

    try
    {
        var principal = tokenHandler.ValidateToken(userToken, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateIssuer = false,   
            ValidateAudience = false, 

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero 
        }, out SecurityToken validatedToken);

        return principal;
    }
    catch
    {
        return null;
    }
}

    private static ClaimsIdentity GenerateClaims(User user)
    {
        var claimIdentity = new ClaimsIdentity();
        claimIdentity.AddClaim(new Claim(type:ClaimTypes.Name, value:user.Email));
        claimIdentity.AddClaim(new Claim(type:ClaimTypes.Role, value:user.UserRole.ToString()));

        return claimIdentity;
    }
}