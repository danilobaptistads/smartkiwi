namespace SmartKiwiApp.Models;
public class ClientQueue
{
    public int currentPriority;
    public DateTime lastCallTime;
    public Guid Id { get; private set; }
    public string Name { get; private set;}
    public int Priority { get; private set; }
    private readonly Queue<Client> clientQueue;
    protected ClientQueue() { }
    public ClientQueue(string name)
    {
        Id = Guid.NewGuid();
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



