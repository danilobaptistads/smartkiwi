using SmartKiwiApp.Dto;
using SmartKiwiApp.Models;
using Microsoft.AspNetCore.Identity;

namespace SmartKiwiApp.Services;
public class UserService
{
    private readonly UserManager<User> _userManager;
    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<IdentityResult> CreateNewUSer(string name, string email, string rawPassword)
    {
        
        var userDto = new NewUserDto(name,email,rawPassword);
        var newUser = new User()
        {
            Name = userDto.name,
            Email = userDto.email,
            UserName = userDto.email
        };

        return await _userManager.CreateAsync(newUser, userDto.rawPassword);
        
    }

    public async Task UpdateUserName(Guid currentUSerID, string newName)
    {
        var userToUpdate = await _userRepository.GetUserById(currentUSerID);
        if(userToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
    
        await _userRepository.UpdateName(userToUpdate, newName);
    }

    public async Task UpdateUserEmail(Guid currentUSerID, string currentUserPassword,string newName)
    {
        var userToUpdate = await _userRepository.GetUserById(currentUSerID);
        if(userToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        if(!userToUpdate.ValidatePassword(currentUserPassword, _passwordService))
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        await _userRepository.UpdateEmail(userToUpdate, newName);
    }

    public async Task UpdateUserPassword(Guid currentUSerId, string newPassword, string informedPassword)
    {
        var userToUpdate = await _userRepository.GetUserById(currentUSerId);
        if(userToUpdate == null || !userToUpdate.ValidatePassword(informedPassword, _passwordService))
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        await _userRepository.UpdatePassword(userToUpdate, newPassword, informedPassword, _passwordService);
    }

    public async Task DeleteCurrentUser(Guid currentUserId,string informedPassword)
    {
        var userToDelete = await _userRepository.GetUserById(currentUserId);
        if(userToDelete == null || !userToDelete.ValidatePassword(informedPassword, _passwordService))
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }

        await _userRepository.DeleteUser(userToDelete);

    }

    public async Task<string> AuthenticateUser(string informedEmail, string informedPassword)
    {
        var userToAuthenticate = await _userRepository.GetUserByEmail(informedEmail);
        if(userToAuthenticate == null || !userToAuthenticate.ValidatePassword(informedPassword, _passwordService))
        {
            throw new UnauthorizedAccessException("Usuário ou senha inválidos");
            
        }
        return _tokenService.GenerateToken(userToAuthenticate);
        

    }
}