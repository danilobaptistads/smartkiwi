using Microsoft.AspNetCore.Identity;
using SmartKiwiApp.Models;
using System.Text.RegularExpressions;
public class EmailValidator : IUserValidator<User>
{
    public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        if (string.IsNullOrWhiteSpace(user.Email))
        {
            var error = new IdentityError
                {
                    Code = "EmailRequired",
                    Description = "O e-mail é obrigatório."
                };
            return Task.FromResult(IdentityResult.Failed(error));
            
        }
        
        string padrao = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";
        var isValidEmail = Regex.IsMatch(user.Email, padrao, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        
        if (!isValidEmail)
        {
            var error = new IdentityError
                {
                    Code = "InvalidEmail",
                    Description = "Formato email inválido"
                };
            return Task.FromResult(IdentityResult.Failed(error));
        }

        return Task.FromResult(IdentityResult.Success);
    }
    
}
