using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;

namespace SmartKiwiApp.Repository;
public class UserRepository: IUserRepository
{
    protected readonly SmartKiwiContext _context;

    public UserRepository(SmartKiwiContext context)
    {
        _context = context;

    }


    public async Task Add(User newUser)
    {
        var exist = await _context.Users.AnyAsync(u => u.Email == newUser.Email);
        if(exist)
        {
            throw new InvalidOperationException("Email já cadastrado");
        }
        
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        
    }

    public async Task<User?> GetUserByEmail(string userEmail)
    {
        var returnedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        if(returnedUser == null)
        {
            throw new InvalidOperationException("Usuárionão encontrado");
        }

        return returnedUser;
    }

    public async Task<User?> GetUserById(Guid Id)
    {
        var returnedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
        if(returnedUser == null)
        {
            throw new InvalidOperationException("Usuárionão encontrado");
        }

        return returnedUser;
    }

    public async Task UpdateEmail(User userToUpdate, string newEmail)
    {
                
        userToUpdate.UpdateEmail(newEmail);
        await _context.SaveChangesAsync();
        
    }

    public async Task UpdateName(User userToUpdate, string newName)
    {

        userToUpdate.UpdateName(newName);
        await _context.SaveChangesAsync();
        
    }

    public async Task UpdatePassword(User userToUpdate,  string newPassword, string informedPassword, IPasswordService passwordService)
    {
        userToUpdate.ChangePassword(newPassword, informedPassword, passwordService);

        await _context.SaveChangesAsync();

    }

    public async Task DeleteUser(User userToDelete)
    {
    _context.Users.Remove(userToDelete);
    await _context.SaveChangesAsync();
        
    }

}