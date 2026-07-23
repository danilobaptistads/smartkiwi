// using Moq;
// using SmartKiwiApp.Data;
// using SmartKiwiApp.Models;
// using SmartKiwiApp.Services;
// using SmartKiwiApp.Repository;
// public class UserServiceUpdateTests
// {
//     private readonly Mock<ITokenService> _tokenServiceMock;
//     private readonly Mock<IUserRepository> _userRepositoryMock;
//     private readonly Mock<IPasswordService> _passwordServiceMock;
//     private readonly UserService _userService;
//     public UserServiceUpdateTests()
//     {
//         _tokenServiceMock = new Mock<ITokenService>();
//         _userRepositoryMock = new Mock<IUserRepository>();
//         _passwordServiceMock = new Mock<IPasswordService>();
//         _userService = new UserService( _userRepositoryMock.Object, _passwordServiceMock.Object, _tokenServiceMock.Object);
//     }

//     [Fact]
//     public async Task Deve_Chamar_Metodo_UpdateName()
//     {
//         var currentUSer = new User("Danilo", "da@hotmail.com", "!T12@45");
//         var IdCurrentUser = currentUSer.Id;
//         _userRepositoryMock.Setup(x => x.GetUserById(IdCurrentUser)).ReturnsAsync(currentUSer);
//         _userRepositoryMock.Setup(x => x.UpdateName(It.IsAny<User>(),"Dan")).Returns(Task.CompletedTask);

//         await _userService.UpdateUserName(IdCurrentUser,"Dan");
        
//          _userRepositoryMock.Verify(x => x.UpdateName(It.IsAny<User>(),"Dan"), Times.Once);
        
//     }
//     [Fact]
//     public async Task Deve_Não_Chamar_Metodo_UpdateName_Se_Usuario_Não_Encontrado()
//     {
//         var currentUSer = new User("Danilo", "da@hotmail.com", "!T12@45");
//         var wrongId = Guid.NewGuid();
//         _userRepositoryMock.Setup(x => x.GetUserById(wrongId)).ReturnsAsync((User?)null);
 
//         await Assert.ThrowsAsync<ArgumentException>(() => _userService.UpdateUserName(wrongId,"Dan"));
        
//         _userRepositoryMock.Verify(x => x.UpdateName(It.IsAny<User>(),"Dan"), Times.Never);
        
//     }

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
// }