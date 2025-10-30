
namespace SmartKiwi.Services;
using SmartKiwi.Models;
public class QueueBuilder
{
    private List<Queue> QueueList; 
    public QueueBuilder(List<Queue> queueList)
    {
        QueueList = queueList;
    }
    public bool Build(string queueName, int queuePriority)
    {  try
        {
            var newQueue = new Queue(queueName, queuePriority);
            QueueList.Add(newQueue);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar a fila: {ex.Message}");
            return false;
        }
    }
}