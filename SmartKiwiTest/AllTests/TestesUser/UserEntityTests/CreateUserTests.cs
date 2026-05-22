using System.Text;
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;

public class CreateUserTests
{        
    [Fact]
    public void Deve_Não_Criar_Usuário_Com_Nome_Vazio()
    {
        
        Action user = () => new CleintQueue("", "da@hotmail.com", "mWaZ");
    
        var assertException = Assert.Throws<ArgumentException>(user);
        Assert.Equal("Nome obrigatório.", assertException.Message);
    }
    
    [Fact]
    public void Deve_Não_Criar_Usuário_Com_Email_No_Formato_Invalido()
    {
             
        Action user = () => new CleintQueue("Danilo", "dahotmail.com", "mWaZ");
    
        var assertException = Assert.Throws<ArgumentException>(user);
        Assert.Equal("Email inválido.", assertException.Message);
    }

    [Fact]
    public void Deve_Não_Criar_Usuário_Com_Email_Vazio()
    {
               
        Action user = () => new CleintQueue("Danilo", "", "mWaZ" );
    
        var assertException = Assert.Throws<ArgumentException>(user);
        Assert.Equal("Email obrigatório.", assertException.Message);
    }

    


}