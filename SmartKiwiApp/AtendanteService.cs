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
        {
            var call =  new Call(clientCalled,atendante);
            return call;
        }
        return null;
        
    }

}