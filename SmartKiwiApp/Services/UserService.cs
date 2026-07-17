using SmartKiwiApp.Dto;
using SmartKiwiApp.Models;
using Microsoft.AspNetCore.Identity;

namespace SmartKiwiApp.Services;
public class UserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public async Task<IdentityResult> CreateNewUSer(CreateUserRequest userDto)
    {    
        var newUser = new User()
        {
            Name = userDto.Name,
            Email = userDto.Email,
            UserName = userDto.Email
        };
        
        return await _userManager.CreateAsync(newUser, userDto.RawPassword);
        
    }
    public async Task<IdentityResult> UpdateUserName(UpdateNameRequest userDto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userDto.Id);
        if(userToUpdate == null)
        {
            return IdentityResult.Failed();
        }
        userToUpdate.Name = userDto.NewName;
        
        return await _userManager.UpdateAsync(userToUpdate);
    }
    public async Task<IdentityResult> UpdateUserEmail(UpdateEmailRequest userDto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userDto.Id);
        if(userToUpdate == null)
        {
            return IdentityResult.Failed();
        }
        
        var isValidPassword = await _userManager.CheckPasswordAsync(userToUpdate, userDto.InformedPassword);
        if(!isValidPassword)
        {
             return IdentityResult.Failed();
        }

        userToUpdate.Email = userDto.NewEmail;

        return await _userManager.UpdateAsync(userToUpdate);
    }
    public async Task<IdentityResult> UpdateUserPassword(UpdatePasswordRequest userDto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userDto.Id);
        if(userToUpdate == null)
        {
            return IdentityResult.Failed();
        }

        return await _userManager.ChangePasswordAsync(userToUpdate, userDto.InformedPassword,userDto.NewPassword);
    }
    public async Task<IdentityResult> DeleteCurrentUser(RemoveUserRequest userDto)
    {
        var userToDelete = await _userManager.FindByIdAsync(userDto.Id);
        if(userToDelete == null)
        {
            return IdentityResult.Failed();
        }
        var isValidPassword = await _userManager.CheckPasswordAsync(userToDelete, userDto.InformedPassword);
        if (!isValidPassword)
        {
            return IdentityResult.Failed();
        }

        return await _userManager.DeleteAsync(userToDelete);

    }
    public async  Task<User> AuthenticateUser(LoginRequest userDto)
    {
    
        var userToAuthenticate = await _userManager.FindByEmailAsync(userDto.InformedEmail);
        if(userToAuthenticate == null)
        {
            return null!;
        }
        
        var isValidPassword = await _userManager.CheckPasswordAsync(userToAuthenticate, userDto.InformedPassword);

        if (!isValidPassword)
        {
            return null!;
        }
        return userToAuthenticate;

    }
}