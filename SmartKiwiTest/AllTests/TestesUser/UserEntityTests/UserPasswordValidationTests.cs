using Moq;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;

public class UserPasswordValidationTests
{
    [Fact]
    public void Deve_Validar_Senha_Da_Entidade()
    {
        var passworServiceMock = new Mock<IPasswordService>();
        var userPassword = "996699";
        var userHashedPassword = "OTk2Njk5";
        var informedPassword = userPassword;
        passworServiceMock.Setup(x => x.ValidatePassword(informedPassword,userHashedPassword)).Returns(true);
        var user = new User("Danilo", "da@hotmail.com", userHashedPassword);

        var expected = user.ValidatePassword(informedPassword,passworServiceMock.Object);
    
        Assert.True(expected);
    }

    [Fact]
    public void Deve_Não_Validar_Senha_Da_Entidade()
    {
        
        var userHashedPassword = "OTk2Njk5";
        var informedPassword = "wrongPasswoed";
        var passworServiceMock = new Mock<IPasswordService>();
        passworServiceMock.Setup(x => x.ValidatePassword(informedPassword,userHashedPassword)).Returns(false);
        
        var user = new User("Danilo", "da@hotmail.com", userHashedPassword);

        var expected = user.ValidatePassword(informedPassword,passworServiceMock.Object);
    
        Assert.False(expected);
    }

   
}
    