namespace SmartKiwi.Models;

public class Queue
{
    private Node first;
    private Node last;
    public int length;
    public int currentPriority;
    public DateTime? lastCall = null;
    public string Name { get; }
    private int Priority { get; set; }
    public Queue(string name, int priority)
    {
        Name = name;
        last = null;
        first = null;
        length = 0;
        Priority = priority;
        currentPriority = priority;
    }

    public void Enqueue(Client client)
    {
        var newNode = new Node(client);
        if (length != 0)
        {
            last.NextNode = newNode;
            last = newNode;
            length++;
            return;
        }
        first = newNode;
        last = newNode;
        length++;


    }

    public Client Dequeue()
    {
        if (IsEmpty())
        {
            System.Console.WriteLine(Name);
            throw new InvalidOperationException();
        }

        var currentNode = first;
        first = currentNode.NextNode;
        length--;

        if (first == null)
        {
            last = null;
        }


        return currentNode.data;
    }
    
    public Client GetLastNode()
    {
        return last.data;
    }
    public Boolean IsEmpty()
    {
        return length == 0;
    }

    public void ResetPriorit()
    {
        currentPriority = Priority;
    }
}



