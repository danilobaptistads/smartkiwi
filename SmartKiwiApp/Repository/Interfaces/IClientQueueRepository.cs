
using SmartKiwiApp.Models;
using SmartKiwiApp.Services;
namespace SmartKiwiApp.Repository;

public interface IClientQueueRepository
{
    public Task Add(User newClientQueue);
    public Task<IEnumerable<User>> GetQueues();
    public Task<User> GetQueueById(Guid queueId);
    public  Task UpdateName(User queueToUpdate, string newName);
    public  Task UpdatePriority(User userToUpdate,  int priority);
    public  Task DeleteQueue(User userToDelete);

}