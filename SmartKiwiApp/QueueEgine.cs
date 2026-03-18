using System.Collections;
using SmartKiwiApp.Models;

public class QueueEngine
{
    List<ClientQueue> queueList;
    int successfullyProcessedQueues;
    int sumOfPriotitys;
    ClientQueue lastProcessedQueue;
    public int MaxWaiteMinutes { get; set; }
    public QueueEngine(int maxWaiteMinutes)
    {
        queueList = new List<ClientQueue>();
        successfullyProcessedQueues = 0;
        MaxWaiteMinutes = maxWaiteMinutes;

    }
    public void AddQueue(ClientQueue newQueue)
    {
        queueList.Add(newQueue);
        sumOfPriotitys = SumOfPrioritys();
        
    }
    public ClientQueue ProcessQueue()
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

                UpdatesToNextProcess(queueInTimeout);
                return queueInTimeout;
                
        
        }
        foreach(ClientQueue queue in queueList)
        {
            if( !queue.IsEmpty() && queue.currentPriority > 0 && (queue != lastProcessedQueue || activeQueues == 1))
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
    internal void UpdatesToNextProcess(ClientQueue queue)
    {
        successfullyProcessedQueues++;
        queue.currentPriority--;
        lastProcessedQueue = queue;
        queue.lastCallTime = DateTime.Now;
    }
    internal ClientQueue HasQueueInTimeout()
    {
        foreach(ClientQueue queue in queueList)
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
        foreach( ClientQueue queue in queueList)
        {
            queue.lastCallTime = initialTime;
        }
    }
}