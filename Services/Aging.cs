
namespace SmartKiwi.Services;

using System.Net.WebSockets;
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
        Console.WriteLine("Rodando Aging");
        foreach (var queue in MainQueueList)
        {
            var timeElapsedFromLastCall = currentTime - queue.lastCall;

            System.Console.WriteLine(timeElapsedFromLastCall);
            if (timeElapsedFromLastCall >= TimeSpan.FromMinutes(MaxWaite))
            {
                if (queue.currentPriority < MaxPriority)
                {
                    Console.WriteLine("Fila envelheceu");
                    ++queue.currentPriority;
                }
                else
                {
                    ResetPriority();
                }
            }

        }
    }

    public void ResetPriority()
    {
        Console.WriteLine("Resetando prio");
        for (var i = 1; i < MainQueueList.Count; i++)
        {
            MainQueueList[i].ResetPriorit();

        }
    }
}