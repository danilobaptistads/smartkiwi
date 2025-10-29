using SmartKiwi.Models;
using SmartKiwi.Models.Queuef;

public class runApp
{
    public List<Queue> queueList = new List<Queue>();
    public QueueController queueController;
    
    public int waiteTicket = 0;
    public runApp()
    {
        Console.Clear();
        Console.WriteLine("SmartKiwi\n BEM VINDO!");
        Thread.Sleep(2500);
        Console.Clear();
        if (queueList.Count() == 0)
        {
            Console.WriteLine("Nenhuma fila criada, Crie a primeira.");
            Thread.Sleep(2000);
;
            QueueBuilder();

            var mensage = "Digite o tempo maximo de esperade uma fila";
            Console.WriteLine(mensage);
            int maxWaite;
            while (!int.TryParse(Console.ReadLine(), out maxWaite))
            {
                Console.WriteLine("Entrada Inválida");
                Console.WriteLine(mensage);
            }
          
            queueController = new QueueController(queueList, maxWaite);
        }
    }
    public void showOptions()
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
                break;
        }

        showOptions();
    }
    public void QueueBuilder()
    {
        string chose;
        do
        {
            var  queueName = "";
            while (queueName == "")
            {
                Console.Clear();
                Console.WriteLine("Digite o nome da fila");
                queueName = Console.ReadLine();
                if (queueName == "")
                {
                    Console.WriteLine("Nome da fila inválido");
                    Thread.Sleep(2000);
                }
            }

            var returnUser = false;
            while(returnUser == false)
            {
                Console.WriteLine("Digite a prioridade da fila");
                returnUser = int.TryParse(Console.ReadLine(), out int queuePriority);
                if (returnUser)
                {
                    var newQueue = new Queue(queueName, queuePriority);
                    queueList.Add(newQueue);
                    returnUser = true;
                }
                else
                {
                    Console.WriteLine("Entrada Inválida,digite um numero inteiro");
                    Thread.Sleep(2000);
                }
            }
            
            
            Console.WriteLine("Deseja criar outra fila? \n'S' sim / 'N' não");
            chose = Console.ReadLine().ToUpper();

            switch (chose)
            {
                case "S":
                    break;

                case "N":
                    return;

                default:
                    Console.WriteLine("Opção inválida! Digite apenas 'S' ou 'N'.");
                    chose = "S";
                    break;
            }
        } while (chose == "S");
       
}
    public void Checkin()
    {   
        Console.Clear();
        Console.WriteLine("Digite seu Nome");
        var clientName = Console.ReadLine();
        var mensage = "Que tipo de atendimento deseja";
        Console.WriteLine(mensage);
        showQueueNames();
        int choseQueue;
        while (!int.TryParse(Console.ReadLine(), out choseQueue))
        {
            Console.Clear();
            Console.WriteLine("entrada invalida");
            Console.WriteLine(mensage);
            showQueueNames();
        }
            waiteTicket++;
            var newClient = new Client(clientName, waiteTicket);
            queueList[choseQueue - 1].Enqueue(newClient);
            Console.WriteLine($"{clientName} sua senha é: {waiteTicket}");
            Thread.Sleep(1000);  
        

    }
    public void InitAttendat()
    {
        
        var chose = "s";
        var atendente = new Attendante(queueController);
        do
        {
            atendente.CallNext();
            Console.WriteLine("Chamar o Proximo? ( S/ N");
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
    public void showQueueNames()
    {
        var optionNumber = 0;
        foreach (var queue in queueList)
        {
            optionNumber++;
            Console.WriteLine($"{optionNumber} - {queue.Name}");
        }
    }
       
    
}