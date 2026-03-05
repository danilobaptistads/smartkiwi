using System.Security.Cryptography;
using SmartKiwiApp.Models;

public class QueueEngine
{
List<ClientQueue> queueList;
List<ClientQueue> queuesToCall;
bool isMajorQueue;
int index;
int callsCount;
int sumOfPriotitys;
ClientQueue currentQueue;
public QueueEngine()
    {
        queueList = new List<ClientQueue>();
        queuesToCall = new List<ClientQueue>();
        isMajorQueue = true;
        index = 0;
        callsCount = 0;

    }
    public void AddQueue(ClientQueue newQueue)
    {
        queueList.Add(newQueue);
        sumOfPriotitys = SumOfPrioritys();
        
    }
    
    public Client ProcessQueue()
    {
        var emptyQueue = 0;

        if (queuesToCall.Count == 0 || callsCount >= sumOfPriotitys)
        {
            queuesToCall = new List<ClientQueue>(queueList);
            resetCurrentPrioritys();
            callsCount = 0;
            isMajorQueue = true;
            index = 0;
        }

        while (emptyQueue < queuesToCall.Count && queuesToCall.Count > 0)
        {
            currentQueue = queuesToCall[index];

            if (currentQueue.Length() == 0 || currentQueue.currentPriority == 0)
            {
                queuesToCall.RemoveAt(index);
                emptyQueue++;
                continue;
            }

            break;
        }

        currentQueue = queuesToCall[index];

        var client = currentQueue.Dequeue();
        currentQueue.currentPriority--;
        callsCount++;

        UpdateQueueToNextCall();
        UpdateMajorQueueState();

        return client;
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
            if(index < (queuesToCall.Count - 1))
            {
                index++;
            }
        }
        else
        {
            if(index > 0)
            {
                index--;
            }
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