using System.Collections;
namespace SmartKiwi.Controller;

public class InitialUi
{

    public void WelcomeMessage()
    {

        Console.Clear();
        Console.WriteLine("SmartKiwi\n BEM VINDO!");
        Thread.Sleep(2500);
        int maxWaite = 0;

    }

    public void NotAnyQueueCreated()
    {
        Console.WriteLine("\nNenhuma fila criada, Crie a primeira.");
        Thread.Sleep(2000);
    }
    public  int GetMaxQueueWaite()
    {
        var mensage = "Digite o tempo maximo de esperade uma fila";
        Console.WriteLine(mensage);
        int maxWaite;
        while (!int.TryParse(Console.ReadLine(), out maxWaite))
        {
            Console.WriteLine("Entrada Inválida");
            Console.WriteLine(mensage);

        }
    
        return maxWaite;
    }


            
}