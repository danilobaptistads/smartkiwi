using SmartKiwiApp.Models;
namespace SmartKiwiApp.Dto;
public record CreateUserRequest (string name, string email, string rawPassword, User.Role role );
public record UpdateNameRequest (string Id, string newName);
public record UpdateEmailRequest (string Id, string informedPassword,string newEmail);
public record UpdatePasswordRequest (string Id, string newPassword, string informedPassword);
 public record RemoveUserRequest(string Id,string informedPassword);
 public record LoginRequest(string informedEmail, string informedPassword);
