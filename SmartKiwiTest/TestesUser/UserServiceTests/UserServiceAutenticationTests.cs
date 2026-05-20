using Moq;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using SmartKiwiApp.Repository;
public class UserServiceAutenticationTests
{
    private readonly UserService _userService;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordService> _passwordServiceMock;

    public UserServiceAutenticationTests()
    {   
        _tokenServiceMock = new Mock<ITokenService>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _userService = new UserService( _userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object);
    }

    [Fact]
    public async Task Deve_Chamar_Metodo_GenerateToken()
    {
        var informedPassword = "C0rrectP@ssWord";
        var informedEmail = "danilo@hotmail.com";
        var userDummy = new CleintQueue("danilo","danilo@hotmail.com", "C0rrectP@ssWord");
        _tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<CleintQueue>())).Returns("MokcTokenTeste");
        _passwordServiceMock.Setup(x => x.ValidatePassword(informedPassword,It.IsAny<string>())).Returns(true);
        _userRepositoryMock.Setup(x => x.GetUserByEmail(informedEmail)).ReturnsAsync(userDummy);
        var expected = await _userService.AuthenticateUser(informedEmail, informedPassword);
        
        Assert.Equal("MokcTokenTeste",expected);
        _tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<CleintQueue>()), Times.Once);
    }

    [Fact]
    public async Task Deve_Não_Chamar_Metodo_GenerateToken_Com_Senha_Incorreta()
    {
        var informedPassword = "Wr0ngP@ssWord";
        var informedEmail = "danilo@hotmail.com";
        var userDummy = new CleintQueue("danilo","danilo@hotmail.com", "C0rrectP@ssWord");
        _tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<CleintQueue>())).Returns("MokcTokenTeste");
        _passwordServiceMock.Setup(x => x.ValidatePassword(informedPassword,It.IsAny<string>())).Returns(false);
        _userRepositoryMock.Setup(x => x.GetUserByEmail(informedEmail)).ReturnsAsync(userDummy);
        
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userService.AuthenticateUser(informedEmail, informedPassword));
        _tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<CleintQueue>()),Times.Never);
    }

    [Fact]
    public async Task Deve_Não_Chamar_Metodo_GenerateToken_Com_Email_Incorreta()
    {
        var informedPassword = "C0rrectP@ssWord";
        var informedEmail = "Wrong@Email.com";
        var userDummy = new CleintQueue("danilo","danilo@hotmail.com", "C0rrectP@ssWord");
        _tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<CleintQueue>())).Returns("MokcTokenTeste");
        //_passwordServiceMock.Setup(x => x.ValidatePassword(informedPassword,It.IsAny<string>())).Returns(false);
        _userRepositoryMock.Setup(x => x.GetUserByEmail(informedEmail)).ReturnsAsync((CleintQueue)null);
        
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _userService.AuthenticateUser(informedEmail, informedPassword));
        _tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<CleintQueue>()),Times.Never);
    }

}

  