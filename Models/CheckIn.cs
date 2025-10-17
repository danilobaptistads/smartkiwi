
namespace SmartKiwi.Models;
using SmartKiwi.Models.Queuef;
public class checkIn
{
    string nameQueue;
    
    private IEnumerable<Queue> QueueList;
    public checkIn(IEnumerable<Queue> queueList)
    {
        QueueList = queueList;
    }
    public void Exec()
    {
        Console.WriteLine("Digite seu nome: ");
        var name = Console.ReadLine();
        Console.WriteLine(@"Qual tipo de Atendimento? 
                            0: Comum
                            1: Prioritário"
                        );
        nameQueue = Console.ReadLine();
        var selectedQueue = GetQueue();
        var waitTicket = GenTickt(selectedQueue);
        var newclient = new Client(name, waitTicket);
        selectedQueue.Enqueue(newclient);

    }

    private Queue GetQueue()
    {
        foreach (var queue in QueueList)
        {
            if (queue.Name == nameQueue)
            {
                return queue;
            }
        }
        throw new InvalidOperationException($"Fila '{nameQueue}' não encontrada.");
    }
    private int GenTickt(Queue queue)
    {
          
        if (queue.IsEmpty() && queue.lastCall == null)
        {
            return 1;
        }
        
        var lastNodeTicket = queue.GetLastNode().WaiteTicket;
        return lastNodeTicket++;

    }
        
    
}