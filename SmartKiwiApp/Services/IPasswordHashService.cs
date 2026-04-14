public interface IPasswordHashService
{
    public string HashPassword(string password);
    public bool VerifyPassword(string informedPassword, string hashedPassword);
}