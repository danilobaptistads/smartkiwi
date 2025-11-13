
namespace SmartKiwi.Services;
using SmartKiwi.Models;
public class QueueBuilder
{
    private List<Queue> QueueList;
    public QueueBuilder(List<Queue> queueList)
    {
        QueueList = queueList;
    }
    public bool Build(string queueName, bool isPriorityQueue = false)
    {
        try
        {
            var newQueue = new Queue(queueName);
            if (isPriorityQueue == true)
            {
                newQueue.IsPriorityQueue();
                QueueList.Add(newQueue);

            }
            
            QueueList.Add(newQueue);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar a fila: {ex.Message}");
            return false;
        }
    }

    public void SearchQueuePriority()
    {
        var i = 0;
        Queue aux;
        foreach (var queue in QueueList)
        {
            if (queue.PriorityQueue && i != 0)
            {
                aux = QueueList[0];
                QueueList[0] = QueueList[i];
                QueueList[i] = aux;
                break;

            }
        }
    }
    public void SetPriorities()
    {
        var basePriority = QueueList.Count;

        foreach(var queue in QueueList)
        {
            queue.SetPriority(basePriority);
            basePriority--;
        }
    }
}


