using SmartKiwiApp.Models;

public interface ITokenService
{
    public string GenerateToken(CleintQueue user);
    public bool ValidateToken(string userToken);
}