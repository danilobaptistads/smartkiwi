namespace SmartKiwiApp.Models;
public class Atendante
{
    public string Name { get; set; }
    public string TicketWindow { get; set; }   
    public Atendante(string name, string ticketWindow) 
    {
        Name = name;
        TicketWindow = ticketWindow;
    }
    
    
}