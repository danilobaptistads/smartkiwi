using System.Security.Cryptography;
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
            UpdateQueueToNextCall();
            UpdateMajorQueueState();
        
        }
        
        var Client = currentQueue.Dequeue();
        currentQueue.currentPriority--;
        callsCount++;

        UpdateQueueToNextCall();
        UpdateMajorQueueState();

        return Client;

        
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
    internal void UpdateQueueToNextCall()
    {
        if(isMajorQueue)
        {
            if(index < (queueList.Count - 1))
            {
                index++;
            }
            currentQueue = queueList[index];
        }
        else
        {
            if(index > 0)
            {
                index--;
            }
            currentQueue = queueList[index];
        }
    }
    internal void UpdateMajorQueueState()
    {
        if(isMajorQueue)
        {
            isMajorQueue = false;
        }
        else
        {
            isMajorQueue = true;
        }
    }
}