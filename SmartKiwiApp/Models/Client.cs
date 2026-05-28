using System.Data.Common;

namespace SmartKiwiApp.Models;
public class Client
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; } 
    public string WaiteTicket { get; private set; }
    public Client( string waiteTicket, string? name = null)
    {
        Name = name;
        Id = Guid.NewGuid();
        WaiteTicket = waiteTicket;

    }
}