namespace SmartKiwi.Services;

using System.Runtime.CompilerServices;
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
    private int MaxQueueListIndex;
    private int QueueListCurrenIndex;
    bool HassPrioritieMatch;
    int queueListCount;
    public QueueController(List<Queue> queueList, int maxWaite)
    {
        callCounter = 0;
        MaxWaite = maxWaite;
        MainQueueList = queueList;
        HassPrioritieMatch = false;
        MaxQueueListIndex = MainQueueList.Count;
        QueueListCurrenIndex = 0;
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
                queue.lastCall = currentTime;
            }
        }
        
        var queueListCount = 0;
        
        while (queueListCount <= currentQueueList.Count)
        {

            var currentQueue = currentQueueList[QueueListCurrenIndex];
            
            if (currentQueue.IsEmpty())
            {
                callCounter = 0;
                QueueListCurrenIndex = (QueueListCurrenIndex + 1) % currentQueueList.Count;
                queueListCount++;
                continue;
            }
            
            if (callCounter < currentQueue.currentPriority)
            {
                var client = currentQueue.Dequeue();
                ++callCounter;
                return client;
            }


            callCounter = 0;
            QueueListCurrenIndex = (QueueListCurrenIndex + 1) % currentQueueList.Count;
            queueListCount++;
        }
        
        return null;
    }
    


}