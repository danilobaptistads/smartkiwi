
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
namespace SmartKiwiApp.Repository;

public interface IUserRepository
{
    public Task Add(User newUser);
    public Task<User> GetUserByEmail(string userEmail);
    public  Task<User> GetUserById(Guid Id);
    public  Task UpdateEmail(Guid currentUserId, string newEmail);
    public  Task UpdateName(Guid currentUserId, string newName);
    public  Task UpdatePassword(Guid currentUserId,  string newPassword, string informedPassword, IHashService hasher);

}