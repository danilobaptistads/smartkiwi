using System.Text;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;

public class CreateUserTests
{
    [Fact]
    public void Deve_Criar_Usuário()
    {
        var password = "996699";
        var hashedPassword = Convert.ToBase64String( Encoding.UTF8.GetBytes(password));

        
        var user = new User("Danilo", "da@hotmail.com", hashedPassword);
        
        Assert.Equal("Danilo", user.Name);
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal("da@hotmail.com", user.Email);
    }
    
    
    [Fact]
    public void Deve_Não_Criar_Usuário_Com_Nome_Vazio()
    {
        
        var password = "996699";
        var hashedPassword = Convert.ToBase64String( Encoding.UTF8.GetBytes(password));
        
        Action user = () => new User("", "da@hotmail.com", hashedPassword);
    
        var assertException = Assert.Throws<ArgumentException>(user);
        Assert.Equal("Nome obrigatório.", assertException.Message);
    }
    
    [Fact]
    public void Deve_Não_Criar_Usuário_Com_Email_No_Formato_Invalido()
    {
        
        var password = "996699";
        var hashedPassword = Convert.ToBase64String( Encoding.UTF8.GetBytes(password));
        
        Action user = () => new User("Danilo", "dahotmail.com", hashedPassword);
    
        var assertException = Assert.Throws<ArgumentException>(user);
        Assert.Equal("Email inválido.", assertException.Message);
    }

    [Fact]
    public void Deve_Não_Criar_Usuário_Com_Email_Vazio()
    {
        
        var password = "996699";
        var hashedPassword = Convert.ToBase64String( Encoding.UTF8.GetBytes(password));
        
        Action user = () => new User("Danilo", "", hashedPassword);
    
        var assertException = Assert.Throws<ArgumentException>(user);
        Assert.Equal("Email obrigatório.", assertException.Message);
    }

    [Fact]
    public void Deve_Não_Criar_Usuário_Com_Senha_No_Formato_Invalido()
    {
        var invalidPassowrodFormat = "996699";
        
        Action user = () => new User("Danilo", "da@hotmail.com", invalidPassowrodFormat);
        
        var assertException = Assert.Throws<ArgumentException>(user);
        Assert.Equal("Fomato de senha inválido.", assertException.Message);

    }

    [Fact]
    public void Deve_Não_Criar_Usuário_Com_Senha_vazia()
    {
        var emptyPassowrod = "";
        
        Action user = () => new User("Danilo", "da@hotmail.com", emptyPassowrod);
        
        var assertException = Assert.Throws<ArgumentException>(user);
        Assert.Equal("Senha não pode ser vazia.", assertException.Message);

    }
    
    


}