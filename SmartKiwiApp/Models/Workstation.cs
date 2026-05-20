namespace SmartKiwiApp.Models;
public class Workstation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TicketWindow { get; set; }

    public Workstation(string name, string ticketWindow)
    {
        Name = name;
        TicketWindow = ticketWindow;
    }
}
