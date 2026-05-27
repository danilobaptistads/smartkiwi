using SmartKiwiApp.Models;
using SmartKiwiApp.Repository;

namespace SmartKiwiApp.Services;
public class CheckinService
{
    private List<ClientQueue> _clientQueuesList;
    private IClientRepository _clientRepository;
    public CheckinService(List<ClientQueue> clientQueueList, IClientRepository clientRepository)
    {
        _clientQueuesList = clientQueueList;
        _clientRepository = clientRepository;

    }

    public Client? Checkin(Guid selectedQueueId, string? name = null)
    {
        ClientQueue? selectedQueue = null;
        Client newClient;
        foreach(var queue in _clientQueuesList)
        {
            if(queue.Id == selectedQueueId)
            {
                selectedQueue = queue;
                break;
            }
        }
        if (selectedQueue == null)
        {
            throw new ArgumentException ("Não foipossivel encntrar a fila");
        }

        var ticket = genTickt(selectedQueue.Prefix, selectedQueue.LastTicktNumber);
        if(name == null)
        {
            newClient = new Client(ticket);
           
        }
        else
        {
             newClient = new Client(ticket,name);
        }
         selectedQueue.Enqueue(newClient);
        _clientRepository.Add(newClient);
        
        selectedQueue.LastTicktNumber++;
        return newClient;
    }

    private string genTickt(string? queuePrefix, int lastTicktNumber)
    {
        var currentTicktNumber = lastTicktNumber + 1;
        return queuePrefix + currentTicktNumber.ToString("D3");

    }
}