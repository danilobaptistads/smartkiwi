using SmartKiwiApp.Models;
namespace SmartKiwiApp.Dto;
public record CreateUserRequestDto (string name, string email, string rawPassword, User.Role role );
public record UpdateUserNameDto (string currentUserId, string newName);
public record UpdateUserEmailDto (string currentUserId, string informedPassword,string newEmail);
public record UpdateUserPasswordDto (string currentUserId, string newPassword, string informedPassword);


