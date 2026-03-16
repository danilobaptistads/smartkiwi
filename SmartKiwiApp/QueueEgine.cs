using SmartKiwiApp.Models;

public class QueueEngine
{
    List<ClientQueue> queueList;
    int index;
    int callsCount;
    int sumOfPriotitys;
    ClientQueue lastProcessedQueue;
    public QueueEngine()
    {
        queueList = new List<ClientQueue>();
        callsCount = 0;

    }
    public void AddQueue(ClientQueue newQueue)
    {
        queueList.Add(newQueue);
        sumOfPriotitys = SumOfPrioritys();
        
    }
    public Client ProcessQueue()
    {
        Client client;
        if(callsCount >= sumOfPriotitys)
        {
            
            resetCurrentPrioritys();
            callsCount = 0;

        }
        var activeQueues = ActiveQueuesTotal();
        foreach(ClientQueue queue in queueList)
        {
            
            if( !queue.IsEmpty() && queue.currentPriority > 0 && (queue != lastProcessedQueue || activeQueues == 1))
            {

                client = queue.Dequeue();
                callsCount++;
                queue.currentPriority--;
                lastProcessedQueue = queue;
                return client;
                
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
}