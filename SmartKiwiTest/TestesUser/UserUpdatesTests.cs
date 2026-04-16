using Moq;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
public class UserUpdatesTests
{
    [Fact]
    public void Deve_Fazer_Update_do_Email()
    {
        
        var userPasswordHash = "OTk2Njk5";
        var user = new User("Danilo", "da@hotmail.com", userPasswordHash);
    
        user.UpdateEmail("danilo@hotmail.com");
    
        Assert.Equal("danilo@hotmail.com", user.Email);
    }

    [Theory]
    [InlineData("")]
    [InlineData("danHotmail.com")]
    public void Deve_Não_Fazer_Update_do_Email_Vazio_Ou_Formato_Incorreto(string newEmail)
    {
        var userPasswordHash = "OTk2Njk5";
        var user = new User("Danilo", "da@hotmail.com", userPasswordHash);
    
        Action updateEmail = () => user.UpdateEmail(newEmail);
    
        Assert.Throws<ArgumentException>(updateEmail);
    }

    [Fact]
    public void Deve_Fazer_Update_do_Nome()
    {
        var userPasswordHash = "OTk2Njk5";
        var user = new User("Danilo", "da@hotmail.com", userPasswordHash);
    
        user.UpdateName("Otto");
    
        Assert.Equal("Otto", user.Name);
    }

    [Fact]
    public void Deve_Não_Fazer_Update_do_Nome_Quando_Vazio()
    {
        var userPasswordHash = "OTk2Njk5";
        var user = new User("Danilo", "da@hotmail.com", userPasswordHash);
    
        Action updateName = () => user.UpdateName("");
    
        Assert.Throws<ArgumentException>(updateName);
    }

    [Fact]
    public void Deve_Alterar_Senha()
    {   var oldPassword = "996699";
        var newPassword = "123456"; 
        var oldHash = "OTk2Njk5";
        var newHash = "MTIzNDU2";
        var user = new User("Danilo", "da@hotmail.com", "OTk2Njk5");
        var hashServiceMock = new Mock<IPasswordHashService>();
        hashServiceMock.Setup(x => x.HashPassword(newPassword)).Returns(newHash);
        hashServiceMock.Setup(x => x.VerifyPassword(oldPassword, oldHash)).Returns(true);
        hashServiceMock.Setup(x => x.VerifyPassword(newPassword,newHash)).Returns(true);
        
        user.ChangePassword("123456","996699", hashServiceMock.Object);
        
        var expected = user.ValidatePassword("123456",hashServiceMock.Object);
    
        Assert.True(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("669966")]
    public void Deve_Não_Alterar_Senha(string informedPassword)
    {
        var Hash = "OTk2Njk5";
        var user = new User("Danilo", "da@hotmail.com", Hash);

        var hashServiceMock = new Mock<IPasswordHashService>();
        hashServiceMock.Setup(x => x.VerifyPassword(informedPassword,Hash)).Returns(false);

        Action expected = ()=> user.ChangePassword("123456",informedPassword, hashServiceMock.Object);
        Assert.Throws<ArgumentException>(expected);
    }
}

