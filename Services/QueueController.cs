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
    bool HasPrioritieMatch;
    public QueueController(List<Queue> queueList, int maxWaite)
    {
        callCounter = 0;
        MaxWaite = maxWaite;
        MainQueueList = queueList;
        //lastAgingCheck = null;
        HasPrioritieMatch = false;
        MaxPriority = MainQueueList[0].currentPriority;
        DynamicQueueList = new List<Queue>();
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

            HasPrioritieMatch = prioritiesMatcher.check(HasPrioritieMatch);

        }

        if (HasPrioritieMatch == false)
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
        foreach (var queue in currentQueueList)
        {
            if (queue.lastCall == null && !queue.IsEmpty())
            {
                Console.WriteLine("primeira chamada");
                callCounter++;
                queue.lastCall = currentTime;
                return queue.Dequeue();
            }
            if (!queue.IsEmpty() && callCounter <= queue.currentPriority)
            {
                callCounter++;
                queue.lastCall = currentTime;

                return queue.Dequeue();

            }
            else
            {
                Console.WriteLine("afila zerou");
                callCounter = 0;
                continue;
            }
        }
        Console.WriteLine("passou todas as condições");
        return null;
    }




}