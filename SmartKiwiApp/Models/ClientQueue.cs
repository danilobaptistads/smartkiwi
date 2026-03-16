namespace SmartKiwiApp.Models;
public class ClientQueue
{
    private Queue<Client> clientQueue;
    public string Name { get; }
    public int Priority { get; private set; }
    public int currentPriority;
    public DateTime lastCallTime;
    
    public ClientQueue(string name)
    {
        Name = name;
        clientQueue = new Queue<Client>();
        lastCallTime = DateTime.MinValue;
      
    }

    public void Enqueue(Client client)
    {
       clientQueue.Enqueue(client);
    }
    public Client Dequeue()
    {
       return clientQueue.Dequeue();
    }
    public void SetPriority(int priority)
    {
        Priority = priority;
        currentPriority = Priority;
    }
    public bool IsEmpty()
    {
        if(clientQueue.Count() > 0)
        {
            return false;
        }
        return true;
    }

}



