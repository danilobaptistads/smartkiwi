using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using SmartKiwiApp.Repository;
using Microsoft.EntityFrameworkCore;



namespace SmartKiwiTest;
public class UserRepositoryTests
{
   
    private SmartKiwiContextInMemory ContextBuilder(string databaseName)
    {
        var options = new DbContextOptionsBuilder<SmartKiwiContextInMemory>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;
        return new SmartKiwiContextInMemory(options);
    }

    [Fact]
    public async Task Deve_Adicionar_Usuario_Ao_Banco()
    {
        using var context = ContextBuilder("AddUserDb");
        var userRepository = new UserRepository(context);
        var user = new User("Danilo", "da@hotmail.com", "996699");

        await userRepository.Add(user);
        var users = await context.Users.ToListAsync();

        Assert.Equivalent(user, users[0]);


    }
    [Fact]
    public async Task Deve_Retornar_Erro_Se_Existir_Email_Igual_No_Db()
    {
        using var context = ContextBuilder("AddExistetUserEmailDb");
        var userRepository = new UserRepository(context);
        var user1 = new User("Danilo", "da@hotmail.com", "996699");
        var user2 = new User("Daniel", "da@hotmail.com", "137713");
        await userRepository.Add(user1);

        await Assert.ThrowsAsync<InvalidOperationException>(
        () => userRepository.Add(user2)
        );

    }

    [Fact]
    public async Task Deve_Buscar_Usuario_Pelo_Email()
    {
        using var context = ContextBuilder("GetUserByEmailDb");
        var userRepository = new UserRepository(context);
        var user = new User("Danilo", "da@hotmail.com", "996699");
        await userRepository.Add(user);
        
        var retornedUser = await userRepository.GetUserByEmail("da@hotmail.com");

        Assert.Equal(user.Email, retornedUser.Email );

    }

    [Fact]
    public async Task Deve_Buscar_Usuario_Pelo_ID()
    {
        using var context = ContextBuilder("GetUserByIdDb");
        var userRepository = new UserRepository(context);
        var user = new User("Danilo", "da@hotmail.com", "996699");
        var currentUserId = user.Id;
        await userRepository.Add(user);

        var retornedUser = await userRepository.GetUserById(currentUserId);

        Assert.Equivalent(user, retornedUser);

    }

    [Fact]
    public async Task Deve_Alterar_Email_do_Usuario_No_Banco()
    {
        using var context = ContextBuilder("EditUSerEmailDb");
        var userRepository = new UserRepository(context);
        var user = new User("Danilo", "da@hotmail.com", "996699");
        var currentUserId = user.Id;
        await userRepository.Add(user);
        
        await userRepository.UpdateEmail(currentUserId, "danilo@hotmail");
        
        var updatedUser = await userRepository.GetUserById(currentUserId);      

        Assert.Equal("danilo@hotmail", updatedUser.Email);

    }
    [Fact]
    public async Task Deve_Alterar_Nome_do_Usuario_No_Banco()
    {
        using var context = ContextBuilder("EditUSerNameDb");
        var userRepository = new UserRepository(context);
        var user = new User("Danilo", "da@hotmail.com", "996699");
        var currentUserId = user.Id;
        await userRepository.Add(user);
        
        await userRepository.UpdateName(currentUserId, "Otto");
        
        var updatedUser = await userRepository.GetUserById(currentUserId);      

        Assert.Equal("Otto", updatedUser.Name);

    }
    
    [Fact]
    public async Task Deve_Alterar_Senha_No_Banco()
    {
        using var context = ContextBuilder("EditUSerPasswordeDb");
        var userRepository = new UserRepository(context);
        var hasherService = new PasswordHashService();
        var userPasswordHash = hasherService.HashPassword("996699");
        var user = new User("Danilo", "da@hotmail.com", userPasswordHash);
        var currentUserId = user.Id;
        await userRepository.Add(user);
        var userToUpdate = await userRepository.GetUserById(currentUserId);
 
        await userRepository.UpdatePassword( currentUserId,"196633", "996699", hasherService);
        var updatedUser = await userRepository.GetUserById(currentUserId);
        var validatedNewPassword = updatedUser.ValidatePassword("196633",hasherService);
        var validatedOldPassword = updatedUser.ValidatePassword("996699",hasherService);


        Assert.True(validatedNewPassword);
        Assert.False(validatedOldPassword);
        
    }
}