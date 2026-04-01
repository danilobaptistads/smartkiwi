namespace SmartKiwiApp.Models;

public class Call
{
    public Guid Id { get; private set; }
    public string ClientName { get; private set; }
    public string AtendanteName { get; private set; }
    public int TicketWindowNumber { get; private set; }
    public DateTime timeOfProcess { get; private set; }
    protected Call() { }
    public Call(string clientName, string atendanteName, int ticketWindowNumber)
    {
        Id = Guid.NewGuid();
        ClientName = clientName;
        AtendanteName = atendanteName;
        TicketWindowNumber = ticketWindowNumber;
        timeOfProcess = DateTime.Now;
    }
}

