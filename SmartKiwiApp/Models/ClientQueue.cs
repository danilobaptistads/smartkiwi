namespace SmartKiwiApp.Models;
public class ClientQueue
{
    public int currentPriority;
    public int LastTicktNumber;
    public DateTime lastCallTime;
    public Guid Id { get; private set; }
    public string Name { get; private set;}
    public Guid WonerId { get; private set; }
    public int Priority { get; private set; }
    public string? Prefix { get; private set; }

    private readonly Queue<Client> clientQueue;
    protected ClientQueue() { }
    public ClientQueue(string name, Guid wonerId,int priority , string? prefix = null)
    {
        Name = name;
        Prefix = prefix;
        WonerId = wonerId;
        Priority = priority;
        Id = Guid.NewGuid();
        LastTicktNumber = 0;
        currentPriority = priority;
        lastCallTime = DateTime.MinValue;
        clientQueue = new Queue<Client>();
       
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