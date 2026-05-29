using System.Data.Common;

namespace SmartKiwiApp.Models;
public class Client
{
    public Guid Id { get; private set; }
    public Guid QueueId { get; private set; }
    public string? Name { get; private set; } 
    public string WaiteTicket { get; private set; }
    public Client( Guid queueId, string waiteTicket, string? name = null)
    {
        Name = name;
        QueueId = queueId;
        Id = Guid.NewGuid();
        WaiteTicket = waiteTicket;

    }
}