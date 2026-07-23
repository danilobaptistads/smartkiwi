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
        var (connection, provider) = DbTestCongigure();
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

    [Fact]
    public async Task Deve_Não_Criar_Usuario_Com_Nome_Vazio()
    {
        var (connection, provider) = DbTestCongigure();
        using(connection)
        using(provider)
        {
            var nomeVazio = "";
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            var result = await userService.CreateNewUSer(new CreateUserRequest
            (
                nomeVazio,
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
    [InlineData("email@Invalido")]
    [InlineData("dan@hotmail.com")]
    public async Task Deve_Não_Criar_Usuario_Com_Email_Ja_Cadastrado_ou_Invalido(string email)
    {
        var (connection, provider) = DbTestCongigure();
        using(connection)
        using(provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            await userService.CreateNewUSer(new CreateUserRequest (
                "Davidson",
                "dan@hotmail.com",
                "i2&4@678",
                UserRole.Admin
            ));    
            var result = await userService.CreateNewUSer(new CreateUserRequest
            (
                "danilo",
                email,
                "Q4t3$t26",
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
        var (connection, provider) = DbTestCongigure();
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
    [InlineData("Abcdefg!", "PasswordRequiresDigit")]
    [InlineData("abcdef1!", "PasswordRequiresUpper")]
    [InlineData("ABCDEF1!", "PasswordRequiresLower")]
    [InlineData("Abcdef12", "PasswordRequiresNonAlphanumeric")]
    public async Task Deve_Não_Criar_Usuario_Com_Senha_Invalida(string invalidPassword, string expectedError)
    {
        var (connection, provider) = DbTestCongigure();
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

     private (SqliteConnection, ServiceProvider) DbTestCongigure()
        {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var services = new ServiceCollection();

        services.AddDbContext<SmartKiwiContext>(options =>
        {
            options.UseSqlite(connection);
        });

        services
            .AddIdentityCore<User>()
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