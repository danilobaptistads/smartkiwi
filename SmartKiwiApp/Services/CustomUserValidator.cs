using Microsoft.AspNetCore.Identity;
using SmartKiwiApp.Models;
public class CustomUserValidator : IUserValidator<User>
{
     public Task<IdentityResult> ValidateAsync(
        UserManager<User> manager,
        User user)
    {
        if (string.IsNullOrWhiteSpace(user.Name))
        {
            var error = new IdentityError
                {
                    Code = "NameRequired",
                    Description = "O nome é obrigatório."
                };
                
            return Task.FromResult(IdentityResult.Failed(
                error));
        }

        return Task.FromResult(IdentityResult.Success);
    }
}