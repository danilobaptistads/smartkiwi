using SmartKiwiApp.Models;
namespace SmartKiwiApp.Repository;

public interface IClientQueueRepository
{
    public Task Add(ClientQueue newClientQueue);
    public Task<IEnumerable<ClientQueue>> GetQueues();
    public Task<ClientQueue> GetQueueById(Guid queueId);
    public Task<IEnumerable<ClientQueue>> GetQueueByUserId(Guid woNerId);
    public Task UpdateQueueName(ClientQueue queueToUpdate, string newName);
    public Task UpdateQueuePriority(ClientQueue queueToUpdate, int priority);
    public Task DeleteQueue(ClientQueue queueToDelete);

}