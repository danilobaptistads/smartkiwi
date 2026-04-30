using Moq;
using SmartKiwiApp.Services;

public class PasswordServiceTests
{
    private readonly Mock<IHashService> _hashServiceMock ;
    private readonly PasswordService _passwordService;

    public PasswordServiceTests()
    {
        _hashServiceMock = new Mock<IHashService>();
        _passwordService = new PasswordService(_hashServiceMock.Object);
    }

    [Fact]
    public void Deve_Retornar_True_Para_Senha_Valida()
    {
        var rawPassword = "Teste@123";

        var result = _passwordService.ValidatePasswordFormat(rawPassword);

        Assert.True(result);
    }

    [Fact]
    public void Deve_Retornar_False_Para_Senha_Vazia()
    {
        var rawPassword = "";

        var result = _passwordService.ValidatePasswordFormat(rawPassword);

        Assert.False(result);
    }

    [Fact]
    public void Deve_Retornar_False_Para_Senha_Nula()
    {
        string? rawPassword = null;

        var result = _passwordService.ValidatePasswordFormat(rawPassword);

        Assert.False(result);
    }

    [Fact]
    public void Deve_Retornar_False_Para_Senha_Menor_Que_8_Caracteres()
    {
        var rawPassword = "I2E4s6";

        var result = _passwordService.ValidatePasswordFormat(rawPassword);

        Assert.False(result);
    }

    [Fact]
    public void Deve_Retornar_False_Para_Senha_Sem_Caractere_Especial()
    {
        var rawPassword = "Teste1234";

        var result = _passwordService.ValidatePasswordFormat(rawPassword);

        Assert.False(result);
    }

    [Fact]
    public void Deve_Retornar_False_Para_Senha_Sem_Letra_Maiuscula()
    {
        var rawPassword = "teste@123";

        var result = _passwordService.ValidatePasswordFormat(rawPassword);

        Assert.False(result);
    }

    [Fact]
    public void Deve_Retornar_Hash_Para_Senha_Valida()
    {
        var rawPassword = "Teste@123";
        _hashServiceMock.Setup(x => x.HashPassword(rawPassword)).Returns("hashed_password");

        var result = _passwordService.ProcssesHashNewPassword(rawPassword);

        Assert.NotNull(result);
        Assert.Equal("hashed_password", result);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Para_Senha_Invalida()
    {
        var rawPassword = "senhainvalida";
        
        var exception = Assert.Throws<ArgumentException>(() => 
            _passwordService.ProcssesHashNewPassword(rawPassword));

        Assert.Equal("Formato invalido", exception.Message);
    }

    [Fact]
    public void Deve_Retornar_True_Para_Senha_Valida_Com_Hash()
    {
        var informedPassword = "Teste@123";
        var hashedPassword = "hashed_value";

        _hashServiceMock.Setup(x => x.VerifyPassword(informedPassword, hashedPassword)).Returns(true);

        var result = _passwordService.ValidatePassword(informedPassword, hashedPassword);

        Assert.True(result);
    }

    [Fact]
    public void Deve_Retornar_False_Para_Senha_Invalida_Com_Hash()
    {
        var informedPassword = "SenhaErrada@123";
        var hashedPassword = "hashed_value";

        _hashServiceMock.Setup(x => x.VerifyPassword(informedPassword, hashedPassword)).Returns(false);

        var result = _passwordService.ValidatePassword(informedPassword, hashedPassword);

        Assert.False(result);
    }

}