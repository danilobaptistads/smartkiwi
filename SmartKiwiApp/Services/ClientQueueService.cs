using SmartKiwiApp.Models;
using SmartKiwiApp.Repository;

namespace SmartKiwiApp.Services;
public class ClientQueueService
{
    private readonly IClientQueueRepository _clientQueueRepository;

    public ClientQueueService(IClientQueueRepository clientQueueRepository)
    {
        _clientQueueRepository = clientQueueRepository;
    }

    public async Task CreateNewQueue(string name, Guid wonerId, int priority)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
           throw new ArgumentException("A Fila Precisa de um nome");
        }
            
        if (priority < 0)
        {
            throw new ArgumentException("Priority cannot be negative");
        }

        var newQueue = new ClientQueue(name, wonerId, priority);
        await _clientQueueRepository.Add(newQueue);
    }

    public async Task<IEnumerable<ClientQueue>> GetQueuesOfUser(Guid userId)
    {
        return await _clientQueueRepository.GetQueueByUserId(userId);
    }

    public async Task UpdateQueueName(Guid queueId, string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("A Fila Precisa de um nome");
        }

        var queueToUpdate = await _clientQueueRepository.GetQueueById(queueId);
        if(queueToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel localizar a fila");
        }
        await _clientQueueRepository.UpdateQueueName(queueToUpdate, newName);
    }

    public async Task UpdateQueuePriority(Guid queueId, int newPriority)
    {
        if (int.IsNegative(newPriority))
        {
            throw new ArgumentException("Prioridade não pode ser negativa");
        }

        var queueToUpdate = await _clientQueueRepository.GetQueueById(queueId);
        if(queueToUpdate == null)
        {
            throw new ArgumentException("Não foi possivel localizar a fila");
        }
        await _clientQueueRepository.UpdateQueuePriority(queueToUpdate, newPriority);
    }

    public async Task DeleteQueue(Guid queueId)
    {

        var queueToDelete= await _clientQueueRepository.GetQueueById(queueId);
        if(queueToDelete == null)
        {
            throw new ArgumentException("Não foi possivel localizar a fila");
        }
        await _clientQueueRepository.DeleteQueue(queueToDelete);
    }


}