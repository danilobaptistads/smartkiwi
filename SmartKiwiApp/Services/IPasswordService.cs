namespace SmartKiwiApp.Services;
public interface IPasswordService
{
    string ProcssesHashNewPassword(string rawPassword);
    bool ValidatePasswordFormat(string rawPassword);
    bool ValidatePassword(string informedPassword, string hashedPassword);
}