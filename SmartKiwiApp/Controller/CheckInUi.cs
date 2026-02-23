namespace SmartKiwi.Controller;

using SmartKiwi.Models;
using SmartKiwi.Services;
public class checkInUi
{
    private List<Queue> QueueList;
    public checkInUi(List<Queue> queueList)
    {
        QueueList = queueList;
    }
    public void Exec()
    {
        Console.Clear();
        Console.WriteLine("Digite seu Nome");
        var clientName = Console.ReadLine();
        var mensage = "Que tipo de atendimento deseja";
        Console.WriteLine(mensage);
        showQueueNames();
        int numberOfQueue;
        while (!int.TryParse(Console.ReadLine(), out numberOfQueue))
        {
            Console.Clear();
            Console.WriteLine("entrada invalida");
            Console.WriteLine(mensage);
            showQueueNames();
        }
        Client newClient = null;
        while (newClient == null)
        {
            var selectedQueue = QueueList[numberOfQueue - 1];
            var checkIn = new checkIn();
            newClient = checkIn.Exec(clientName, selectedQueue);
            if (newClient != null)
            {
                Console.WriteLine($"{newClient.Name} sua senha é {newClient.WaiteTicket}");
   
                Thread.Sleep(1000);
                break;
            }
        }

    }

    public void showQueueNames()
    {
        var optionNumber = 0;
        foreach (var queue in QueueList)
        {
            optionNumber++;
            Console.WriteLine($"{optionNumber} - {queue.Name}");
        }
    }
}