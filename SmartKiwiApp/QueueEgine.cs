using SmartKiwiApp.Models;

public class QueueEngine
{
List<ClientQueue> queueList;
int currentQueueIndex;
int nextQueueIndex;
bool isMajorQueue;
public QueueEngine()
    {
        queueList = new List<ClientQueue>();
        queueList.Count();
        currentQueueIndex = 0;
        isMajorQueue = true;
        
    }
    public void AddQueue(ClientQueue newQueue)
    {
        queueList.Add(newQueue);
    }
    public Client ProcessQueue()
    {
        return default;
    }

    public int QueueListLength()
    {
        return queueList.Count();
    }

}