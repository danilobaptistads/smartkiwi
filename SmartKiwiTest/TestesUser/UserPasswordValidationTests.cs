using Moq;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;

public class UserPasswordValidationTests
{
    [Fact]
    public void Deve_Validar_Senha_Da_Entidade()
    {
        var hashServiceMock = new Mock<IPasswordHashService>();
        var password = "996699";
        var hashedPassword = "OTk2Njk5";
        hashServiceMock.Setup(x => x.VerifyPassword(password,hashedPassword)).Returns(true);
        
        var user = new User("Danilo", "da@hotmail.com", hashedPassword);

        var expected = user.ValidatePassword("996699",hashServiceMock.Object);
    
        Assert.True(expected);
    }
    
    [Theory]
    [InlineData ("","OTk2Njk5")]
    [InlineData ("123456","OTk2Njk5")]
    [InlineData ("123456","FormatoNaoBase64")]
    public void Deve_Não_Validar_Senha(string password, string hashedPassword)
    {
        var hashServiceMock = new Mock<IPasswordHashService>();
        hashServiceMock.Setup(x => x.VerifyPassword(password,hashedPassword)).Returns(false);
        
        var user = new User("Danilo", "da@hotmail.com", hashedPassword);

        var expected = user.ValidatePassword(password,hashServiceMock.Object);
    
        Assert.False(expected);
    }


}