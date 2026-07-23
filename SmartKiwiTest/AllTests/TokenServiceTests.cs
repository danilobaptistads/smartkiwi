// using SmartKiwiApp.Models;
// using SmartKiwiApp.Services;
// using System.Security.Claims;
// public class TokenServiceTests
// {
//     [Fact]
//     public void Deve_Gerar_Um_Token()
//     {
//         var user = new User("danilo", "danilo@email.com","MyP@ssW0rd");
    
//         var tokenService = new TokenService();
//         var token = tokenService.GenerateToken(user);

    
//         Assert.False(string.IsNullOrWhiteSpace(token));
//         Assert.Equal(3, token.Split('.').Length);
//     }

//    [Fact]
//     public void Deve_Validar_Um_Token()
//     {
//         var user = new User("danilo", "danilo@email.com", "MyP@ssW0rd", User.Role.Admin);
//         var tokenService = new TokenService();
//         var userToken = tokenService.GenerateToken(user);

//         var claimsPrincipal = tokenService.ValidateToken(userToken);
//         var email = claimsPrincipal!.FindFirst(ClaimTypes.Name)?.Value;
//         var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

//         Assert.NotNull(claimsPrincipal);
//         Assert.Equal(user.Email, email);
//         Assert.Equal(user.UserRole.ToString(), role);
//     }
// }