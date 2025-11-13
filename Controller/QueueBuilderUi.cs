namespace SmartKiwi.Controller;

using SmartKiwi.Services;
public class QueueBuilderUi
{
    private QueueBuilder QueueBuilder;
    public QueueBuilderUi(QueueBuilder queueBuilder)
    {
        QueueBuilder = queueBuilder;
    }
    public void Exec()
    {

        bool process = true;
        do
        {
            DisplayQueues();

            var queueName = ChoseName();

            var isPriorityQueue = IsQueuePriority(queueName);

            if (isPriorityQueue)
            {
                QueueBuilder.Build(queueName, isPriorityQueue);
            }
            else
            {
                QueueBuilder.Build(queueName);
            }

            DisplayQueues();

            process = Process();


        } while (!process);


    }

    private string ChoseName()
    {
        var queueName = "";
        while (queueName == "")
        {
            Console.WriteLine("Digite o nome da fila");
            queueName = Console.ReadLine();
            if (queueName == "")
            {
                Console.WriteLine("Nome da fila inválido");
                Thread.Sleep(2000);
            }
        }
        return queueName;
    }
    private bool IsQueuePriority(string queueName)
    {
        var answer = "";
        while (answer != "s" && answer != "n")
        {
            Console.WriteLine($"{queueName} fila prioritária? (S/N)");
            answer = Console.ReadLine().ToLower();
            if (answer == "n")
            {
                return false;
            }

        }
        return true;
    }

    private void DisplayQueues()
    {
        Console.Clear();
        var ExistentQueues = QueueBuilder.ListExistentQueues();

        System.Console.WriteLine("Listas:\n");
        foreach (var queue in ExistentQueues)
        {

            if (queue.PriorityQueue)
            {
                System.Console.WriteLine($"{queue.Name}  Prioritária");
            }
            else
            {
                Console.WriteLine($"{queue.Name}");
            }
        }
    }

    private bool Process()
    {
        string chose = "";
        while (chose != "S" && chose != "N")
        {
            Console.WriteLine("Deseja criar outra fila? \n'S' sim / 'N' não");
            chose = Console.ReadLine().ToUpper();

            switch (chose)
            {
                case "S":
                    break;

                case "N":
                    return true;

                default:
                    Console.WriteLine("Opção inválida! Digite apenas 'S' ou 'N'.");
                    break;
            }
        }

        return false;
    }
}