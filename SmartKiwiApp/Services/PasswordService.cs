using System.Text.RegularExpressions;

namespace SmartKiwiApp.Services;
public class PasswordService:IPasswordService
{
    private readonly IHashService _hashService;
    public PasswordService(IHashService hashService)
    {
        _hashService = hashService;
    }
    public string ProcssesHashNewPassword(string rawPassword)
    {
       if(ValidatePasswordFormat(rawPassword))
       {
            return _hashService.HashPassword(rawPassword);
       }

       throw new ArgumentException("Formato invalido");
    }

    public bool ValidatePasswordFormat(string rawPassword)
    {
        if (string.IsNullOrEmpty(rawPassword) )
        {
            return false;
        }
        if (rawPassword.Length < 8)
        {
            return false;
        }
        
        var hasSpeciasChar = Regex.IsMatch(rawPassword, "[^a-zA-Z0-9]");
        if (!hasSpeciasChar)
        {
            return false;
        }
        var hasUpercase = Regex.IsMatch(rawPassword, "[A-Z]");
        if (!hasUpercase)
        {
            return false;
        }
        return true;
    }
    
    public bool ValidatePassword(string informedPassword, string hashedPassword)
    {
        if (!_hashService.VerifyPassword(informedPassword, hashedPassword))
        {
            return false;
        }
        return true;
    }
}