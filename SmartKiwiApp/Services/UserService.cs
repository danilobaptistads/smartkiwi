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
            Name = userDto.name,
            Email = userDto.email,
            UserName = userDto.email
        };

        return await _userManager.CreateAsync(newUser, userDto.rawPassword);   
    }
    public async Task<IdentityResult> UpdateUserName(UpdateNameRequest userDto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userDto.Id);
        if(userToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        userToUpdate.Name = userDto.newName;
        
        return await _userManager.UpdateAsync(userToUpdate);
    }
    public async Task<IdentityResult> UpdateUserEmail(UpdateEmailRequest userDto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userDto.Id);
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
    public async Task<IdentityResult> UpdateUserPassword(UpdatePasswordRequest userDto)
    {
        var userToUpdate = await _userManager.FindByIdAsync(userDto.Id);
        if(userToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }

        return await _userManager.ChangePasswordAsync(userToUpdate, userDto.informedPassword,userDto.newPassword);
    }
    public async Task<IdentityResult> DeleteCurrentUser(RemoveUserRequest userDto)
    {
        var userToDelete = await _userManager.FindByIdAsync(userDto.Id);
        if(userToDelete == null)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }
        var isValidPassword = await _userManager.CheckPasswordAsync(userToDelete, userDto.informedPassword);
        if (!isValidPassword)
        {
            throw new ArgumentException("Não foi possivel realizar a alteração");
        }

        return await _userManager.DeleteAsync(userToDelete);

    }
    public async  Task<SignInResult> AuthenticateUser(LoginRequest userDto)
    {
        return await _signInManager.PasswordSignInAsync(
            userDto.informedEmail, 
            userDto.informedPassword,
            isPersistent: true,
            lockoutOnFailure: true
           
            );

    }
}