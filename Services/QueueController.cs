namespace SmartKiwi.Services;

using SmartKiwi.Models;
public class QueueController
{
    private List<Queue> MainQueueList;
    private int callCounter;
    private int MaxWaite;
    private int MaxPriority;
    private Aging aging;
    private PrioritiesMatcher prioritiesMatcher;
    private CycleChecker cycleChecker;
    private int QueueListCurrenIndex;
    bool HassPrioritieMatch;
    public QueueController(List<Queue> queueList, int maxWaite)
    {
        callCounter = 0;
        MaxWaite = maxWaite;
        MainQueueList = queueList;
        HassPrioritieMatch = false;
        QueueListCurrenIndex = 0;
        MaxPriority = MainQueueList[0].Priority;
        cycleChecker = new CycleChecker(MaxWaite);
        aging = new Aging(MainQueueList, MaxWaite, MaxPriority);
        prioritiesMatcher = new PrioritiesMatcher(MainQueueList);

    }
    public Client AdvanceQueue()
    {

        var currentTime = DateTime.Now;
        var newCycle = cycleChecker.exec();

        if (newCycle)
        {
            aging.Exec(currentTime);
            System.Console.WriteLine($"é um novo ciclo HassPrioritieMatch é {HassPrioritieMatch}");
            if(HassPrioritieMatch == true)
            {
                aging.ResetPriority();
                callCounter = 0;
                System.Console.WriteLine($"prioridades resetadas");
            }
            HassPrioritieMatch = prioritiesMatcher.check(HassPrioritieMatch);
            Console.WriteLine($" HassPrioritieMatch atual é {HassPrioritieMatch}");


        }

        if (HassPrioritieMatch == false)
        {
 
            Console.WriteLine(" chamada Comum");
        }
        else
        {
            Console.WriteLine(" chamada Dinamica");
        }

        return GetClient(MainQueueList, currentTime);


    }
    private Client GetClient(List<Queue> QueueList, DateTime currentTime)
    {
        if (MainQueueList[0].lastCall == null)
        {
            foreach (var queue in MainQueueList)
            {
                queue.lastCall = currentTime;
            }
        }
        
        var queueListCount = 0;
        
        while (queueListCount <= MainQueueList.Count)
        {

            var currentQueue = MainQueueList[QueueListCurrenIndex];
            
            if (!currentQueue.IsEmpty() && callCounter < currentQueue.currentPriority)
            {
                var client = currentQueue.Dequeue();
                ++callCounter;
                return client;
            }


            callCounter = 0;
            QueueListCurrenIndex = (QueueListCurrenIndex + 1) % MainQueueList.Count;
            queueListCount++;
        }
        
        return null;
    }
    


}