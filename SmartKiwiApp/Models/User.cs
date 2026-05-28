using SmartKiwiApp.Services;
using System.Text.RegularExpressions;

namespace SmartKiwiApp.Models;
public class User
{
    public enum Role { Admin, Employee }

    private string _name = string.Empty;
    private string _email = string.Empty;
    private string _password = string.Empty;
    private Role _role;
    public Guid Id { get; private set; }
    public Role UserRole => _role;
    public string Name 
    { 
        get => _name; 
        private set
        {
            ValidateName(value);
            _name = value;
        } 
    }
    public string Email 
    { 
        get => _email; 
        private set
        {
            ValidateEmail(value);
            _email =  value;
        }
    }
    protected User() { }
    public User(string name, string email, string password, Role role = Role.Employee)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        _password = password;
        _role = role;
    }

    public void UpdateEmail(string newEmail)
    {

        Email = newEmail;
    }
    public void UpdateName(string newName)
    {

        Name = newName;
    }
    public void SetRole(Role role)
    {
        _role = role;
    }
    private void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Nome obrigatório.");
    }
    private void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email obrigatório.");

        var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        if (!Regex.IsMatch(email, pattern))
            throw new ArgumentException("Email inválido.");
    }
    public bool ValidatePassword(string informedPassword, IPasswordService passwordService)
    {
        var isValidPassword = passwordService.ValidatePassword(informedPassword, _password);
        if (!isValidPassword)
        {
            return false;
        }

        return true;

    }
    public void ChangePassword(string newPassword, string informedPassword, IPasswordService passwordService)
    {
        var isValidPassword = passwordService.ValidatePassword(informedPassword, _password);
        if (!isValidPassword)
        {
            throw new ArgumentException("Não foi possivel alterar a senha");
        }

        _password = passwordService.ProcssesHashNewPassword(newPassword);

    }
}