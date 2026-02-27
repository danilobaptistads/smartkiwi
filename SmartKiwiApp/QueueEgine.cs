using SmartKiwiApp.Models;

public class QueueEngine
{
List<ClientQueue> queueList;
bool isMajorQueue;
int index;
int callsCount;
int sumPriotitys;
ClientQueue currentQueue;
public QueueEngine()
    {
        queueList = new List<ClientQueue>();
        isMajorQueue = true;
        index = 0;
        callsCount = 0;


    }
    public void AddQueue(ClientQueue newQueue)
    {
        queueList.Add(newQueue);
        sumPriotitys = SumOfPrioritys();
        
    }
    public Client ProcessQueue()
    {
        currentQueue = queueList[index];
        if(callsCount >= sumPriotitys)
        {
            resetCurrentPrioritys();
            isMajorQueue = true;
        }

        if(currentQueue.Length() == 0 || currentQueue.currentPriority == 0)
        {
            index = GetNextIndex();
            currentQueue = queueList[index];
            
        }
        
        var Client = currentQueue.Dequeue();
        currentQueue.currentPriority--;
        callsCount++;

        if(isMajorQueue)
        {
            index = GetNextIndex();
            currentQueue = queueList[index];
            isMajorQueue = false;
        }
        else
        {
            index = GetPreviusIndex();
            currentQueue = queueList[index];
            isMajorQueue = true;
        }

        return Client;

        
    }
    internal int GetNextIndex()
    {
        if(index < (queueList.Count - 1))
        {
            index++;
        }
        return index;
    }
    internal int GetPreviusIndex()
    {
        if(index > 0)
        {
            index--;
        }
        return index;
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
}