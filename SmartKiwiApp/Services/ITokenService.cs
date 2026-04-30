using SmartKiwiApp.Models;

public interface ITokenService
{
    public string GenerateToken(User user);
    public bool ValidateToken(string userToken);
}