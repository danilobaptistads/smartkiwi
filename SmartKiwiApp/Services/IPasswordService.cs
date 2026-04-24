namespace SmartKiwiApp.Services;
public interface IPasswordService
{
    string ProcssesHashNewPassword(string rawPassword, IPasswordHashService hashService);
    bool ValidatePasswordFormat(string rawPassword);
}