namespace SmartKiwiApp.Models;
public class ClientQueue
{
    private Queue<Client> clientQueue;
    public string Name { get; }
    public DateTime? lastCall = null;
    public int Priority { get; private set; }
    public int currentPriority;
    public bool IsPriorityQueue { get; private set; }
    
    public ClientQueue(string name)
    {
        Name = name;
        clientQueue = new Queue<Client>();
        IsPriorityQueue = false;
    }

    public void MakeAsPriorityQueue ()
    {
        IsPriorityQueue = true;
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

}



