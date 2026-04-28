using Moq;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using SmartKiwiApp.Repository;
public class UserSercviceUpdeteTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;
    public UserSercviceUpdeteTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordServiceMock = new Mock<IPasswordService>();
    }

    [Fact]
    public async Task Deve_Chamar_Metodo_UpdateName()
    {
        var currentUSer = new User("Danilo", "da@hotmail.com", "!T12@45");
        var IdCurrentUser = currentUSer.Id;
        var userService = new UserService( _userRepositoryMock.Object, _passwordServiceMock.Object);

        _userRepositoryMock.Setup(x => x.GetUserById(IdCurrentUser)).ReturnsAsync(currentUSer);
        _userRepositoryMock.Setup(x => x.UpdateName(It.IsAny<User>(),"Dan")).Returns(Task.CompletedTask);

        await userService.UpdateUserName(IdCurrentUser,"Dan");
        
         _userRepositoryMock.Verify(x => x.UpdateName(It.IsAny<User>(),"Dan"), Times.Once);
        
    }
    [Fact]
    public async Task Deve_Não_Chamar_Metodo_UpdateName_Se_Usuario_Não_Encontrado()
    {
        var currentUSer = new User("Danilo", "da@hotmail.com", "!T12@45");
        var wrongId = Guid.NewGuid();
        var userService = new UserService( _userRepositoryMock.Object, _passwordServiceMock.Object);

        _userRepositoryMock.Setup(x => x.GetUserById(wrongId)).ReturnsAsync((User)null);
        //_userRepositoryMock.Setup(x => x.UpdateName(It.IsAny<User>(),"Dan")).Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateUserName(wrongId,"Dan"));
        
         _userRepositoryMock.Verify(x => x.UpdateName(It.IsAny<User>(),"Dan"), Times.Never);
        
    }

    [Fact]
    public async Task Deve_Chamar_Metodo_UpdateEmail_Se_Senha_válida()
    {
        var validUserPassword = "!T12@45";
        var currentUSer = new User("Danilo", "da@hotmail.com", validUserPassword );
        var IdCurrentUser = currentUSer.Id;
        var userService = new UserService( _userRepositoryMock.Object, _passwordServiceMock.Object);

        _passwordServiceMock.Setup(x => x.ValidatePassword(validUserPassword,It.IsAny<string>())).Returns(true);
        _userRepositoryMock.Setup(x => x.GetUserById(IdCurrentUser)).ReturnsAsync(currentUSer);
        _userRepositoryMock.Setup(x => x.UpdateEmail(It.IsAny<User>(),"danilo@gmail.com")).Returns(Task.CompletedTask);

        await userService.UpdateUserEmail(IdCurrentUser,validUserPassword,"danilo@gmail.com");
        
         _userRepositoryMock.Verify(x => x.UpdateEmail(It.IsAny<User>(),"danilo@gmail.com"), Times.Once);
        
    }

    [Fact]
    public async Task Deve_Não_Chamar_Metodo_UpdateEmail_Se_Senha_válida_Errada()
    {
        var validUserPassword = "!T12@45";
        var wrongUserPassword = "@daSilva123";
        var currentUSer = new User("Danilo", "da@hotmail.com", validUserPassword );
        var IdCurrentUser = currentUSer.Id;
        var userService = new UserService( _userRepositoryMock.Object, _passwordServiceMock.Object);

        _passwordServiceMock.Setup(x => x.ValidatePassword(wrongUserPassword,It.IsAny<string>())).Returns(false);
        _userRepositoryMock.Setup(x => x.GetUserById(IdCurrentUser)).ReturnsAsync(currentUSer);
        _userRepositoryMock.Setup(x => x.UpdateEmail(It.IsAny<User>(),"danilo@gmail.com")).Returns(Task.CompletedTask);

        await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateUserEmail(IdCurrentUser,wrongUserPassword,"danilo@gmail.com"));
        
         _userRepositoryMock.Verify(x => x.UpdateEmail(It.IsAny<User>(),"danilo@gmail.com"), Times.Never);
        
    }
}