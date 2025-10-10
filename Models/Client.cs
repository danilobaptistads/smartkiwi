namespace SmartKiwi.Models;

public class Client
{
    public string Name { get; set; }
    public int WaiteTickt { get; set; }
    public Client(string name, int waiteTickt)
    {
        Name = name;
        WaiteTickt = waiteTickt;

    }
}