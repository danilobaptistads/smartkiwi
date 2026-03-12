using SmartKiwiApp.Models;

public class QueueEngine
{
List<ClientQueue> queueList;
int index;
int callsCount;
int sumOfPriotitys;
ClientQueue currentQueue;
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
            ResetLastCalledState();
            callsCount = 0;
            
        

        }
        var activeQueues = ActiveQueuesTotal();
        foreach(ClientQueue queue in queueList)
        {
            
            if( !queue.IsEmpty() && queue.currentPriority > 0 && (!queue.isLastCalled || ActiveQueuesTotal() == 1))
            {

                client = queue.Dequeue();
                ChangeLastCalledState(queue);
                callsCount++;
                queue.currentPriority--;
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

    internal void ResetLastCalledState()
    {
        foreach (ClientQueue queue in queueList)
        {
            queue.isLastCalled = false;
        }
    }

    internal void ChangeLastCalledState(ClientQueue currentQueue)
    {
        foreach (ClientQueue queue in queueList)
        {
            if(currentQueue.Name == queue.Name)
            {
                currentQueue.isLastCalled = true;
                continue;
            }
            if(queue.isLastCalled)
            {
                queue.isLastCalled = false;
            }

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