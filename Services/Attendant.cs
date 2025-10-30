namespace SmartKiwi.Services;

using SmartKiwi.Models;
public class Attendante
{
    private QueueController QueueController { get; set; }
    public Attendante(QueueController queueController)
    {
        QueueController = queueController;
    }

    public void CallNext()
    {
        var called = QueueController.WhosNext();
        if(called == null)
        {
            System.Console.WriteLine("Não háfila de espera");
            return;
        }
        Show(called);
    }


    public void Show(Client called)
    {

        Console.WriteLine($"Senha: {called.WaiteTicket} /n Nome: {called.Name}");

    }
}