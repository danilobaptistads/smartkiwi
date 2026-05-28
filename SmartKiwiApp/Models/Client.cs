using System.Data.Common;

namespace SmartKiwiApp.Models;
public class Client
{
    public string? Name { get; private set; } 
    public string WaiteTicket { get; private set; }
    public Client( string waiteTicket, string? name = null)
    {
        Name = name;
        WaiteTicket = waiteTicket;

    }
}