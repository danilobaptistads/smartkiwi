namespace SmartKiwiApp.Models;

public class Call
{
    public Client Client { get; set; }
    public Atendante Atendante { get; set; }
    public DateTime timeOfProcess { get; set; }
    
    public Call(Client client, Atendante atendante)
    {
        Client = client;
        Atendante = atendante;
        timeOfProcess = DateTime.Now;
    }
}

