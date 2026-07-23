using Microsoft.AspNetCore.Identity;
namespace SmartKiwiApp.Models;
public class User : IdentityUser<Guid>
{
    public string Name { get;  set; } = string.Empty;
    public UserRole UserRole{ get; set; }
    

}