using SmartKiwiApp.Dto;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
public class UserServiceCreateTests
{

    [Fact]
    public async Task Deve_Criar_um_Novo_usuario()
    {
        var (connection, provider) = CreateTestEnvironment();
        using(connection)
        using(provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            var result = await userService.CreateNewUSer(new CreateUserRequest
            (
                "danilo",
                "dan@hotmail.com",
                "Q4t3$t26",
                UserRole.Admin
            ));    
            Assert.True(result.Succeeded);
        }

    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    public async Task Deve_Não_Criar_Usuario_Com_Nome_Vazio(string nomeVazioOuEspaços)
    {
        var (connection, provider) = CreateTestEnvironment();
        using(connection)
        using(provider)
        {
        
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            var result = await userService.CreateNewUSer(new CreateUserRequest
            (
                nomeVazioOuEspaços,
                "dan@hotmail.com",
                "I2&b52016",
                UserRole.Admin
            ));    
            Assert.False(result.Succeeded);
            Assert.NotEmpty(result.Errors);
        }
    }

    [Theory]
    [InlineData("")]
    [InlineData("EmailInvalido.com")]
    public async Task Deve_Não_Criar_Usuario_Com_Email_Invalido_Ou_Vazio(string emailInvalidoOuVazio)
    {
        var (connection, provider) = CreateTestEnvironment();
        using(connection)
        using(provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            var result = await userService.CreateNewUSer(new CreateUserRequest
            (
                "danilo",
                emailInvalidoOuVazio,
                "V@lid&Passw0rd",
                UserRole.Admin
            ));    
            Assert.False(result.Succeeded);
            Assert.NotEmpty(result.Errors);
        }

    }

    
    [Fact]
    public async Task Deve_Não_Criar_Usuario_Com_Email_Ja_Cadastrado()
    {
        var (connection, provider) = CreateTestEnvironment();
        using(connection)
        using(provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            await userService.CreateNewUSer(new CreateUserRequest (
                "Davidson",
                "dan@hotmail.com",
                "V@lid&Passw0rd",
                UserRole.Admin
            ));
            var firstresult = await userManager.FindByEmailAsync("dan@hotmail.com");
            Assert.NotNull(firstresult);
            var result = await userService.CreateNewUSer(new CreateUserRequest
            (
                "danilo",
                "dan@hotmail.com",
                "V@lid&Passw0rd",
                UserRole.Admin
            ));    
            Assert.False(result.Succeeded);
            Assert.NotEmpty(result.Errors);
        }

    }

    [Fact]
    public async Task Deve_Não_Criar_Usuario_Com_Senha_Vazia()
    {
        var senhaVazia = "";
        var (connection, provider) = CreateTestEnvironment();
        using(connection)
        using(provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            var result = await userService.CreateNewUSer(new CreateUserRequest
            (
                "danilo",
                "dan@hotmail.com",
                senhaVazia,
                UserRole.Admin
            ));    
   
            Assert.Contains(result.Errors, e => e.Code == "PasswordTooShort");
        }

    }
    [Theory]
    [InlineData("Ab1!", "PasswordTooShort")]
    [InlineData("Abcdehg!", "PasswordRequiresDigit")]
    [InlineData("abcdef1!", "PasswordRequiresUpper")]
    [InlineData("ABCDEF1!", "PasswordRequiresLower")]
    [InlineData("Abcdef12", "PasswordRequiresNonAlphanumeric")]
    public async Task Deve_Não_Criar_Usuario_Com_Senha_Invalida(string invalidPassword, string expectedError)
    {
        var (connection, provider) = CreateTestEnvironment();
        using(connection)
        using(provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            var result = await userService.CreateNewUSer(new CreateUserRequest
            (
                "danilo",
                "dan@hotmail.com",
                invalidPassword,
                UserRole.Admin
            ));    
   
            Assert.Contains(result.Errors, e => e.Code == expectedError);
        }

    }

     private (SqliteConnection, ServiceProvider) CreateTestEnvironment()
        {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var services = new ServiceCollection();

        services.AddDbContext<SmartKiwiContext>(options =>
        {
            options.UseSqlite(connection);
        });

        services
            .AddIdentityCore<User>(options => 
            {
                options.User.RequireUniqueEmail =true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase =true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                
            })
            .AddUserValidator<EmailValidator>()
            .AddUserValidator<CustomUserValidator>()
            .AddEntityFrameworkStores<SmartKiwiContext>();
            


        var provider = services.BuildServiceProvider();


        using (var scope = provider.CreateScope())
        {
            var context = scope.ServiceProvider
                .GetRequiredService<SmartKiwiContext>();

            context.Database.EnsureCreated();
        }

        return (connection, provider);
    }
}