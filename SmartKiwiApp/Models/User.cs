using Microsoft.AspNetCore.Identity;
namespace SmartKiwiApp.Models;
public class User : IdentityUser<Guid>
{
    public string Name { get;  set; } = string.Empty;
    public enum Role { Admin, Employee }
    public Role UserRole { get; set; }
    public void SetRole(Role role)
    {
        UserRole = role;
    }
}