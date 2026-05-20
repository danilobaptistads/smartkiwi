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

        var newQueue = new ClientQueue(name,wonerId,priority);
        await _clientQueueRepository.Add(newQueue);
    }

}