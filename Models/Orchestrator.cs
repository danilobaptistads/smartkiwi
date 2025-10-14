namespace SmartKiwi.Models;

public class Orchestrator
{
    private IEnumerable<Queue> QueueList { get; set; }
    public Orchestrator(IEnumerable<Queue> queueList)
    {
        QueueList = queueList;
    }

    public void InitWaiteQueue()
    {

        
        var running = true;

        while (running)
        {

            //checar se tem fila vazia
            if (Console.KeyAvailable)
            {
                running = false;
            }


        }
    }
    


}