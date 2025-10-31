
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

        foreach (var queue in MainQueueList)
        {
            var timeElapsed = currentTime - queue.lastCall;

            if (timeElapsed >= TimeSpan.FromMinutes(MaxWaite) && queue.curretPriority < MaxPriority)
            {
                Console.WriteLine("Fila envelheceu");
                ++queue.curretPriority;
            }

        }
    }
}