using SmartKiwi.Services;

namespace SmartKiwi.Controller;
public class MainUi
{
    private QueueBuilderUi QueueBuilderUi;
    private checkInUi CheckInUi;
    private AttendantUi AttendantUi;
     public MainUi (QueueBuilderUi queueBuilderUi, checkInUi checkInUi,AttendantUi attendantUi)
    {
        CheckInUi = checkInUi;
        AttendantUi = attendantUi;
        QueueBuilderUi = queueBuilderUi;
    }
     public void Exec()
    {
        Console.Clear();
        
        Console.WriteLine("Selecione uma Opção:\n ");
        Console.WriteLine("1 - Configurar Filas");
        Console.WriteLine("2 - Realizar Checkin");
        Console.WriteLine("3 - Iniciar Atendimento");
        var chose = Console.ReadLine();

        switch(chose)
        {
            case "1":
                QueueBuilderUi.Exec();
                break;
            case "2":
                CheckInUi.Exec();
                break;
            case "3":
                AttendantUi.Exec();
                break;
            default:
                Console.WriteLine("Opção invalida");
                Console.Clear();
                break;
        }

        Exec();
    }
}