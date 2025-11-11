
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
        foreach (var queue in MainQueueList)
        {
            if (queue.lastCall != null)
            {
                timeElapsedFromLastCall = currentTime - queue.lastCall;

                if (timeElapsedFromLastCall > TimeSpan.FromMinutes(MaxWaite))
                {
                    if (queue.currentPriority < MaxPriority)
                    {
                        ++queue.currentPriority;

                    }
                }

            }
        }
    }

    public void ResetPriority()
    {
        Console.WriteLine("Resetando prio");
        MainQueueList[0].currentPriority = MainQueueList[0].Priority;
        System.Console.WriteLine(MainQueueList[0].currentPriority);
        for (var i = 1; i < MainQueueList.Count; i++)
        {
            MainQueueList[i].currentPriority = MainQueueList[i].Priority;
            Console.WriteLine(MainQueueList[i].currentPriority);


        }
    }
}