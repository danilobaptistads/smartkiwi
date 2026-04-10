using SmartKiwiApp.Services;
public class PasswordHashServiceTests
{
    private readonly PasswordHashService _passwordHashService;
    public PasswordHashServiceTests()
    {
        _passwordHashService = new PasswordHashService();
    }

    [Fact]
    public void Deve_Nao_Retornar_Um_Hash_Vazio()
    {
        var informedPassword = "123456";

        var result = _passwordHashService.HashPassword(informedPassword);
    
        Assert.NotNull(result);
    }

    [Fact]
    public void Deve_Retornar_Um_Hash_Diferente_Da_String_Informada()
    {
        var informedPassword = "123456";

        var result = _passwordHashService.HashPassword(informedPassword);
    
        Assert.NotEqual(informedPassword,result);
    }

    [Fact]
    public void Deve_Retornar_True_Ao_Validar_O_Hash()
    {
        var userPassword = "123456";
        var informedPassword = "123456";
        var userPasswordHashe = _passwordHashService.HashPassword(userPassword);
        
        var result = _passwordHashService.VerifyPassword(informedPassword, userPasswordHashe);
    
        Assert.True(result);
    }

    [Fact]
    public void Deve_Retornar_aalse_Ao_Validar_O_Hash()
    {
        var userPassword = "78910";
        var informedPassword = "123456";
        var userPasswordHashe = _passwordHashService.HashPassword(userPassword);
        
        var result = _passwordHashService.VerifyPassword(informedPassword, userPasswordHashe);
    
        Assert.False(result);
    }

}