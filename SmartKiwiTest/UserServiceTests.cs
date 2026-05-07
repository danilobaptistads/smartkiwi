using Moq;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using SmartKiwiApp.Repository;
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;
    private readonly Mock<ITokenService> _tokenServiceMock;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _tokenServiceMock = new Mock<ITokenService>();
    }
    [Fact]
    public async Task Deve_Adicionar_Usuario_Via_Repository()
    {
        var userRawPassword = "996699";
        var userHashedPassword = "OTk2Njk5";
        var userService = new UserService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object);
        
        _userRepositoryMock.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.CompletedTask);
        _userRepositoryMock.Setup(x => x.GetUserByEmail("dan@hotmail.com")).ReturnsAsync((User)null);
        _passwordServiceMock.Setup(x => x.ProcssesHashNewPassword(userRawPassword)).Returns(userHashedPassword);
        
         await userService.CreateNewUSer("danilo", "dan@hotmail.com","996699");
        
        _passwordServiceMock.Verify(x => x.ProcssesHashNewPassword(userRawPassword), Times.Once);
        _userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        _userRepositoryMock.Verify(x => x.GetUserByEmail("dan@hotmail.com"), Times.Once);
    
    }

    [Fact]
    public async Task Deve_Não_Adicionar_Usuario_Com_Email_Já_Registrado()
    {      
        var NewUserRawPassword = "996699";
        var NewUserHashedPAssword = "OTk2Njk5";
        var AlreadyRegisteredEmail = "dan@hotmail.com";
        var userDummy = new User("dummy", "dan@hotmail.com", "anyhash");
        var userService = new UserService(_userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object);

        _userRepositoryMock.Setup(x => x.GetUserByEmail(AlreadyRegisteredEmail)).ReturnsAsync(userDummy);
        
        var assertException = await Assert.ThrowsAsync<InvalidOperationException>(()=> userService.CreateNewUSer("danilo", AlreadyRegisteredEmail,"996699"));
        Assert.Equal("Não foi possível realizar o cadastro", assertException.Message);

        _userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
    }
    
}