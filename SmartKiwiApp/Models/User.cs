using SmartKiwiApp.Services;
namespace SmartKiwiApp.Models;
using System.Text.RegularExpressions;

public class CleintQueue
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
    protected CleintQueue() { }
    public CleintQueue(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        _password = password;
    }

    public void UpdateEmail(string newEmail)
    {

        Email = newEmail;
    }
    public void UpdateName(string newName)
    {

        Name = newName;
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
    public bool ValidatePassword(string informedPassword, IPasswordService passworService)
    {
        var isValidPassword = passworService.ValidatePassword(informedPassword, _password);
        if (!isValidPassword)
        {
            return false;
        }

        return true;

    }
    public void ChangePassword(string newPassword, string informedPassword, IPasswordService passworService)
    {
        var isValidPassword = passworService.ValidatePassword(informedPassword, _password);
        if (!isValidPassword)
        {
            throw new ArgumentException("Não foi possivel alterar a senha");
        }

        _password = passworService.ProcssesHashNewPassword(newPassword);

    }
}