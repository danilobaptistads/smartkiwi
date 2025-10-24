using System.Security.Cryptography.X509Certificates;
using SmartKiwi.Models;
using SmartKiwi.Models.Queuef;

public class runApp
{
    public List<Queue> queueList = new List<Queue>();
    public QueueController queueController;
    
    public int waiteTicket = 0;
    public runApp()
    {
        Console.WriteLine("Digite o tempo maximo de esperade uma fila");
        var maxWaite = int.Parse(Console.ReadLine());
        queueController = new QueueController(queueList, maxWaite);
    }

    public void showOptions()
    {
        Console.WriteLine("SmartKiwi/n");
        Console.WriteLine("Selecione uma Opção: ");
        Console.WriteLine("1 - Configurar Filas");
        Console.WriteLine("2 - Realizar Checkin");
        Console.WriteLine("3 - Iniciar Atendimento");
        var chose = Console.ReadLine();

        switch(chose)
        {
            case "1":
                QueueBuilder();
                break;
            case "2":
                Checkin();
                break;
            case "3":
                InitAttendat();
                break;
            default:
                Console.WriteLine("Opção invalida");
                Console.Clear();
                showOptions();
                break;


        }



    
    }
    public void QueueBuilder()
    {
    
        Console.WriteLine("Digite o nome da fila");
        var queueName = Console.ReadLine();
        Console.WriteLine("Digite a prioridade");
        var queuePriority = int.Parse(Console.ReadLine());

        var newQueue = new Queue(queueName, queuePriority);

        queueList.Add(newQueue);
        showOptions();



    }
    public void Checkin()
    {
        Console.WriteLine("Digite seu Nome");
        var clientName = Console.ReadLine();
        Console.WriteLine("Que tipo de atendimento deseja");
        showQueueNames();
        var choseQueue = int.Parse(Console.ReadLine());
        waiteTicket++;
        var newClient = new Client(clientName, waiteTicket);
        queueList[choseQueue].Enqueue(newClient);
        Console.WriteLine($"{clientName} sua senha é: {waiteTicket}");
        Thread.Sleep(1000);
        showOptions();

    }
    public void InitAttendat()
    {
        var chose = "s";
        var atendente = new Attendante(queueController);
        do
        {
            Console.WriteLine("Chamar o Proximo? /n ( s para Sim / n para sair");
            chose = Console.ReadLine().ToLower();
            switch (chose)
            {
                case "s":
                    atendente.CallNext();
                    break;
                case "n":
                    showOptions();
                    break;
                default:
                    Console.WriteLine("opção invalida");
                    chose = "s";
                    break;
            }
            } while (chose == "s");
        
    }

    public void showQueueNames()
    {
        var optionNumber = 0;
        foreach (var queue in queueList)
        {
            Console.WriteLine($"{optionNumber} - {queue.Name}");
        }
    }
    
     
    
    
}