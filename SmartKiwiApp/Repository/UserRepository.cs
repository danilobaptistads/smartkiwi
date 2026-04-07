using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;

namespace SmartKiwiApp.Repository;

public class UserRepository
{
    protected readonly SmartKiwiContextInMemory _context;

    public UserRepository(SmartKiwiContextInMemory context)
    {
        _context = context;

    }


    public virtual async Task Add(User newUser)
    {
        var exist = await _context.Users.AnyAsync(u => u.Email == newUser.Email);
        if(exist)
        {
            throw new InvalidOperationException("Email já cadastrado");
        }
        
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        
    }

    public async Task<User> GetUserByEmail(string userEmail)
    {
        var returnedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        if(returnedUser == null)
        {
            throw new InvalidOperationException("Usuárionão encontrado");
        }

        return returnedUser;
    }


}