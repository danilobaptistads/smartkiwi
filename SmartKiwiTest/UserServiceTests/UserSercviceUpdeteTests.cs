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
}