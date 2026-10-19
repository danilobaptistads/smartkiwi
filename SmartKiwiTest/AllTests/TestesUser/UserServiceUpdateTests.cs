using SmartKiwiApp.Dto;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
public class UserServiceUpdateTests
{

    [Fact]
    public async Task Deve_Fazer_Update_Do_Nome()
    {
        var (connection, provider) = CreateTestEnvironment();
        using(connection)
        using(provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            await userService.CreateNewUSer(new CreateUserRequest
            (
                "danilo",
                "dan@hotmail.com",
                "Q4t3$t26",
                UserRole.Admin
            ));    
            var userToUpdate = await userManager.FindByEmailAsync("dan@hotmail.com");
            var dto = new UpdateNameRequest( userToUpdate!.Id.ToString(), "Dan" );
            await userService.UpdateUserName(dto);
            var result  = await userManager.FindByIdAsync(userToUpdate!.Id.ToString());
            Assert.Equal("Dan", result!.Name);
                   
        }
    }
    [Fact]
    public async Task Deve_Não_Chamar_Metodo_UpdateName_Se_Usuario_Não_Encontrado()
    {
        var (connection, provider) = CreateTestEnvironment();
        using(connection)
        using(provider)
        {
            var userManager = provider.GetRequiredService<UserManager<User>>();
            var userService = new UserService(userManager);
            var wrongId = Guid.NewGuid();
            var dto = new UpdateNameRequest( wrongId.ToString(), "Dan" );
            var result =  await userService.UpdateUserName(dto);
            Assert.False(result.Succeeded);
            Assert.Equal("UserNotFound", result.Errors.First().Code);

        }
    }

//     [Fact]
//     public async Task Deve_Chamar_Metodo_UpdateEmail_Se_Senha_válida()
//     {
//         var validUserPassword = "!T12@45";
//         var currentUSer = new User("Danilo", "da@hotmail.com", validUserPassword );
//         var IdCurrentUser = currentUSer.Id;

//         _passwordServiceMock.Setup(x => x.ValidatePassword(validUserPassword,It.IsAny<string>())).Returns(true);
//         _userRepositoryMock.Setup(x => x.GetUserById(IdCurrentUser)).ReturnsAsync(currentUSer);
//         _userRepositoryMock.Setup(x => x.UpdateEmail(It.IsAny<User>(),"danilo@gmail.com")).Returns(Task.CompletedTask);

//         await _userService.UpdateUserEmail(IdCurrentUser,validUserPassword,"danilo@gmail.com");
        
//          _userRepositoryMock.Verify(x => x.UpdateEmail(It.IsAny<User>(),"danilo@gmail.com"), Times.Once);
        
//     }

//     [Fact]
//     public async Task Deve_Não_Chamar_Metodo_UpdateEmail_Se_Senha_válida_Errada()
//     {
//         var validUserPassword = "!T12@45";
//         var wrongUserPassword = "@daSilva123";
//         var currentUSer = new User("Danilo", "da@hotmail.com", validUserPassword );
//         var IdCurrentUser = currentUSer.Id;

//         _passwordServiceMock.Setup(x => x.ValidatePassword(wrongUserPassword,It.IsAny<string>())).Returns(false);
//         _userRepositoryMock.Setup(x => x.GetUserById(IdCurrentUser)).ReturnsAsync(currentUSer);
//         _userRepositoryMock.Setup(x => x.UpdateEmail(It.IsAny<User>(),"danilo@gmail.com")).Returns(Task.CompletedTask);

//         await Assert.ThrowsAsync<ArgumentException>(() => _userService.UpdateUserEmail(IdCurrentUser,wrongUserPassword,"danilo@gmail.com"));
        
//          _userRepositoryMock.Verify(x => x.UpdateEmail(It.IsAny<User>(),"danilo@gmail.com"), Times.Never);
        
//     }

//     [Fact]
//     public async Task Deve_Chamar_Metodo_ChangeUserPassword_Quando_Senha_Correta()
//     {
//         var currentUser = new User("Danilo", "da@hotmail.com", "!T12@45");
//         var informedPassword = "!T12@45";
//         _userRepositoryMock.Setup(x => x.GetUserById(currentUser.Id)).ReturnsAsync(currentUser);
//         _userRepositoryMock.Setup(x => x.UpdatePassword(It.IsAny<User>(),"NewP@ssW0rd", informedPassword, _passwordServiceMock.Object)).Returns(Task.CompletedTask);
//         _passwordServiceMock.Setup(x => x.ValidatePassword(informedPassword,It.IsAny<string>())).Returns(true);

//         await _userService.UpdateUserPassword(currentUser.Id, "NewP@ssW0rd", informedPassword);
    
//         _userRepositoryMock.Verify(x => x.UpdatePassword(It.IsAny<User>(),"NewP@ssW0rd", informedPassword, _passwordServiceMock.Object), Times.Once);
//     }

//     [Fact]
//     public async Task Deve_Não_Chamar_Metodo_ChangeUserPassword_Quando_Senha_Incorreta()
//     {
//         var currentUser = new User("Danilo", "da@hotmail.com", "!T12@45");
//         var informedPassword = "wr0ngInfor3dP@ssword";
//         _userRepositoryMock.Setup(x => x.GetUserById(currentUser.Id)).ReturnsAsync(currentUser);
//         _userRepositoryMock.Setup(x => x.UpdatePassword(It.IsAny<User>(),"NewP@ssW0rd", informedPassword, _passwordServiceMock.Object)).Returns(Task.CompletedTask);
//         _passwordServiceMock.Setup(x => x.ValidatePassword(informedPassword,It.IsAny<string>())).Returns(false);

//          await Assert.ThrowsAsync<ArgumentException>(() => _userService.UpdateUserPassword(currentUser.Id, "NewP@ssW0rd", informedPassword));
    
//         _userRepositoryMock.Verify(x => x.UpdatePassword(It.IsAny<User>(),"NewP@ssW0rd", informedPassword, _passwordServiceMock.Object), Times.Never);
//     }

//     [Fact]
//     public async Task Deve_Chama_Metodo_Deletar_Usuario_Quando_Senha_Correta()
//     {
//         var currentUser = new User("Danilo", "da@hotmail.com", "infor3dP@sswordCorrect");
//         var informedPassword = "infor3dP@sswordCorrect";
//         _userRepositoryMock.Setup(x => x.GetUserById(currentUser.Id)).ReturnsAsync(currentUser);
//         _userRepositoryMock.Setup(x => x.DeleteUser(It.IsAny<User>())).Returns(Task.CompletedTask);
//         _passwordServiceMock.Setup(x => x.ValidatePassword(informedPassword,It.IsAny<string>())).Returns(true);
    
//         await _userService.DeleteCurrentUser(currentUser.Id, informedPassword);
    
//         _userRepositoryMock.Verify(x => x.DeleteUser(It.IsAny<User>()));
//     }

//     [Fact]
//     public async Task Deve_Não_Chamar_Metodo_DeleteUSer_Quando_Senha_Incorreta()
//     {
//         var currentUser = new User("Danilo", "da@hotmail.com", "!T12@45");
//         var informedPassword = "wr0ngInfor3dP@ssword";
//         _userRepositoryMock.Setup(x => x.GetUserById(currentUser.Id)).ReturnsAsync(currentUser);
//         _userRepositoryMock.Setup(x => x.DeleteUser(It.IsAny<User>())).Returns(Task.CompletedTask);
//         _passwordServiceMock.Setup(x => x.ValidatePassword(informedPassword,It.IsAny<string>())).Returns(false);

//          await Assert.ThrowsAsync<ArgumentException>(() => _userService.DeleteCurrentUser(currentUser.Id, informedPassword));

//         _userRepositoryMock.Verify(x => x.DeleteUser(It.IsAny<User>()), Times.Never);
//     }

//     [Fact]
//     public async Task Deve_Não_Chamar_Metodo_UpdateEmail_Se_Usuario_Não_Encontrado()
//     {
//         var wrongId = Guid.NewGuid();
//         var validUserPassword = "!T12@45";

//         _userRepositoryMock.Setup(x => x.GetUserById(wrongId)).ReturnsAsync((User?)null);

//         await Assert.ThrowsAsync<ArgumentException>(() => _userService.UpdateUserEmail(wrongId, validUserPassword, "novo@email.com"));

//         _userRepositoryMock.Verify(x => x.UpdateEmail(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
//     }

//     [Fact]
//     public async Task Deve_Não_Chamar_Metodo_UpdatePassword_Se_Usuario_Não_Encontrado()
//     {
//         var wrongId = Guid.NewGuid();
//         var informedPassword = "!T12@45";

//         _userRepositoryMock.Setup(x => x.GetUserById(wrongId)).ReturnsAsync((User?)null);

//         await Assert.ThrowsAsync<ArgumentException>(() => _userService.UpdateUserPassword(wrongId, "NewP@ssW0rd", informedPassword));

//         _userRepositoryMock.Verify(x => x.UpdatePassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IPasswordService>()), Times.Never);
//     }

//     [Fact]
//     public async Task Deve_Não_Chamar_Metodo_DeleteUser_Se_Usuario_Não_Encontrado()
//     {
//         var wrongId = Guid.NewGuid();
//         var informedPassword = "!T12@45";

//         _userRepositoryMock.Setup(x => x.GetUserById(wrongId)).ReturnsAsync((User?)null);

//         await Assert.ThrowsAsync<ArgumentException>(() => _userService.DeleteCurrentUser(wrongId, informedPassword));

//         _userRepositoryMock.Verify(x => x.DeleteUser(It.IsAny<User>()), Times.Never);
//     }


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

