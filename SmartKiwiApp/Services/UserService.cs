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
    public async Task<IdentityResult> CreateNewUSer(CreateUserRequestDto userDto)
    {    
        var newUser = new User()
        {
            Name = userDto.name,
            Email = userDto.email,
            UserName = userDto.email
        };

        return await _userManager.CreateAsync(newUser, userDto.rawPassword);   
    }
    public async Task<IdentityResult> UpdateUserName(UpdateUserNameDto userDto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userDto.currentUserId);
        if(userToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        userToUpdate.Name = userDto.newName;
        
        return await _userManager.UpdateAsync(userToUpdate);
    }
    public async Task<IdentityResult> UpdateUserEmail(UpdateUserEmailDto userDto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userDto.currentUserId);
        if(userToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        
        var isValidPassword = await _userManager.CheckPasswordAsync(userToUpdate, userDto.informedPassword);
        if(!isValidPassword)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }

        userToUpdate.Email = userDto.newEmail;

        return await _userManager.UpdateAsync(userToUpdate);
    }

    public async Task<IdentityResult> UpdateUserPassword(UpdateUserPasswordDto userDto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userDto.currentUserId);
        if(userToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }

        return await _userManager.ChangePasswordAsync(userToUpdate, userDto.informedPassword,userDto.newPassword);
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