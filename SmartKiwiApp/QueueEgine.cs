using SmartKiwiApp.Models;

public class QueueEngine
{
List<ClientQueue> queueList;
bool isMajorQueue;
int index;
int nextIndex;
int previosIndex;
ClientQueue currentQueue;
public QueueEngine()
    {
        queueList = new List<ClientQueue>();
        isMajorQueue = true;
        index = 0;
        nextIndex = index + 1;
        previosIndex = index - 1;
        
    }
    public void AddQueue(ClientQueue newQueue)
    {
        queueList.Add(newQueue);
    }
    public Client ProcessQueue()
    {
        if(queueList[index].Length() == 0)
        {
            index = nextIndex;
        }
        

        return queueList[index].Dequeue();
    }

    public int QueueListLength()
    {
        return queueList.Count();
    }

}