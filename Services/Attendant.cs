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
        Console.Clear();
        var called = QueueController.AdvanceQueue();
        if(called == null)
        {
            Console.WriteLine("Não háfila de espera");
            return;
        }
        Show(called);
    }


    public void Show(Client called)
    {

        Console.WriteLine($"Senha: {called.WaiteTicket} \nNome: {called.Name}");

    }
}