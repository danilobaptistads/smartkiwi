using Microsoft.EntityFrameworkCore;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Repository;

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
        userRepository.Add(user);
        
        var retornedUser = await userRepository.GetUserByEmail("da@hotmail.com");

        Assert.Equal(user.Email, retornedUser.Email );

    }

    }