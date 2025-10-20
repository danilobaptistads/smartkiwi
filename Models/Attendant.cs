namespace SmartKiwi.Models;
using SmartKiwi.Models.Queuef;
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
        Call(called);
    }


    public void Call(Client called)
    {

        Console.WriteLine($"Senha: {called.WaiteTicket} /n Nome: {called.Name}");

    }
}