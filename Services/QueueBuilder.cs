
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

    public void SetPriorityQueueFirst()
    {
     
        Queue aux;
        for ( var i = 0; i < QueueList.Count; i++)
        {
            if(QueueList[0].PriorityQueue == true)
            {
                break;
            }
            if (QueueList[i].PriorityQueue == true)
            {
                aux = QueueList[0];
                QueueList[0] = QueueList[i];
                QueueList[i] = aux;
                break;

            }
        }
    }
    public void AssignPriorityByOrder()
    {
        var basePriority = QueueList.Count;

        foreach (var queue in QueueList)
        {
            queue.SetPriority(basePriority);
            basePriority--;
        }
    }
    
    public List<Queue> ListExistentQueues()
    {
        return QueueList;
    }
}


