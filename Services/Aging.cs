
namespace SmartKiwi.Services;
using SmartKiwi.Models;
public class Aging
{
    private int MaxWaite;
    private int MaxPriority;
    private List<Queue> MainQueueList;

    public Aging(List<Queue> mainQueueList, int maxWaite, int maxPriority)
    {
        MaxWaite = maxWaite;
        MaxPriority = maxPriority;
        MainQueueList = mainQueueList;
    }
    public void Exec(DateTime currentTime)
    {
        TimeSpan? timeElapsedFromLastCall;
        Console.WriteLine("Rodando Aging");
        foreach (var queue in MainQueueList)
        {
            if (queue.lastCall != null)
            {
                timeElapsedFromLastCall = currentTime - queue.lastCall;



                if (timeElapsedFromLastCall > TimeSpan.FromMinutes(MaxWaite))
                {
                    if (queue.currentPriority < MaxPriority)
                    {
                        Console.WriteLine("Fila envelheceu");
                        ++queue.currentPriority;

                        Console.WriteLine($"A prioridare de {queue.Name} é {queue.currentPriority}");
                        Console.ReadKey();
                    }
                }

            }
            System.Console.WriteLine($"{queue.Name} nunca foi chamada");
        }
    }

    public void ResetPriority()
    {
        Console.WriteLine("Resetando prio");
        for (var i = 1; i < MainQueueList.Count; i++)
        {
            MainQueueList[i].currentPriority = MainQueueList[i].Priority;

        }
    }
}