using SmartKiwiApp.Models;
using SmartKiwiApp.Repository;

namespace SmartKiwiApp.Services;
public class AtendanteService
{
    private QueueEngine QueueEngine { get; set; }
    private IClientRepository _clientRepository;
    public AtendanteService(QueueEngine queueEngine, IClientRepository clientRepository)
    {
        QueueEngine = queueEngine;
        _clientRepository = clientRepository;
    }

    public Call? ProcessNextCall(Atendante atendante)
    {
        var clientCalled = QueueEngine.ProcessClient();
        if(clientCalled != null)
        {   var clientName = clientCalled.Name;
            var atendanteName = atendante.Name;
            var ticketWindowNumber = atendante.TicketWindow;
            var call =  new Call(clientName, atendanteName, ticketWindowNumber);
            _clientRepository.RemoveClient(clientCalled);
            return call;
        }
        return null;
        
    }

}