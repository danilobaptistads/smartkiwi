namespace SmartKiwiApp.Models;
public class ClientQueue
{
    public int currentPriority;
    public DateTime lastCallTime;
    public Guid Id { get; private set; }
    public int LastTicktNumber { get; set; }
    public Guid OwnerId { get; private set; } 
    public int Priority { get; private set; }
    public string? Prefix { get; private set; }
    public string Name { get; private set;} = null!;
    private readonly Queue<Client> clientQueue = new();
    protected ClientQueue() { }
    public ClientQueue(string name, Guid ownerId,int priority , string? prefix = null)
    {
        Name = name;
        Prefix = prefix;
        OwnerId = ownerId;
        Priority = priority;
        Id = Guid.NewGuid();
        LastTicktNumber = 0;
        currentPriority = priority;
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
    public void changePrefix(string? newPrefix = null)
    {
        Prefix = newPrefix;
    }
}