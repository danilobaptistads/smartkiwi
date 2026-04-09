namespace SmartKiwiApp.Models;
public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    protected User() { }
    public User(string name, string email, string password)
    {
        Id =  Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
    }

    public void UpdateEmail(string newEmail)
    {
        if(newEmail != null)
        {
            Email = newEmail;   
        }
    }
    public void UpdateName(string newName)
    {
        if(newName != null)
        {
            Name = newName;   
        }
    }
}