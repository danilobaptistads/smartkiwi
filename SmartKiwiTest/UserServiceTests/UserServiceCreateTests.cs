using Moq;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using SmartKiwiApp.Repository;
public class UserServiceCreateTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;

    private readonly Mock<IPasswordService> _passwordServiceMock;
    public UserServiceCreateTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordServiceMock = new Mock<IPasswordService>();
    }
    [Fact]
    public async Task Deve_Chamar_Metodos_Add_E_GEtEmailById_E_ProcssesHashNewPassword()
    {
        var userRawPassword = "996699";
        var userHashedPassword = "OTk2Njk5";
        var userService = new UserService( _userRepositoryMock.Object, _passwordServiceMock.Object);
        
        _userRepositoryMock.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.CompletedTask);
        _userRepositoryMock.Setup(x => x.GetUserByEmail("dan@hotmail.com")).ReturnsAsync((User)null);
        _passwordServiceMock.Setup(x => x.ProcssesHashNewPassword(userRawPassword)).Returns(userHashedPassword);
        
         await userService.CreateNewUSer("danilo", "dan@hotmail.com","996699");
        
        _passwordServiceMock.Verify(x => x.ProcssesHashNewPassword(userRawPassword), Times.Once);
        _userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        _userRepositoryMock.Verify(x => x.GetUserByEmail("dan@hotmail.com"), Times.Once);
    
    }

    [Fact]
    public async Task Deve_Não_Chammar_Metodo_Add_Quando_Email_Já_Cadastrado()
    {      
        var NewUserRawPassword = "996699";
        var NewUserHashedPAssword = "OTk2Njk5";
        var AlreadyRegisteredEmail = "dan@hotmail.com";
        var userDummy = new User("dummy", "dan@hotmail.com", "anyhash");
        var userService = new UserService( _userRepositoryMock.Object, _passwordServiceMock.Object);

        _userRepositoryMock.Setup(x => x.GetUserByEmail(AlreadyRegisteredEmail)).ReturnsAsync(userDummy);
        
        var assertException = await Assert.ThrowsAsync<InvalidOperationException>(()=> userService.CreateNewUSer("danilo", AlreadyRegisteredEmail,"996699"));
        Assert.Equal("Não foi possível realizar o cadastro", assertException.Message);

        _userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never);
    }


}