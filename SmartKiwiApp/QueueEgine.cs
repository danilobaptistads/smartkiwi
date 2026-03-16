using SmartKiwiApp.Models;

public class QueueEngine
{
    List<ClientQueue> queueList;
    int sucessfulProcessedQueues;
    int sumOfPriotitys;
    ClientQueue lastProcessedQueue;
    public QueueEngine()
    {
        queueList = new List<ClientQueue>();
        sucessfulProcessedQueues = 0;

    }
    public void AddQueue(ClientQueue newQueue)
    {
        queueList.Add(newQueue);
        sumOfPriotitys = SumOfPrioritys();
        
    }
    public ClientQueue ProcessQueue()
    {

        if(sucessfulProcessedQueues >= sumOfPriotitys)
        {

            resetCurrentPrioritys();
            sucessfulProcessedQueues = 0;

        }
        var activeQueues = ActiveQueuesTotal();
        foreach(ClientQueue queue in queueList)
        {
            
            if( IsQualificadQueue(queue, activeQueues))
            {

                UpdatesToNextProcess(queue);
                return queue;
                
            }
            

        }
        
        return null;

    }
    internal int SumOfPrioritys()
    {
        var sum = 0;
        foreach(ClientQueue queue in queueList)
        {
            sum += queue.Priority;
        }
        return sum;
    }
    internal void resetCurrentPrioritys()
    {
        foreach(ClientQueue queue in queueList)
        {
            queue.currentPriority = queue.Priority;
        }
    }
    internal int ActiveQueuesTotal()
    {   
        var total =0;
        foreach(var queue in queueList)
        {
            if(!queue.IsEmpty() && queue.currentPriority > 0)
            {
                total++;
            }
            
        }
        return total;
    }
    internal bool IsQualificadQueue(ClientQueue queue, int activeQueues)
    {
        if( !queue.IsEmpty() && queue.currentPriority > 0 && (queue != lastProcessedQueue || activeQueues == 1))
        {
            return true;
        }
        return false;
    }
    internal void UpdatesToNextProcess(ClientQueue queue)
    {
        sucessfulProcessedQueues++;
        queue.currentPriority--;
        lastProcessedQueue = queue;
    }
}