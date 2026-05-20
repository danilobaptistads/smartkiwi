namespace SmartKiwiApp.Services;
public interface IHashService
{
    public string HashPassword(string password);
    public bool VerifyPassword(string informedPassword, string hashedPassword);
}