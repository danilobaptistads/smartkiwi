namespace SmartKiwi.Models;

using SmartKiwi.Models.Queuef;
public class QueueController
{
    private List<Queue> MainQueueList;
    private List<Queue> DynamicQueueList;
    private int callCounter;
    private int MaxWaite;
    private DateTime? lastAgingCheck;
    private int MaxPriority;
    private List<Queue> currentQueueList;
    bool PrioritiesMatch;
    public QueueController(List<Queue> queueList, int maxWaite)
    {
        callCounter = 0;
        MaxWaite = maxWaite;
        MainQueueList = queueList;
        lastAgingCheck = null;
        DynamicQueueList = new List<Queue>();
        MaxPriority = MainQueueList[0].curretPriority;
        PrioritiesMatch = false;


    }
    public Client WhosNext()
    {
       
        var currentTime = DateTime.Now;
        var newCicle = CheckCycle(MaxWaite, currentTime);

        if (newCicle)
        {
            Aging(MaxWaite, currentTime);
            PrioritiesMatch = CheckPrioritiesMatch(PrioritiesMatch);

        }

        if (PrioritiesMatch == false)
        {
            currentQueueList = MainQueueList;

        }
        else
        {
            currentQueueList = DynamicQueueList;
        }

        return SearchNext(currentQueueList, currentTime);
        

    }
    private Client SearchNext(List<Queue> currentQueueList, DateTime currentTime)
    {
        foreach (var queue in currentQueueList)
        {
            if (queue.lastCall == null && !queue.IsEmpty())
            {
                System.Console.WriteLine("primeira chamada");
                callCounter++;
                queue.lastCall = currentTime;
                return queue.Dequeue();
            }
            if (!queue.IsEmpty() && callCounter <= queue.curretPriority)
            {
                callCounter++;
                queue.lastCall = currentTime;
                System.Console.WriteLine(" chamada normal");
                return queue.Dequeue();
                
            }
            else
            {
                System.Console.WriteLine("afila zerou");
                callCounter = 0;
                continue;
            }
        }
        System.Console.WriteLine("passou todas as condições");
        return null;        
    }
    private void Aging(int maxAge, DateTime currentTime)
    {

        foreach (var queue in MainQueueList)
            {
                var timeElapsed = currentTime - queue.lastCall;

                if (timeElapsed >= TimeSpan.FromMinutes(maxAge) && queue.curretPriority < MaxPriority)
                {
                        queue.curretPriority++;
                }
                    
            }
    }
    private bool CheckPrioritiesMatch(bool PrioritiesMatch)
    {
        if (PrioritiesMatch != true)
        {

            for (var i = 1; i < (MainQueueList.Count + 1); i++)
            {
                if (MainQueueList[i].IsEmpty() && DynamicQueueList.Contains(MainQueueList[i]))
                {
                    DynamicQueueList.Remove(MainQueueList[i]);
                    continue;
                }

                if (MainQueueList[0].curretPriority == MainQueueList[i].curretPriority)
                {
                    DynamicQueueList.Add(MainQueueList[i]);
                    PrioritiesMatch = true;
                }
            }
        }
        else
        {
            DynamicQueueList.Clear();
            PrioritiesMatch = false;
        }
        return PrioritiesMatch;
    }
    private bool CheckCycle(int maxWaite, DateTime currentTime)
    {

        if (lastAgingCheck != null)
        {
            var TimeElapsed = currentTime - lastAgingCheck;

            if (TimeElapsed >= TimeSpan.FromMinutes(maxWaite))
            {
                lastAgingCheck = currentTime;
                return true;
            }
        }
        
        lastAgingCheck = currentTime;
        return false;

    }
    
}