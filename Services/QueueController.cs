namespace SmartKiwi.Services;

using SmartKiwi.Models;
public class QueueController
{
    private List<Queue> MainQueueList;
    private List<Queue> DynamicQueueList;
    private List<Queue> currentQueueList;
    private int callCounter;
    private int MaxWaite;
    //private DateTime? lastAgingCheck;
    private int MaxPriority;
    private Aging aging;
    private PrioritiesMatcher prioritiesMatcher;
    private CycleChecker cycleChecker;
    bool HassPrioritieMatch;
    public QueueController(List<Queue> queueList, int maxWaite)
    {
        callCounter = 0;
        MaxWaite = maxWaite;
        MainQueueList = queueList;
        HassPrioritieMatch = false;
        MaxPriority = MainQueueList[0].Priority;
        DynamicQueueList = new List<Queue>() { MainQueueList[0] };
        cycleChecker = new CycleChecker(MaxWaite);
        aging = new Aging(MainQueueList, MaxWaite, MaxPriority);
        prioritiesMatcher = new PrioritiesMatcher(MainQueueList, DynamicQueueList);

    }
    public Client AdvanceQueue()
    {

        var currentTime = DateTime.Now;
        var newCycle = cycleChecker.exec();

        if (newCycle)
        {
            aging.Exec(currentTime);

            HassPrioritieMatch = prioritiesMatcher.check(HassPrioritieMatch);

            Console.WriteLine($"priotiesmatcher é : {HassPrioritieMatch}");

        }

        if (HassPrioritieMatch == false)
        {
            currentQueueList = MainQueueList;
            Console.WriteLine(" chamada Comum");
        }
        else
        {
            currentQueueList = DynamicQueueList;
            Console.WriteLine(" chamada Dinamica");
        }

        return GetClient(currentQueueList, currentTime);


    }
    private Client GetClient(List<Queue> currentQueueList, DateTime currentTime)
    {
        if (currentQueueList[0].lastCall == null)
        {
            foreach (var queue in currentQueueList)
            {
                queue.lastCall = DateTime.Now;
            }
        }
        foreach (var queue in currentQueueList)
        {

            if (!queue.IsEmpty())
            {
                if (callCounter <= queue.currentPriority)
                {
                    callCounter++;
                    queue.lastCall = currentTime;
                    return queue.Dequeue();
                }
                else
                {

                    callCounter = 0;
                    continue;
                }

            }
            

        }

        return null;
    }

}