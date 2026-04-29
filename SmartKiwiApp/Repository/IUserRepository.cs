
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
namespace SmartKiwiApp.Repository;

public interface IUserRepository
{
    public Task Add(User newUser);
    public Task<User> GetUserByEmail(string userEmail);
    public  Task<User> GetUserById(Guid Id);
    public  Task UpdateEmail(User userToUpdate, string newEmail);
    public  Task UpdateName(User userToUpdate, string newName);
    public  Task UpdatePassword(User userToUpdate,  string newPassword, string informedPassword, IPasswordService hasher);
    public  Task DeleteUser(User userToDelete);

}