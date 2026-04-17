using Moq;
using SmartKiwiApp.Data;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
using SmartKiwiApp.Repository;
public class UserServiceTests
{
    [Fact]
    public async Task Deve_Criar_Um_Usuario()
    {
        var userService = new UserService();
        var UserRepositoryMock = new Mock<IUserRepository>();
        UserRepositoryMock.Setup(x => x.Add(It.IsAny<User>())).Returns(Task.CompletedTask);
        
        await userService.CreateNewUSer("danilo", "dan@hotmail.com","996699");

        UserRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
    }

}