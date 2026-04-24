namespace SmartKiwiApp.Services;
public interface IPasswordService
{
    string ProcssesHashNewPassword(string rawPassword, IHashService hashService);
    bool ValidatePasswordFormat(string rawPassword);
}