
namespace SmartKiwi.Services;

using SmartKiwi.Models;
public class checkIn
{
    public Client Exec(string clientName, Queue selectedQueue)
    {
        try
        {
            var waiteTicket = GenTickt(selectedQueue);
            var newClient = new Client(clientName, waiteTicket);
            selectedQueue.Enqueue(newClient);
            return newClient;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Não foi possivel realizar o checkin. \nErro {ex}");
            return null;   
        }
        
    }

    private int GenTickt(Queue selectedQueue)
    {

        if (selectedQueue.IsEmpty() && selectedQueue.lastCall == null)
        {
            return 1;
        }

        var lastNodeTicket = selectedQueue.GetLastNode().WaiteTicket;
        return lastNodeTicket++;

    }
        
    
}