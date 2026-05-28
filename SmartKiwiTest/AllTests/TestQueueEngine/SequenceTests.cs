namespace SmartKiwiTest;
using SmartKiwiApp.Models;
public class SequenceTests
{
    private readonly int maxWaiteTime;
    private readonly ClientQueue queueA;
    private readonly ClientQueue queueB;
    private readonly ClientQueue queueC;
    private  QueueEngine queueEngine;
    private  List<ClientQueue> queueList;
    public SequenceTests()
    {
        queueA = new ClientQueue("A", new Guid(), 3);
        queueB = new ClientQueue("B", new Guid(), 2);
        queueC = new ClientQueue("C", new Guid(), 1);
        queueList = new();
        maxWaiteTime = 10;
        queueEngine = new QueueEngine(maxWaiteTime,queueList);
    }

    [Fact]
    public void Deve_Chamar_Na_Sasquencia_ABABAC_Em_Um_Ciclo()
    {
        var callsList = new List<string>();
        queueA.Enqueue(new Client("TICKET", "A_1"));
        queueA.Enqueue(new Client("TICKET", "A_2"));
        queueA.Enqueue(new Client("TICKET", "A_3"));
        queueB.Enqueue(new Client("TICKET", "B_1"));
        queueB.Enqueue(new Client("TICKET", "B_2"));
        queueC.Enqueue(new Client("TICKET", "C_1"));
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
       // queueEngine = new QueueEngine(maxWaiteTime,queueList);
        queueEngine. InicializeLastcallTime();

        for (int i = 0; i < 6; i++)
        {
            var clientCalled = queueEngine.ProcessClient();
            if(clientCalled != null)
            {
            callsList.Add(clientCalled.Name ?? string.Empty);
            }
        }

        Assert.Collection(
            callsList,
            q => Assert.Same("A_1", q),
            q => Assert.Same("B_1", q),
            q => Assert.Same("A_2", q),
            q => Assert.Same("B_2", q),
            q => Assert.Same("A_3", q),
            q => Assert.Same("C_1", q)

            );
    }
    [Fact]
    public void Deve_Chamar_Na_Sasquencia_ABABACABABAC_Em_2_Ciclos()
    {
        var callsList = new List<string>();
        queueA.Enqueue(new Client("TICKT","A_1"));
        queueA.Enqueue(new Client("TICKT","A_2"));
        queueA.Enqueue(new Client("TICKT","A_3"));
        queueA.Enqueue(new Client("TICKT","A_4"));
        queueA.Enqueue(new Client("TICKT","A_5"));
        queueA.Enqueue(new Client("TICKT","A_6"));
        queueB.Enqueue(new Client("TICKT","B_1"));
        queueB.Enqueue(new Client("TICKT","B_2"));
        queueB.Enqueue(new Client("TICKT","B_3"));
        queueB.Enqueue(new Client("TICKT","B_4"));
        queueC.Enqueue(new Client("TICKT","C_1"));
        queueC.Enqueue(new Client("TICKT","C_2"));
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        queueEngine = new QueueEngine(maxWaiteTime,queueList);
        queueEngine. InicializeLastcallTime();

        for (int i = 0; i < 12; i++)
        {
            var ClientCalled = queueEngine.ProcessClient();
            if(ClientCalled != null)
            {
                callsList.Add(ClientCalled.Name ?? string.Empty);
            }
                
        }

        Assert.Collection(
            callsList,
            q => Assert.Same("A_1", q),
            q => Assert.Same("B_1", q),
            q => Assert.Same("A_2", q),
            q => Assert.Same("B_2", q),
            q => Assert.Same("A_3", q),
            q => Assert.Same("C_1", q),
            q => Assert.Same("A_4", q),
            q => Assert.Same("B_3", q),
            q => Assert.Same("A_5", q),
            q => Assert.Same("B_4", q),
            q => Assert.Same("A_6", q),
            q => Assert.Same("C_2", q)

            );
   }
    [Fact]
    public void Deve_Chamar_Uma_vez_Cada_Quando_Prioridades_Iguais()
    {
        Client? clientCalled;
        var expected = new string[]{"A_1","B_1","C_1"};
        var callsList = new List<string>();
        queueA.SetPriority(1);
        queueB.SetPriority(1);
        queueC.SetPriority(1);
        queueList.Add(queueA);
        queueList.Add(queueB);
        queueList.Add(queueC);
        queueEngine = new QueueEngine(maxWaiteTime,queueList);
        queueEngine. InicializeLastcallTime();
        queueA.Enqueue(new Client("TICKET","A_1"));
        queueB.Enqueue(new Client("TICKET","B_1"));
        queueC.Enqueue(new Client("TICKET","C_1"));
            
        while ((clientCalled = queueEngine.ProcessClient()) != null)
        {
            callsList.Add(clientCalled.Name ?? string.Empty);
        }
        Assert.Equal(expected, callsList);
    }
    [Fact]
    public void Deve_Chamar_Mesmo_Com_Uma_Fila()
    {
        Client? clientCalled;
        var expected = new string[]{"A_1","A_2","A_3","A_4"};
        var callsList = new List<string>();
        queueList.Add(queueA);
        //queueEngine = new QueueEngine(maxWaiteTime,queueList);

        queueEngine. InicializeLastcallTime();
        queueA.Enqueue(new Client("TIKET", "A_1"));
        queueA.Enqueue(new Client("TIKET", "A_2"));
        queueA.Enqueue(new Client("TIKET", "A_3"));
        queueA.Enqueue(new Client("TIKET", "A_4"));

        while ((clientCalled = queueEngine.ProcessClient()) != null)
        {
            callsList.Add(clientCalled.Name ?? string.Empty);
        }
        Assert.Equal(expected, callsList);
    }
}