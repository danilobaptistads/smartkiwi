using SmartKiwiApp.Models;

public class QueueEngine
{  
    int sumOfPriotitys;
    ClientQueue lastProcessedQueue;
    int successfullyProcessedQueues;
    List<ClientQueue> QueueList {get; set;}
    public int MaxWaiteMinutes { get; set; }
    public QueueEngine(int maxWaiteMinutes, List<ClientQueue> queueList)
    {
        QueueList = queueList;
        successfullyProcessedQueues = 0;
        MaxWaiteMinutes = maxWaiteMinutes;
        sumOfPriotitys = SumOfPrioritys();

    }
    public void AddQueue(ClientQueue newQueue)
    {
        QueueList.Add(newQueue);
        sumOfPriotitys = SumOfPrioritys();
        
    }
    
    public Client ProcessClient()
    {
        var currentQueue = SelectNextQueue();
        if(currentQueue != null && !currentQueue.IsEmpty())
        {
            var client = currentQueue.Dequeue();
            UpdateQueueForNextCall(currentQueue);
            return client;
        }
        
        return null;

    }
    public ClientQueue SelectNextQueue()
    {

        if(successfullyProcessedQueues >= sumOfPriotitys)
        {

            resetCurrentPrioritys();
            successfullyProcessedQueues = 0;

        }
        var activeQueues = ActiveQueuesTotal();
        var queueInTimeout = HasQueueInTimeout();

        if ( queueInTimeout != null)
        {
                return queueInTimeout;
        }

        foreach(ClientQueue queue in QueueList)
        {

            if( !queue.IsEmpty() && queue.currentPriority > 0 && (queue != lastProcessedQueue || activeQueues == 1))
            {
                return queue;
            }
        }
        return null;
    }
    internal int SumOfPrioritys()
    {
        var sum = 0;
        
        foreach(ClientQueue queue in QueueList)
        {
            sum += queue.Priority;
        }
        return sum;
    }
    internal void resetCurrentPrioritys()
    {
        foreach(ClientQueue queue in QueueList)
        {
            queue.currentPriority = queue.Priority;
        }
    }
    internal int ActiveQueuesTotal()
    {   
        var total =0;
        foreach(var queue in QueueList)
        {
            if(!queue.IsEmpty() && queue.currentPriority > 0)
            {
                total++;
            }
            
        }
        return total;
    }
    internal void UpdateQueueForNextCall(ClientQueue queue)
    {
        successfullyProcessedQueues++;
        queue.currentPriority--;
        lastProcessedQueue = queue;
        queue.lastCallTime = DateTime.Now;
    }
    internal ClientQueue HasQueueInTimeout()
    {
        foreach(ClientQueue queue in QueueList)
        {

            if (queue.IsEmpty())
            {
                continue;
            } 
            var timeSinceLastCall = DateTime.Now - queue.lastCallTime;
            if(timeSinceLastCall.TotalMinutes >= MaxWaiteMinutes)
            {
                return queue;
                
            }
        }
        return null;
    }
     public void InicializeLastcallTime()
    {
        var initialTime = DateTime.Now;
        foreach( ClientQueue queue in QueueList)
        {
            queue.lastCallTime = initialTime;
        }
    }
}