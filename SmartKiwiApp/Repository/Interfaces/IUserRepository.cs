
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
namespace SmartKiwiApp.Repository;

public interface IUserRepository
{
    public Task Add(CleintQueue newUser);
    public Task<CleintQueue> GetUserByEmail(string userEmail);
    public  Task<CleintQueue> GetUserById(Guid Id);
    public  Task UpdateEmail(CleintQueue userToUpdate, string newEmail);
    public  Task UpdateName(CleintQueue userToUpdate, string newName);
    public  Task UpdatePassword(CleintQueue userToUpdate,  string newPassword, string informedPassword, IPasswordService hasher);
    public  Task DeleteUser(CleintQueue userToDelete);

}