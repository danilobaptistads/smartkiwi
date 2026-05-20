using SmartKiwiApp.Models;
namespace SmartKiwiApp.Repository;

public interface IClientQueueRepository
{
    public Task Add(ClientQueue newClientQueue);
    public Task<IEnumerable<CleintQueue>> GetQueues();
    public Task<CleintQueue> GetQueueById(Guid queueId);
    public  Task UpdateQueueName(CleintQueue queueToUpdate, string newName);
    public  Task UpdateQueuePriority(CleintQueue queueToUpdate,  int priority);
    public  Task DeleteQueue(CleintQueue queueToDelete);

}