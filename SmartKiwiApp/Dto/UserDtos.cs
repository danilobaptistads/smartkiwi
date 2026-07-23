using SmartKiwiApp.Models;
namespace SmartKiwiApp.Dto;
public record CreateUserRequest (string Name, string Email, string RawPassword, UserRole Role );
public record UpdateNameRequest (string Id, string NewName);
public record UpdateEmailRequest (string Id, string InformedPassword,string NewEmail);
public record UpdatePasswordRequest (string Id, string NewPassword, string InformedPassword);
 public record RemoveUserRequest(string Id,string InformedPassword);
 public record LoginRequest(string InformedEmail, string InformedPassword);
