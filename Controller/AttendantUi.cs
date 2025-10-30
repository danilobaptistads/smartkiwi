namespace SmartKiwi.Controller;
using SmartKiwi.Services;
public class AttendantUi
{
    private QueueController QueueController;
    public AttendantUi(QueueController queueController)
    {
        QueueController = queueController;
    }
    public void Exec()
    {

        string chose;
        var atendente = new Attendante(QueueController);
        System.Console.WriteLine("Fila não iniciada");
        do
        {
            Console.WriteLine("Chamar? ( S/ N");
            chose = Console.ReadLine().ToLower();
            switch (chose)
            {
                case "s":
                    atendente.CallNext();
                    break;
                case "n":
                    break;
                default:
                    Console.WriteLine("opção invalida");
                    chose = "s";
                    break;
            }
            
            
        } while (chose == "s");
        Console.Clear();

    }



}