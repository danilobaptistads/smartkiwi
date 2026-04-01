namespace SmartKiwiApp.Models;
public class Atendante
{
    public string Name { get; set; }
    public int TicketWindow { get; set; }   
    public Atendante(string name, int ticketWindow) 
    {
        Name = name;
        TicketWindow = ticketWindow;
    }
    
    
}