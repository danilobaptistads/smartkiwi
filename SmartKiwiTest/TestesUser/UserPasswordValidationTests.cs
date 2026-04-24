using Moq;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;

public class UserPasswordValidationTests
{
    [Fact]
    public void Deve_Validar_Senha_Da_Entidade()
    {
        var hashServiceMock = new Mock<IHashService>();
        var userPassword = "996699";
        var userHashedPassword = "OTk2Njk5";
        var informedPassword = userPassword;
        hashServiceMock.Setup(x => x.VerifyPassword(informedPassword,userHashedPassword)).Returns(true);
        
        var user = new User("Danilo", "da@hotmail.com", userHashedPassword);

        var expected = user.ValidatePassword(informedPassword,hashServiceMock.Object);
    
        Assert.True(expected);
    }

    [Fact]
    public void Deve_Não_Validar_Senha_Da_Entidade()
    {
        var hashServiceMock = new Mock<IHashService>();
        var userPassword = "996699";
        var userHashedPassword = "OTk2Njk5";
        var informedPassword = "wrongPasswoed";
        hashServiceMock.Setup(x => x.VerifyPassword(informedPassword,userHashedPassword)).Returns(false);
        
        var user = new User("Danilo", "da@hotmail.com", userHashedPassword);

        var expected = user.ValidatePassword(informedPassword,hashServiceMock.Object);
    
        Assert.False(expected);
    }

   
}
    