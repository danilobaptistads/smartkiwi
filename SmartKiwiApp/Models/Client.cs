namespace SmartKiwi.Models;

public class Client
{
    public string Name { get; set; }
    public int WaiteTicket { get; set; }
    public Client(string name, int waiteTicket)
    {
        Name = name;
        WaiteTicket = waiteTicket;

    }
}