using SmartKiwiApp.Models;
public class AtendanteService
{
    private QueueEngine QueueEngine { get; set; }
    public AtendanteService(QueueEngine queueEngine)
    {
        QueueEngine = queueEngine;
    }

    public Call? ProcessNextCall(Atendante atendante)
    {
        var clientCalled = QueueEngine.ProcessClient();
        if(clientCalled != null)
        {   var clientName = clientCalled.Name;
            var atendanteName = atendante.Name;
            var ticketWindowNumber = atendante.TicketWindow;
            var call =  new Call(clientName, atendanteName, ticketWindowNumber);
            return call;
        }
        return null;
        
    }

}