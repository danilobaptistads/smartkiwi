using Moq;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using SmartKiwiApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace SmartKiwiTest;
public class UserRepositoryTests
{   
    private string oldPassword = "996699";
    private string newPassword = "196633";
    private string oldPasswordHash = "OTk2Njk5";
    private string newPasswordHash = "MTk2NjMz";

    private SmartKiwiContext ContextBuilder(string databaseName)
    {
        var options = new DbContextOptionsBuilder<SmartKiwiContext>()
            .UseInMemoryDatabase(databaseName: databaseName)
            .Options;
        return new SmartKiwiContext(options);
    }

    [Fact]
    public async Task Deve_Adicionar_Usuario_Ao_Banco()
    {
        using var context = ContextBuilder("AddUserDb");
        var userRepository = new UserRepository(context);
        
        var user = new User("Danilo", "da@hotmail.com", oldPasswordHash);

        await userRepository.Add(user);
        var users = await context.Users.ToListAsync();

        Assert.Equivalent(user, users[0]);

    }
    [Fact]
    public async Task Deve_Retornar_Erro_Se_Existir_Email_Igual_No_Db()
    {
        using var context = ContextBuilder("AddExistetUserEmailDb");
        var userRepository = new UserRepository(context);
        var user1 = new User("Danilo", "da@hotmail.com", oldPasswordHash);
        var user2 = new User("Daniel", "da@hotmail.com", oldPasswordHash);
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
        var user = new User("Danilo", "da@hotmail.com", oldPasswordHash);
        await userRepository.Add(user);

        var retornedUser = await userRepository.GetUserByEmail("da@hotmail.com");
        Assert.NotNull(retornedUser);
        Assert.Equal(user.Email, retornedUser.Email );

    }

    [Fact]
    public async Task Deve_Buscar_Usuario_Pelo_ID()
    {
        using var context = ContextBuilder("GetUserByIdDb");
        var userRepository = new UserRepository(context);
        var user = new User("Danilo", "da@hotmail.com", oldPasswordHash);
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
        var user = new User("Danilo", "da@hotmail.com", oldPasswordHash);
        await userRepository.Add(user);

        await userRepository.UpdateEmail(user, "danilo@hotmail.com");

        var updatedUser = await userRepository.GetUserById(user.Id);      
        Assert.NotNull(updatedUser);
        Assert.Equal("danilo@hotmail.com", updatedUser.Email);

    }
    [Fact]
    public async Task Deve_Alterar_Nome_do_Usuario_No_Banco()
    {
        using var context = ContextBuilder("EditUSerNameDb");
        var userRepository = new UserRepository(context);
        var user = new User("Danilo", "da@hotmail.com", oldPasswordHash);
        await userRepository.Add(user);

        await userRepository.UpdateName(user, "Otto");

        var updatedUser = await userRepository.GetUserById(user.Id);      
        
        Assert.NotNull(updatedUser);
        Assert.Equal("Otto", updatedUser.Name);

    }

    [Fact]
    public async Task Deve_Alterar_Senha_No_Banco()
    {
        var hasherServiceMock = new Mock<IPasswordService>();
        hasherServiceMock.Setup(x => x.ProcssesHashNewPassword(newPassword)).Returns(newPasswordHash);
        hasherServiceMock.Setup(x => x.ValidatePassword(oldPassword,oldPasswordHash)).Returns(true);
        hasherServiceMock.Setup(x => x.ValidatePassword(newPassword,oldPasswordHash)).Returns(false);
        hasherServiceMock.Setup(x => x.ValidatePassword(newPassword,newPasswordHash)).Returns(true);

        using var context = ContextBuilder("EditUSerPasswordeDb");
        var userRepository = new UserRepository(context);
        var user = new User("Danilo", "da@hotmail.com", oldPasswordHash);
        await userRepository.Add(user);

        await userRepository.UpdatePassword(user, newPassword, oldPassword, hasherServiceMock.Object);
        
        var updatedUser = await userRepository.GetUserById(user.Id);

        if(updatedUser != null)
        {
            var validatedNewPassword = updatedUser.ValidatePassword(newPassword,hasherServiceMock.Object);
            var validatedOldPassword = updatedUser.ValidatePassword(oldPassword,hasherServiceMock.Object);
            Assert.True(validatedNewPassword);
            Assert.False(validatedOldPassword);
        }
        

    }
}