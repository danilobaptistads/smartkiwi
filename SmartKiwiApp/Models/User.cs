using SmartKiwiApp.Services;
namespace SmartKiwiApp.Models;
using System.Text.RegularExpressions;

public class User
{
    
    private string _name;
    private string _email;
    private string _password;
    public Guid Id { get; private set; }
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
    private string Password 
    { 
        get => _password;
        set
        {
            ValidatePasswordFormatBase64(value);
            _password = value;
        } 
    }

    protected User() { }
    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
    }

    public void UpdateEmail(string newEmail)
    {


        Email = newEmail;
    }
    public void UpdateName(string newName)
    {

        Name = newName;
    }
    public bool ValidatePassword(string informedPassword, IPasswordHashService hasher)
    {
        var isValidPassword = hasher.VerifyPassword(informedPassword, Password);
        if (!isValidPassword)
        {
            return false;
        }

        return true;

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
    private void ValidatePasswordFormatBase64(string value)
    {
        if (string.IsNullOrEmpty(value) )
        {
            throw new ArgumentException("Senha não pode ser vazia.");
      
        }
        if (!Convert.TryFromBase64String(value, new byte[value.Length], out _))
        {
              throw new ArgumentException("Fomato de senha inválido.");
        }
    }
    public void ChangePassword(string newPassword, string informedPassword, IPasswordHashService hasher)
    {
        var isValidPassword = hasher.VerifyPassword(informedPassword, Password);
        if (!isValidPassword)
        {
            throw new ArgumentException("Não foi possivel alterar a senha");
        }

        Password = hasher.HashPassword(newPassword);

    }
}