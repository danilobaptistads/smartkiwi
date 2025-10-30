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
        string chose;
        do
        {
            var queueName = "";
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
            int queuePriority;
            var returnUser = false;

            while (returnUser == false)
            {
                Console.WriteLine("Digite a prioridade da fila");
                returnUser = int.TryParse(Console.ReadLine(), out queuePriority);
                if (returnUser)
                {

                    if (QueueBuilder.Build(queueName, queuePriority))
                    {
                        returnUser = true;
                    }
                    else
                    {
                        Console.WriteLine("Não foi possivel criar a fila tente novamente");
                        Thread.Sleep(2000);

                    }

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

}