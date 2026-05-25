using SmartKiwiApp.Models;
using System.Security.Claims;

namespace SmartKiwiApp.Services;
public interface ITokenService
{
    public string GenerateToken(User user);
    public ClaimsPrincipal? ValidateToken(string userToken);
}